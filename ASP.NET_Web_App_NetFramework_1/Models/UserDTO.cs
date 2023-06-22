using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Web_App_NetFramework_1.Models
{
    public class UserDTO
    {
        public int u_Id { get; set; }
        public string uName { get; set; }
        public string uEmail { get; set; }
        public string uPassword { get; set; }
        public string passwordConfirmed { get; set; }
        public bool uReset { get; set;}
        public bool uConfirmed { get; set; }
        public string uToken { get; set; }
    }
}