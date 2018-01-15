using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPISummaryPortal.Models;
using log4net;
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
    public class iICEStagingController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(iICEStagingController));
        
        //Set the Connection 
        FPI_Dev_DatabaseEntities1 db = new FPI_Dev_DatabaseEntities1();

        //Passing Empty Model to Bind to the View
        public ActionResult iICE_Staging()
        {
            try
            {
                iICE_Staging model = new iICE_Staging();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "iICE_Staging"));
            }

        }

        //Passing Empty Model to Bind to the View
        public ActionResult ProductInfo()
        {
            try
            {
                ProductInfo mod = new ProductInfo();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ProductInfo"));
            }

        }

        //Passing Empty Model to Bind to the View
        public ActionResult SearchDate()
        {
            try
            {
                SearchDate mo = new SearchDate();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "SearchDate"));
            }

        }


        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "Index"));
            }
        }

        [HttpGet]
        public ActionResult ListProducts()
        {
            try
            {
                ViewBag.Message = "Listing of Products from iICE Staging Environment - Picked up by FPI Process and Sent to Sitecore";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ListProducts"));
            }
           
        }

        [HttpGet]
        public ActionResult ListProductDetails()
        {
            try
            {
                ViewBag.Message = "Product Details - iICE Staging Environment ";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ListProductDetails"));
            }

        }

        [HttpGet]
        public ActionResult SearchProduct()
        {
            try
            {
                ViewBag.Message = "Search Product to check whether the Product in iICE Staging Environment";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "SearchProduct"));
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
                    var itemcoderesults = ((from m in db.iICE_Staging
                                            where m.ItemCode == itemcode
                                            select m).ToList());


                    if (itemcoderesults.Count() > 0)
                    {
                        iICE_Staging pdm = new iICE_Staging();

                        return Json("Found", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json("Not Found", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("Error! - No ItemCode");

                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "SearchProduct"));
            }


        }

        [HttpGet]
        public ActionResult SearchResults()
        {
            try
            {
                ViewBag.Message = "Product Search Results - iICE Environment";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "SearchResults"));
            }

        }

        //Closing Database Connections
        protected override void Dispose(bool disposing)
        {
            try
            {
                db.Dispose();
                base.Dispose(disposing);
            }
            catch (Exception e)
            {
                Log.Error("Exception caught: {0}", e);
            }

        }

        [HttpGet]
        public ActionResult ShowProductListing()
        {
            try
            {
                ViewBag.Message = "Show Product Details by Date - iICE Environment ";
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ShowProductListing"));
            }
        }

        [HttpPost]
        public ActionResult ShowProductListing(Models.SearchDate dt)
        {
            try
            {
                Session["ShowResults"] = null;
                string st = dt.providedSearchDate.ToShortDateString();

                if (string.IsNullOrEmpty(st) == false)
                {
                    ViewBag.Message = "Show Products Details by Date - iICE Environment";
                    ViewBag.Date = st;
                    //check if there are products for that date
                    var foundProductsiICE = (from m in db.iICE_Staging
                                             where DbFunctions.TruncateTime(m.TransferredDate) == DbFunctions.TruncateTime(dt.providedSearchDate)
                                             select m).ToList();
                    //count
                    int countOFoundProductsiICE = foundProductsiICE.Count();

                    if (countOFoundProductsiICE > 0)
                    {
                        Session["ShowResults"] = true;
                        ViewBag.CounteriICE = countOFoundProductsiICE;
                        ViewBag.productsiice = "There were" + " " + ViewBag.CounteriICE + " " + "Products Sent From iice on the Provided Date:" + " " + ViewBag.Date;
                        return View(foundProductsiICE);
                    }
                    else
                    {
                        Session["ShowResults"] = null;
                        ViewBag.CounteriICE = countOFoundProductsiICE;
                        ViewBag.productsiice = "There were" + " " + ViewBag.CounteriICE + " " + "Products Sent From iice on the Provided Date" + " " + ViewBag.Date;
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
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ShowProductListing"));
            }


        }

        [HttpGet]
        public ActionResult ShowProductDetails(int? id)
        {
            string brandName = "LE";
            try
            {
                Session["ShowResultsDetailsiICE"] = null;
                ViewBag.Message = "Products Detailed Listing - iICE Staging Environment ";
                ViewBag.NotificationMessage = "No Results Yet";
                //SearchDetails based on the 
                var productdetailiICE = (from m in db.iICE_Staging
                                         where m.ID.CompareTo(id.Value) == 0 
                                         select m).ToList();
                int countofProductdetailiICE = productdetailiICE.Count();
                if (countofProductdetailiICE > 0)
                {
                    Session["ShowResultsDetailsiICE"] = true;
                    ViewBag.NotificationMessage = "Displaying Results Successfully";
                    return View(productdetailiICE);
                }
                else
                {
                    Session["ShowResultsDetailsiICE"] = null;
                    ViewBag.NotificationMessage = "No Details Found";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ShowProdutDetails"));
            }


        }

        [HttpGet]
        public ActionResult ShowProductDetailListing()
        {
            try
            {
                Session["ShowProductDetailListing"] = null;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ShowProductDetailListing"));
            }


        }

        [HttpPost]
        public ActionResult ShowProductDetailListing(Models.ProductInfo pc)
        {
            try
            {
                Session["ShowProductDetailListing"] = null;
                string pcodesearch = pc.productCode.ToString();

                if (String.IsNullOrEmpty(pcodesearch)==false)
                {
                    
                    var pCodeSearchList = ((from e in db.iICE_Staging
                                          where e.ItemCode == pcodesearch
                                          select e)).ToList();

                    int counterFoundProducts = pCodeSearchList.Count();

                    if (counterFoundProducts > 0)
                    {
                        Session["ShowProductDetailListing"] = true;
                        ViewBag.CounteriICE = counterFoundProducts;
                        ViewBag.productsiice = ViewBag.CounteriICE + " " + "Products found in iice on the" + " " + ViewBag.Date;
                        return View(pCodeSearchList);
                    }
                    else
                    {
                        Session["ShowProductDetailListing"] = null;
                        ViewBag.CounteriICE = counterFoundProducts;
                        ViewBag.productsiice = ViewBag.CounteriICE + " " + "Products found in iice on the" + " " + ViewBag.Date;
                        return View();
                    }
                }
                else
                {
                    Session["ShowProductDetailListing"] = null;
                    return View();
                }

                
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "iICEStaging", "ShowProductDetailListing"));
            }


        }




    }
}