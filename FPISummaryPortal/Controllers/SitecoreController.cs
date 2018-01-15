using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using FPISummaryPortal.Models;

namespace FPISummaryPortal.Controllers
{
    public class SitecoreController : Controller
    {

        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(SitecoreController));

        //Set the Connection 
        FPI_Dev_DatabaseEntities1 sc = new FPI_Dev_DatabaseEntities1();

        // GET: Sitecore
        public ActionResult Index()
        {
            return View();
        }

        //Closing Database Connections
        protected override void Dispose(bool disposing)
        {
            sc.Dispose();
            base.Dispose(disposing);
        }
    }
}