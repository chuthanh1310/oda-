using oda_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace oda_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly database _database = new database();

        public ActionResult Index()
        {
            try
            {
                var contents = _database._dbcontext.iw_diemngap.ToList();
                return View(contents);
            }
            catch (Exception ex)
            {
                // Log the inner exception details
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                // Optionally, add logging to a file or logging system

                // Provide a user-friendly error message
                ViewBag.ErrorMessage = "An error occurred while fetching data.";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public JsonResult Gettoado(string PostData)
        {
            var result = 0;

            return Json(result);
        }
    }
}
