using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ProblemListAssignToVendor 
    {
        [Required]
        public string QrCode { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }

    public class AssignToVendor
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemData> data { get; set; }

    }

    public class ItemData
    {
        public string qrcode { get; set; }
        public string headid { get; set; }
        public string itemName { get; set; }
        public string ItemId { get; set; }
        public string remark { get; set; }
        public List<ProblemData> Issue { get; set; }
    }

    public class ProblemData
    {
        public string problemid { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }

    }


}
