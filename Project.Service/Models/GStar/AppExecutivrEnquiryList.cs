using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class AppExecutivrEnquiryList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int SlNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class ExecutivrEnquiryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutivrEnquiryList> data { get; set; }
    }

    public class ExecutivrEnquiryList
    {
       
        public string SalesExecutiveName { get; set; }
        public string DealerName { get; set; }
        public string SubjectName { get; set; }
        public string EnquiryNumber { get; set; }
        public string EnquiryText { get; set; }
        public string Date { get; set; }
        public string EnquiryStatus { get; set; }
       
    }
}