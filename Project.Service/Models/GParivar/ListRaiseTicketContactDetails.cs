using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{

    public class ListofRaiseTicketContactDetails
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string ContactNo { get; set; }
    }

    public class GetRaiseTicketContactDetailLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetRaiseTicketContactDetailList> data { get; set; }
    }

    public class GetRaiseTicketContactDetailList
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string PincodeID { get; set; }
        public string Pincode { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string City { get; set; }
        public string CustomerID { get; set; }
        public string CustUniquekey { get; set; }
        public string statenm { get; set; }
        public string Distrctnm { get; set; }
        
    }
}