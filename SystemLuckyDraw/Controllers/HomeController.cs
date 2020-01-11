using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLuckyDraw.Models;

namespace SystemLuckyDraw.Controllers
{
    
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
      
        public ActionResult AddNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNumber(WinningNumber number)
        {
            
            db.winningNumbers.Add(number);

            db.SaveChanges();

            return RedirectToAction("WinningNumber");
        }

        public ActionResult WinningNumber()
        {
            

            return View(db.winningNumbers.ToList());
        }

        public ActionResult Winners()
        {


            return View(db.prices.ToList());
        }
    }
}