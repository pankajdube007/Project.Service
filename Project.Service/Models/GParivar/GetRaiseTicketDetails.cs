using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    
    public class ListofRaiseTicketDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
       
        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        public string Search { get; set; }
    }

    public class GetRaiseTicketDetailLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetRaiseTicketDetailList> data { get; set; }
    }

    public class GetRaiseTicketDetailList
    {
        public string TktNo { get; set; }
        public string TktStatus { get; set; }
        public string Tktdt { get; set; }
        public string CustContactNo { get; set; }
        public string FullAddress { get; set; }
        
    }
}