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
         
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:47.638</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <User-Name data_type="1">andrew</User-Name>
            <NAS-IP-Address data_type="3">10.1.3.240</NAS-IP-Address>
            <NAS-Port-Type data_type="0">0</NAS-Port-Type>
            <Called-Station-Id data_type="1">00-03-0f-1e-23-f5</Called-Station-Id>
            <Calling-Station-Id data_type="1">00-00-00-00-00-00</Calling-Station-Id>
            <NAS-Port data_type="0">4294967295</NAS-Port>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 1</Class>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Fully-Qualifed-User-Name data_type="1">CORP\andrew</Fully-Qualifed-User-Name>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <Packet-Type data_type="0">1</Packet-Type>
            <Reason-Code data_type="0">0</Reason-Code>
         </Event>
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:47.638</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 1</Class>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <Fully-Qualifed-User-Name data_type="1">CORP\andrew</Fully-Qualifed-User-Name>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Packet-Type data_type="0">3</Packet-Type>
            <Reason-Code data_type="0">16</Reason-Code>
         </Event>
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:54.544</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <User-Name data_type="1">andrew</User-Name>
            <NAS-IP-Address data_type="3">10.1.3.240</NAS-IP-Address>
            <NAS-Port-Type data_type="0">0</NAS-Port-Type>
            <Called-Station-Id data_type="1">00-03-0f-1e-23-f5</Called-Station-Id>
            <Calling-Station-Id data_type="1">00-00-00-00-00-00</Calling-Station-Id>
            <NAS-Port data_type="0">4294967295</NAS-Port>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 2</Class>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <NP-Policy-Name data_type="1">Доступ (RO)</NP-Policy-Name>
            <MS-Extended-Quarantine-State data_type="0">0</MS-Extended-Quarantine-State>
            <MS-Quarantine-State data_type="0">0</MS-Quarantine-State>
            <Quarantine-Update-Non-Compliant data_type="0">1</Quarantine-Update-Non-Compliant>
            <Fully-Qualifed-User-Name data_type="1">corp.tmpk.net/Структура компании/Технический отдел/Служба информационных технологий/Андрей Поляков</Fully-Qualifed-User-Name>
            <Packet-Type data_type="0">1</Packet-Type>
            <Reason-Code data_type="0">0</Reason-Code>
         </Event>
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:54.544</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 2</Class>
            <MS-Extended-Quarantine-State data_type="0">0</MS-Extended-Quarantine-State>
            <MS-Quarantine-State data_type="0">0</MS-Quarantine-State>
            <Fully-Qualifed-User-Name data_type="1">corp.tmpk.net/Структура компании/Технический отдел/Служба информационных технологий/Андрей Поляков</Fully-Qualifed-User-Name>
            <MS-Link-Drop-Time-Limit data_type="0">120</MS-Link-Drop-Time-Limit>
            <MS-Link-Utilization-Threshold data_type="0">50</MS-Link-Utilization-Threshold>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Service-Type data_type="0">1</Service-Type>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <NP-Policy-Name data_type="1">Доступ (RO)</NP-Policy-Name>
            <Cisco-AV-Pair data_type="1">shell:priv-lvl=1</Cisco-AV-Pair>
            <Quarantine-Update-Non-Compliant data_type="0">1</Quarantine-Update-Non-Compliant>
            <Packet-Type data_type="0">2</Packet-Type>
            <Reason-Code data_type="0">0</Reason-Code>
         </Event>
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:54.560</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <User-Name data_type="1">andrew</User-Name>
            <NAS-IP-Address data_type="3">10.1.3.240</NAS-IP-Address>
            <NAS-Port-Type data_type="0">0</NAS-Port-Type>
            <Called-Station-Id data_type="1">00-03-0f-1e-23-f5</Called-Station-Id>
            <Calling-Station-Id data_type="1">00-00-00-00-00-00</Calling-Station-Id>
            <NAS-Port data_type="0">4294967295</NAS-Port>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 3</Class>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <NP-Policy-Name data_type="1">Доступ (RO)</NP-Policy-Name>
            <MS-Extended-Quarantine-State data_type="0">0</MS-Extended-Quarantine-State>
            <MS-Quarantine-State data_type="0">0</MS-Quarantine-State>
            <Quarantine-Update-Non-Compliant data_type="0">1</Quarantine-Update-Non-Compliant>
            <Fully-Qualifed-User-Name data_type="1">corp.tmpk.net/Структура компании/Технический отдел/Служба информационных технологий/Андрей Поляков</Fully-Qualifed-User-Name>
            <Packet-Type data_type="0">1</Packet-Type>
            <Reason-Code data_type="0">0</Reason-Code>
         </Event>
         <Event>
            <Timestamp data_type="4">01/18/2019 13:04:54.560</Timestamp>
            <Computer-Name data_type="1">ALPHA</Computer-Name>
            <Event-Source data_type="1">IAS</Event-Source>
            <Class data_type="1">311 1 10.1.3.10 01/18/2019 09:54:08 3</Class>
            <MS-Extended-Quarantine-State data_type="0">0</MS-Extended-Quarantine-State>
            <MS-Quarantine-State data_type="0">0</MS-Quarantine-State>
            <Fully-Qualifed-User-Name data_type="1">corp.tmpk.net/Структура компании/Технический отдел/Служба информационных технологий/Андрей Поляков</Fully-Qualifed-User-Name>
            <MS-Link-Drop-Time-Limit data_type="0">120</MS-Link-Drop-Time-Limit>
            <MS-Link-Utilization-Threshold data_type="0">50</MS-Link-Utilization-Threshold>
            <Client-IP-Address data_type="3">10.1.3.240</Client-IP-Address>
            <Client-Vendor data_type="0">9</Client-Vendor>
            <Client-Friendly-Name data_type="1">Общий</Client-Friendly-Name>
            <Proxy-Policy-Name data_type="1">Cisco</Proxy-Policy-Name>
            <Provider-Type data_type="0">1</Provider-Type>
            <Service-Type data_type="0">1</Service-Type>
            <SAM-Account-Name data_type="1">CORP\andrew</SAM-Account-Name>
            <Authentication-Type data_type="0">1</Authentication-Type>
            <NP-Policy-Name data_type="1">Доступ (RO)</NP-Policy-Name>
            <Cisco-AV-Pair data_type="1">shell:priv-lvl=1</Cisco-AV-Pair>
            <Quarantine-Update-Non-Compliant data_type="0">1</Quarantine-Update-Non-Compliant>
            <Packet-Type data_type="0">2</Packet-Type>
            <Reason-Code data_type="0">0</Reason-Code>
         </Event>
       */
      private string Lines;

      public Form1()
      {
         InitializeComponent();

         // Добавляем колонки
         lv.Columns.Add("Reason-Code");
         lv.Columns.Add("Timestamp");
         lv.Columns.Add("NAS-IP-Address");
         lv.Columns.Add("Client-Friendly-Name");
         lv.Columns.Add("User-Name");
         lv.Columns.Add("NP-Policy-Name");

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
         }
      }
   }
}
