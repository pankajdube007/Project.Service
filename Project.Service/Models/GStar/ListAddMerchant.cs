using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    

    public class ListofAddMerchant
    {
        [Required]
        public int ExId { get; set; }
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string suppliername { get; set; }

        [Required]
        public string gstno { get; set; }

        [Required]
        public string addline1 { get; set; }

        [Required]
        public string addline2 { get; set; }

        [Required]
        public int cityid { get; set; }

        [Required]
        public int areaid { get; set; }

        [Required]
        public int stateid { get; set; }

        [Required]
        public int countryid { get; set; }

        [Required]
        public string pinno { get; set; }

        [Required]
        public string ConcernedPerson { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string mobile { get; set; }

        [Required]
        public int merchanttype { get; set; }
        
        public string gstcertificate { get; set; }

        public string bankdetails { get; set; }

    }

    public class AddMerchants
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddMerchant> data { get; set; }

    }

    public class AddMerchant
    {
        public string output { get; set; }
    }

}