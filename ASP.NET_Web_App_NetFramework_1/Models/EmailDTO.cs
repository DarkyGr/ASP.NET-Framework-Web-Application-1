using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Web_App_NetFramework_1.Controllers
{
    public class EmailDTO
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}