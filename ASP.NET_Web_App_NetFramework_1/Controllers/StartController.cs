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


        // Register Method
        public ActionResult Register()
        {
            return View();
        }

        // Register Method
        [HttpPost]
        public ActionResult Register(UserDTO user)
        {
            if (user.uPassword != user.passwordConfirmed)
            {
                ViewBag.Name = user.uName;
                ViewBag.Email = user.uEmail;
                ViewBag.Message = "Passwords don't match";
                return View();
            }

            // If doesn't exist the user, 
            if (DBUser.GetUserByEmail(user.uEmail) == null)
            {
                user.uPassword = UtilityService.ConvertSHA256(user.uPassword);
                user.uToken = UtilityService.GenerateToken();
                user.uReset = false;
                user.uConfirmed = false;
                bool response = DBUser.Register(user);

                if (response)
                {
                    string path = HttpContext.Server.MapPath("~/Template/Confirm.html");
                    string content = System.IO.File.ReadAllText(path);
                    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Start/Confirm?token=" + user.uToken);

                    string htmlBody = string.Format(content, user.uName, url);

                    EmailDTO emailDTO = new EmailDTO()
                    {
                        To = user.uEmail,
                        Subject = "Confirmation email",
                        Body = htmlBody
                    };

                    bool sended = EmailService.Send(emailDTO);
                    ViewBag.Created = true;
                    ViewBag.Message = $"Your account has been created. We have sent an email to {user.uEmail} to confirm your account";
                }
                else
                {
                    ViewBag.Message = "Your account couldn't be created";
                }
            }
            else
            {
                ViewBag.Message = "The email is already registered";
            }

            return View();
        }


        // Confirm Methid
        public ActionResult Confirm(string token)
        {
            ViewBag.Response = DBUser.Confirm(token);
            return View();
        }
    }
}