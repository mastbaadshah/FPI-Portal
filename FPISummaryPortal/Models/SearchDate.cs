using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FPISummaryPortal.Models
{
    public class SearchDate
    {
        [DisplayName("FPI Run Date")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime providedSearchDate { get; set; }

    }
}