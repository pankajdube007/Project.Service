using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListOfVendorprofile
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int vendorID { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string Cat { get; set; }

    }

    public class GetVendorprofileDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetVendorprofileDetail> data { get; set; }
    }

    public class GetVendorprofileDetail
    {
        public string email { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string dealnm { get; set; }
        public string addline1 { get; set; }
        public string panno { get; set; }
        public string GSTNo { get; set; }
        public string countrynm { get; set; }
        public string statenm { get; set; }
        public string citynm { get; set; }
        public string areanm { get; set; }



    }
}