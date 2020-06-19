using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RadiusParserLogs
{
   public partial class Form1 : Form
   {
      private string _lines;
      private XDocument xml;
      private List<Events> events;

      public Form1()
      {
         InitializeComponent();

         // Добавляем колонки
         lv.Columns.Add("Reason-Code"); // Reason-Code
         lv.Columns.Add("Дата/Время"); // Timestamp
         lv.Columns.Add("IP"); // NAS-IP-Address
         lv.Columns.Add("Client-Friendly-Name"); // Client-Friendly-Name
         lv.Columns.Add("Логин"); // User-Name
         lv.Columns.Add("Политика"); // NP-Policy-Name

         // Авто Размер колонок
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
      }

      private void openbtn_Click(object sender, EventArgs e)
      {
         // Открываем файл
         if (ofd.ShowDialog() != DialogResult.OK) return;
         lv.Items.Clear();

         readFile();
         parseXml();
         addListView();

         // Авто Размер колонок
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
      }

      /// <summary>
      /// Считываем из файла
      /// </summary>
      private void readFile()
      {
         using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
         using (var reader = new StreamReader(stream))
            _lines = reader.ReadToEnd();
      }

      /// <summary>
      /// Парсинг лога
      /// </summary>
      private void parseXml()
      {
         // Парсим лог 
         xml = XDocument.Parse("<events>" + _lines + "</events>");
         events = new List<Events>();

         foreach (var evnt in xml.Descendants("Event"))
         {
            // Фильтрация по Reason-Code 16 и 0
            if (evnt.Element("Reason-Code")?.Value != "0" && evnt.Element("Reason-Code")?.Value != "16") continue;

            events.Add(new Events
            {
               reasonCode = evnt.Element("Reason-Code")?.Value,
               timestamp = evnt.Element("Timestamp")?.Value,
               nasIpAddress = evnt.Element("NAS-IP-Address")?.Value,
               clientFriendlyName = evnt.Element("Client-Friendly-Name")?.Value,
               userName = evnt.Element("User-Name")?.Value,
               npPolicyName = evnt.Element("NP-Policy-Name")?.Value
            });
         }
      }

      /// <summary>
      /// Добавление в таблицу
      /// </summary>
      private void addListView()
      {
         foreach (var evnt in events)
         {
            var item = new ListViewItem(new[]
            {
               evnt.reasonCode,
               evnt.timestamp,
               evnt.nasIpAddress,
               evnt.clientFriendlyName,
               evnt.userName,
               evnt.npPolicyName
            });

            switch (evnt.reasonCode)
            {
               case "0":
                  item.BackColor = Color.LightGreen;
                  break;
               case "16":
                  item.BackColor = Color.IndianRed;
                  break;
            }

            lv.Items.Add(item);
         }
      }
   }
}
