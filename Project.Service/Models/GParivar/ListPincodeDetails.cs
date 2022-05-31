using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    
    public class ListofPincodeDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Pincode { get; set; }
        
    }

    public class PincodeDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PincodeDetailsList> data { get; set; }
    }

    public class PincodeDetailsList
    {
        public string StateID { get; set; }
        public string statenm { get; set; }
        public string DistrictID { get; set; }
        public string Distrctnm { get; set; }
        public string citynm { get; set; }
        
    }
}