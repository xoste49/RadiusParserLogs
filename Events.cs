using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiusParserLogs
{
   class Events
   {
      public string reasonCode { get; set; }
      public string timestamp { get; set; }
      public string nasIpAddress { get; set; }
      public string clientFriendlyName { get; set; }
      public string userName { get; set; }
      public string npPolicyName { get; set; }
   }
}
