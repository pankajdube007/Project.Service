using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListAllAssignToVendor
    {

        [Required]
        public string VendorID { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ListVendorAll
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorDataAll> data { get; set; }

    }

    public class VendorDataAll
    {

        public string itemName { get; set; }
        public string ItemId { get; set; }
        public string qrcode { get; set; }
        public string qrslno { get; set; }
        public string headid { get; set; }
        public List<ProblemListDataAll> Issue { get; set; }
    }

    public class ProblemListDataAll
    {
        public string problemid { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }


    }

}