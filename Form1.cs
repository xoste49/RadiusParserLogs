using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RadiusParserLogs
{
   public partial class Form1 : Form
   {
      private string _lines;

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

         // Считываем из файла
         using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
         using (var reader = new StreamReader(stream))
            _lines = reader.ReadToEnd();

         // Парсим лог 
         var xml = XDocument.Parse("<events>" + _lines + "</events>");

         foreach (var evnt in xml.Descendants("Event"))
         {
            var events = new Events
            {
               reasonCode = evnt.Element("Reason-Code")?.Value,
               timestamp = evnt.Element("Timestamp")?.Value,
               nasIpAddress = evnt.Element("NAS-IP-Address")?.Value,
               clientFriendlyName = evnt.Element("Client-Friendly-Name")?.Value,
               userName = evnt.Element("User-Name")?.Value,
               npPolicyName = evnt.Element("NP-Policy-Name")?.Value
            };


            // Фильтрация по Reason-Code 16 и 0
            if (events.reasonCode != "0" && events.reasonCode != "16") continue;

            var item = new ListViewItem(new string[]
            {
               events.reasonCode,
               events.timestamp,
               events.nasIpAddress,
               events.clientFriendlyName,
               events.userName,
               events.npPolicyName
            });

            switch (events.reasonCode)
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
         // Авто Размер колонок
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
         lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
      }
   }
}
