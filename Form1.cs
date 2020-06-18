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

            //Directory = Path.GetDirectoryName(ofdFile.FileName);
            //FileName = Path.GetFileName(ofdFile.FileName);

            // Считываем из файла
            using (FileStream stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
               //stream.Seek(currentPosition, SeekOrigin.Begin);
               using (StreamReader reader = new StreamReader(stream))
               {
                  Lines = reader.ReadToEnd();
                  //currentPosition = stream.Position;
               }
            }

            // Парсим лог 
            XDocument xml = XDocument.Parse("<events>" + Lines + "</events>");
            string classID;

            foreach (var evnt in xml.Descendants("Event"))
            {
               //classID = evnt.Element("Class").Value;

               //lv.Columns.Add("Reason-Code");
               //lv.Columns.Add("Timestamp");
               //lv.Columns.Add("NAS-IP-Address");
               //lv.Columns.Add("Client-Friendly-Name");
               //lv.Columns.Add("User-Name");
               //lv.Columns.Add("NP-Policy-Name");

               //requests[classID].setResponce(events);
               //List<Events> events = new List<Events>();
               RadiusParserLogs.Events events = new Events();

               if (evnt.Elements("Reason-Code").Any()) events.reasonCode = evnt.Element("Reason-Code").Value;
               if (evnt.Elements("Timestamp").Any()) events.timestamp = evnt.Element("Timestamp").Value;
               if (evnt.Elements("NAS-IP-Address").Any()) events.nasIpAddress = evnt.Element("NAS-IP-Address").Value;
               if (evnt.Elements("Client-Friendly-Name").Any()) events.clientFriendlyName = evnt.Element("Client-Friendly-Name").Value;
               if (evnt.Elements("User-Name").Any()) events.userName = evnt.Element("User-Name").Value;
               if (evnt.Elements("NP-Policy-Name").Any()) events.npPolicyName = evnt.Element("NP-Policy-Name").Value;
               ListViewItem item = new ListViewItem(new string[]
               {
                  events.reasonCode,
                  events.timestamp,
                  events.nasIpAddress,
                  events.clientFriendlyName,
                  events.userName,
                  events.npPolicyName
               });

               //item.BackColor = requests[classID].getRowColor();
               lv.Items.Add(item);

               //this.Invoke(new MethodInvoker(delegate { lvLogTable.Items.Add(item); }));
               //if (cbScroll.Checked)
               //{
               //   this.Invoke(new MethodInvoker(delegate { lvLogTable.Items[lvLogTable.Items.Count - 1].EnsureVisible(); }));
               //}

               
            }
            // Авто Размер колонок
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
         }
      }
   }
}
