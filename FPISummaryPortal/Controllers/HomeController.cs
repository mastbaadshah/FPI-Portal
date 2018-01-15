using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using FPISummaryPortal.Models;

namespace FPISummaryPortal.Controllers
{
    public class HomeController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        //Set the Connection 
        FPI_Dev_DatabaseEntities1 hm = new FPI_Dev_DatabaseEntities1();


        public ActionResult Index()
        {
            log.Info("Hello Wor");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "FPI Portal Description.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "FPI Portal Contact Team";

            return View();
        }

        public string getGreetingMessage()
        {
            DateTime now = DateTime.Now;
            var greeting = "Good" + ((now.Hour > 12) ? " Evening" : " Day");
            return greeting;
        }

        public DateTime getCurrentDate()
        {
            var current_date = DateTime.Now;

            return current_date;
        }

        //Closing Database Connections
        protected override void Dispose(bool disposing)
        {
            hm.Dispose();
            base.Dispose(disposing);
        }
    }
}