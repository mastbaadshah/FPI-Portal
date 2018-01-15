using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FPISummaryPortal.Models
{
    public class ProductInfo
    {
        
        [Required(ErrorMessage = "A Product Code is required")]
        [DisplayName("Product Code")]
        public string productCode { get; set; }
       

    }
}