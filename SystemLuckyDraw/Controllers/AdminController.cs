using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemLuckyDraw.Models;
namespace SystemLuckyDraw.Controllers
{
    
    public class AdminController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Admin
       
        public ActionResult DrawPrize()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult DrawPrize(Price prize,string Random,string Number)
        {
            if (prize.TypeOfPrize == null)
            {
                ModelState.AddModelError("message","You need to select the prize type");
                return View();
            }
            if (prize.TypeOfPrize.Equals("1"))
            {
                if (GrandPirze() != "noValues")
                {
                    var firstwinner =GrandPirze();
                    var user = db.winningNumbers.Single(x => x.WinningNo == firstwinner);
                    Price winner = new Price() { TypeOfPrize = "1", Email = user.Email.ToString(), WinningNo = GrandPirze().ToString() };
                    var prizeDetail = db.prices.Where(x => x.TypeOfPrize.Equals(prize.TypeOfPrize)).FirstOrDefault();
                    if (prizeDetail == null)
                    {
                        if (CleanSpace(winner) == true)
                        {
                            return RedirectToAction("DrawPrize");
                        }
                        ModelState.AddModelError("message", "Something Went Wrong");


                    }
                    else
                    {
                        ModelState.AddModelError("TypeOfPrize", "The prize is already draw");
                        return View();
                    }

                }
              
            }
            else if (Random.Equals("0"))
            {
                if (string.IsNullOrEmpty(Number))
                {
                    ModelState.AddModelError("Number", "You have to enter 4 digit number");
                    return View();
                }
                else
                {
                    
                        var number = Number.ToString();
                        var user = db.winningNumbers.Where(x => x.WinningNo == number).FirstOrDefault();
                        if (user == null)
                        {
                            ModelState.AddModelError("Number", "The number is not exist in the database");
                            return View();
                        }
                        else
                        {
                            Price prize1 = new Price() { TypeOfPrize = prize.TypeOfPrize, Email = user.Email, WinningNo = number.ToString() };
                            var prizeDetail = db.prices.Where(x => x.TypeOfPrize.Equals(prize.TypeOfPrize)).FirstOrDefault();
                            if (prizeDetail == null)
                            {
                                if (CleanSpace(prize1) == true)
                                {
                                    return RedirectToAction("DrawPrize");
                                }
                                ModelState.AddModelError("message", "Something Went Wrong");


                            }
                            else
                            {
                                ModelState.AddModelError("TypeOfPrize", "The prize is already draw");
                                return View();
                            }

                        }

                   

                }

            }
            else
            {
                if (RemainingPrize() == "empty")
                {
                    ModelState.AddModelError("TypeOfPrize", "Please select Grand prize and then Others");
                    return View();
                }
                var winnerNumber = RemainingPrize();
                var user = db.winningNumbers.Where(x => x.WinningNo == winnerNumber).FirstOrDefault();
                Price prize1 = new Price { TypeOfPrize = prize.TypeOfPrize, WinningNo = winnerNumber.ToString(), Email = user.Email };
                var prizeDetail = db.prices.Where(x => x.TypeOfPrize.Equals(prize.TypeOfPrize)).FirstOrDefault();
                if (prizeDetail == null)
                {
                    if (CleanSpace(prize1) == true)
                    {
                        return RedirectToAction("DrawPrize");
                    }
                    ModelState.AddModelError("message", "Something Went Wrong");

                }
                else
                {
                    ModelState.AddModelError("TypeOfPrize", "The prize is already draw");
                    return View();
                }


            }
            return RedirectToAction("Winners");
            /*db.prices.Add(data);
            db.SaveChanges();

            return RedirectToAction("Winners");*/

        }

        public ActionResult WinningNumber()
        {

            
            return View(db.winningNumbers.ToList());
        }

        public ActionResult Winners()
        {


            return View(db.prices.ToList());
        }


        private bool CleanSpace(Price prize)
        {
            db.prices.Add(prize);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private string GrandPirze()
        {
            Random r = new Random();
            var lists = new List<string>();
            var grandPrizeWinners = db.winningNumbers.GroupBy(x => x.Email).OrderByDescending(x => x.Count()).Take(2);
            if (grandPrizeWinners != null)
            {
                foreach (var group in grandPrizeWinners)
                {
                    foreach (var data in group)
                    {
                        lists.Add(data.WinningNo);
                    }
                }

                var randomList = lists.OrderBy(x => r.Next(lists.Count));
                var index = r.Next(randomList.ToList().Count);
                return randomList.ToList()[index];
            }
            return "noValues";
        }

        private string RemainingPrize()
        {
            List<string> getAllWinningNumbers = new List<string>();
            List<string> getWinnerNumbersList = new List<string>();
            List<SelectListItem> remainingNumber = new List<SelectListItem>();
            foreach (var winningNumber in db.winningNumbers.ToList())
            {
                getAllWinningNumbers.Add(winningNumber.WinningNo);
            }

            var winnerList = db.prices.ToList();
            if (winnerList == null)
            {
                return "empty";
            }
            else
            {
                foreach (var winner in winnerList)
                {
                    var winnerWinningNumbers = db.winningNumbers.Where(x => x.Email.Equals(winner.Email)).ToList();
                    foreach (var winnerWinningNumber in winnerWinningNumbers)
                    {
                        getWinnerNumbersList.Add(winnerWinningNumber.WinningNo);
                    }
                }
            }

            foreach (var item in getWinnerNumbersList)
            {
                getAllWinningNumbers.Remove(item);
            }

            foreach (var item in getAllWinningNumbers)
            {
                remainingNumber.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.remainingNumber = remainingNumber;



            return getPrize(getAllWinningNumbers);
        }

        private string getPrize(List<string> getAllWinningNumbers)
        {
            Random r = new Random();

            return getAllWinningNumbers.OrderBy(x => r.Next(getAllWinningNumbers.Count)).ToList()[r.Next(getAllWinningNumbers.ToList().Count)];
        }

    }
}