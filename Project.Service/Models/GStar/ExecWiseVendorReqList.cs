using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ExecWiseVendorReqList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExecWiseVendorReqLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecWiseVendorReqLists1> data { get; set; }
    }

    public class ExecWiseVendorReqLists1
    {
        public string ReferenceNo { get; set; }
        public string slno { get; set; }
        public string vendorname { get; set; }
        public string inspectiondate { get; set; }
        public string status { get; set; }
        public string vaddress { get; set; }
        public string vcontact { get; set; }
    }

}