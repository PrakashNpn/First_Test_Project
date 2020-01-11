using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLuckyDraw.Models;
using System.Web.Security;

namespace SystemLuckyDraw.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users model)
        {
            /*bool isValid = db.LoginUsers.Any(x => x.Email == model.Email && x.Password == model.Password && x.Email != "admin@ucsm.com");
            if (isValid)
            {
                Session["Id"] = model.Id;
                Session["Email"] = model.Email;
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("AddNumber", "Home");
            }
            else if (db.LoginUsers.Any(x => x.Email == "admin@ucsm.com" && x.Password == "wakeUPup@1"))
            {
                Session["Id"] = model.Id;
                Session["Email"] = model.Email;
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("DrawPrize", "Admin");
            }*/
            Users isValid = db.LoginUsers.Where(m => m.Email.Equals(model.Email) && m.Password.Equals(model.Password)).SingleOrDefault();
            if (isValid.Email == "admin@ucsm.com" && isValid.Password == "wakeUPup@1")
            {
                
                Session["Email"] = model.Email;
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("DrawPrize", "Admin");
            }
            Users isUser = db.LoginUsers.Where(m => m.Email.Equals(model.Email) && m.Password.Equals(model.Password)).SingleOrDefault();
            if (isUser.Email == model.Email && isUser.Password == model.Password)
            {
                Session["UserId"] = isUser.Id.ToString();
                Session["Email"] = model.Email;
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("AddNumber", "Home");
            }


            ModelState.AddModelError("", "Invalid Username or Password"); 
            return View("Login");    
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users users)
        {

           db.LoginUsers.Add(users);
           db.SaveChanges();
            
            return RedirectToAction("Login");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}