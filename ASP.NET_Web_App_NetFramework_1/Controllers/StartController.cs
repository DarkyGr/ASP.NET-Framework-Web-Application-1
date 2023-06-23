using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ASP.NET_Web_App_NetFramework_1.Models;
using ASP.NET_Web_App_NetFramework_1.Data;
using ASP.NET_Web_App_NetFramework_1.Services;


namespace ASP.NET_Web_App_NetFramework_1.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Login()
        {
            return View();
        }

        // Login HttpPost
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            UserDTO user = DBUser.Validate(email, UtilityService.ConvertSHA256(password));

            if (user != null)
            {
                // If the user hasn't confirmed
                if (!user.uConfirmed)
                {
                    ViewBag.Message = $"You need to confirm your account. An email was sent to {email}";
                }
                // If the user hasn't reseted
                else if (user.uReset)
                {
                    ViewBag.Message = $"Your account has been requested to be restored, please check your inbox {email}";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.Message = "No matches found";
            }


            return View();
        }
    }
}