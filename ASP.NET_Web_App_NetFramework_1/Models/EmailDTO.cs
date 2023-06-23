using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Web_App_NetFramework_1.Controllers
{
    public class EmailDTO
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}