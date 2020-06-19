using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RadiusParserLogs
{
   public partial class Form1 : Form
   {
      /*
       * Нужная информация при неудачной авторизации (Reason-Code - 16) – 
         Дата/Время (Timestamp), 
         IP (NAS-IP-Address),
         Логин (User-Name).
         Нужная информация при успешной авторизации (Reason-Code - 0) – 
         Дата/Время (Timestamp), 
         IP (NAS-IP-Address), 
         Клиент (Client-Friendly-Name), 
         Логин (User-Name), 
         Политика (NP-Policy-Name)
       */
      private string Lines;

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
         if (ofd.ShowDialog() == DialogResult.OK)
         {
            lv.Items.Clear();

            // Считываем из файла
            using (FileStream stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
                  Lines = reader.ReadToEnd();

            // Парсим лог 
            XDocument xml = XDocument.Parse("<events>" + Lines + "</events>");
            string classID;

            foreach (var evnt in xml.Descendants("Event"))
            {
               Events events = new Events();

               events.reasonCode = evnt.Element("Reason-Code")?.Value;
               events.timestamp = evnt.Element("Timestamp")?.Value;
               events.nasIpAddress = evnt.Element("NAS-IP-Address")?.Value;
               events.clientFriendlyName = evnt.Element("Client-Friendly-Name")?.Value;
               events.userName = evnt.Element("User-Name")?.Value;
               events.npPolicyName = evnt.Element("NP-Policy-Name")?.Value;

               // Фильтрация по Reason-Code 16 и 0
               if (events.reasonCode != "0" && events.reasonCode != "16") continue;

               ListViewItem item = new ListViewItem(new string[]
               {
                  events.reasonCode,
                  events.timestamp,
                  events.nasIpAddress,
                  events.clientFriendlyName,
                  events.userName,
                  events.npPolicyName
               });

               if(events.reasonCode=="0") item.BackColor = Color.LightGreen;
               if(events.reasonCode=="16") item.BackColor = Color.IndianRed;

               lv.Items.Add(item);
            }
            // Авто Размер колонок
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
         }
      }
   }
}
