using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using log4net;
using FPISummaryPortal.Models;
using System.Globalization;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;

namespace FPISummaryPortal.Controllers
{
    [HandleError]
    [SessionState(SessionStateBehavior.Required)]
    public class HGController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HGController));

        //Set the Connection 
        FPI_Dev_DatabaseEntities1 hg = new FPI_Dev_DatabaseEntities1();

        // GET: HG
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListProducts(int? id)
        {
            ViewBag.Message = "Listing of Products from HG PDM Module - Picked up by FPI Process";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }




            return View();
        }

        [HttpGet]
        public ActionResult ListProductDetails()
        {
            ViewBag.Message = "Product Details - HG PDM Module";
            return View();
        }

        [HttpGet]
        public ActionResult SearchProduct()
        {
            try
            {
                ViewBag.Message = "Quick Product Look Up";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "SearchProduct"));
            }
        }

        [HttpPost]
        public ActionResult SearchProduct(string itemcode)
        {
            try
            {

                if (string.IsNullOrEmpty(itemcode) == false)
                {
                    //Search the Database for the ProductCode
                    var itemcoderesults = ((from m in hg.HG_PDM
                                            where m.ProductCode == itemcode
                                            select m).ToList());


                    if (itemcoderesults.Count() > 0)
                    {
                        HG_PDM pdm = new HG_PDM();

                        return Json("Found in HG", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json("Not Found in HG", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("Error! - No ItemCode");

                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "SearchProduct"));
            }


        }

        //Closing Database Connections
        protected override void Dispose(bool disposing)
        {
            hg.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult SearchByDate()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "SearchByDate"));
            }
        }

        [HttpGet]
        public ActionResult SearchByProductCode()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "SearchByProductCode"));
            }

        }

        [HttpPost]
        public ActionResult SearchByProductCode(Models.ProductInfo Pc)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "SearchByProductCode"));
            }
        }


        [HttpGet]
        public ActionResult ShowDetailsByProductCode()
        {
            try
            {
                ViewBag.Message = "Details By Product Code";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowDetailsByProductCode"));
            }
        }


        [HttpPost]
        public ActionResult ShowDetailsByProductCode(Models.ProductInfo pcode)
        {
            try
            {
                Session["ShowHGProductListing"] = null;
                string pcodesearch = pcode.productCode.ToString();

                if (String.IsNullOrEmpty(pcodesearch) == false)
                {

                    var pCodeSearchList = ((from e in hg.HG_PDM
                                            where e.ProductCode == pcodesearch
                                            select e)).ToList();

                    int counterFoundProducts = pCodeSearchList.Count();

                    if (counterFoundProducts > 0)
                    {
                        Session["ShowHGProductListing"] = true;
                        ViewBag.CounterHG = counterFoundProducts;
                        ViewBag.productsHG = ViewBag.CounterHG + " " + "Products found in FPI-HG for Product Code" + " " + pcodesearch;
                        return View(pCodeSearchList);
                    }
                    else
                    {
                        Session["ShowHGProductListing"] = null;
                        ViewBag.CounterHG = counterFoundProducts;
                        ViewBag.productsHG = ViewBag.CounterHG + " " + "Products found in FPI-HG for Product Code" + " " + pcodesearch;
                        return View();
                    }
                }
                else
                {
                    Session["ShowHGProductListing"] = null;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowDetailsByProductCode"));
            }
        }

        [HttpGet]
        public ActionResult ShowHGProductDetails(int? id)
        {
            string brandNameHG = "LE";
            try
            {
                Session["ShowDetailedListingOfProductsHG"] = null;
                ViewBag.Message = "Product Detailed Listing - Hamilton Grant Environment ";
                ViewBag.NotificationMessage = "No Results Yet";
                //SearchDetails based on the 
                var productdetailHG = (from m in hg.HG_PDM
                                       where m.ID.CompareTo(id.Value) == 0
                                       select m).ToList();
                int countofProductdetailsHG = productdetailHG.Count();
                if (countofProductdetailsHG > 0)
                {
                    Session["ShowDetailedListingOfProductsHG"] = true;
                    ViewBag.NotificationMessage = "Displaying Results Successfully";
                    return View(productdetailHG);
                }
                else
                {
                    Session["ShowDetailedListingOfProductsHG"] = null;
                    ViewBag.NotificationMessage = "No Details Found";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowHGProductDetails"));
            }
        }

        [HttpPost]
        public ActionResult ShowHGProductDetails()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowHGProductDetails"));
            }
        }

        [HttpGet]
        public ActionResult ShowProductDetailsByDate()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowProductDetailsByDate"));
            }
        }

        [HttpPost]
        public ActionResult ShowProductDetailsByDate(Models.SearchDate dt)
        {
            try
            {
                Session["ShowHGResultsByDate"] = null;
                string st = dt.providedSearchDate.ToShortDateString();

                if (string.IsNullOrEmpty(st) == false)
                {
                    ViewBag.Message = "Product Listing - HG Environment ";
                    ViewBag.Date = st;
                    //check if there are products for that date
                    var foundHGProductsByDate = (from m in hg.HG_PDM
                                                 where DbFunctions.TruncateTime(m.TransferredDate) == DbFunctions.TruncateTime(dt.providedSearchDate)
                                                 select m).ToList();
                    //count
                    int countOfFoundHGProductsByDate = foundHGProductsByDate.Count();

                    if (countOfFoundHGProductsByDate > 0)
                    {
                        Session["ShowHGResultsByDate"] = true;
                        ViewBag.CounterHG = countOfFoundHGProductsByDate;
                        ViewBag.productsHG ="There were" + " " + ViewBag.CounterHG + " " + "Products Sent from HG on the Provided Date:" + " " + ViewBag.Date;
                        return View(foundHGProductsByDate);
                    }
                    else
                    {
                        Session["ShowHGResultsByDate"] = null;
                        ViewBag.CounterHG = countOfFoundHGProductsByDate;
                        ViewBag.productsHG ="There were" + " " + ViewBag.CounterHG + " " + "Products Sent from HG on the Provided Date:" + " " + ViewBag.Date;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Something Went Wrong!- Date came through as null";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "HG", "ShowProductDetailsByDate"));
            }

        }

    }
}
