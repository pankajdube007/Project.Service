using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListSetAadharNoMapping
    {


        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string AadharCardNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PancardNo { get; set; }

        [Required]
        public Boolean NameAsPerPancard { get; set; }

    }

    public class AddListSetAadharNoMappingDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddListSetAadharNoMappingDetail> data { get; set; }
    }

    public class AddListSetAadharNoMappingDetail
    {
        public string output { get; set; }
    }
}