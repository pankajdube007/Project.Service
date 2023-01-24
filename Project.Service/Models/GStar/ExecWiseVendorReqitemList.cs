using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ExecWiseVendorReqitemList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ExId { get; set; }

        [Required]
        public string slno { get; set; }
    }

    public class ExecWiseVendorReqitemLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecWiseVendorReqitemList1> data { get; set; }
    }

    public class ExecWiseVendorReqitemList1
    {
        public string ProductCode { get; set; }
        public string slno { get; set; }
        public string RefID { get; set; }
        public string status { get; set; }
        public string inspectiondate { get; set; }
        public string files { get; set; }
        public string remark { get; set; }
        public string isedit { get; set; }

    }
}