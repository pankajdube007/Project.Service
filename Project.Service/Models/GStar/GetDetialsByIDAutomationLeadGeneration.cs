using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class GetListofDetialsByIDAutomationLeadGeneration
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string SlNo { get; set; }

        //public string FromDate { get; set; }

        //public string ToDate { get; set; }
    }

    public class GetDetialsByIDAutomationLeadGenerationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetDetialsByIDAutomationLeadGenerationList> data { get; set; }
    }

    public class GetDetialsByIDAutomationLeadGenerationList
    {
        public string SlNo { get; set; }

        public string Reference_NO { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Mob_No { get; set; }

        public string FullAddress { get; set; }
        public string ItemNames { get; set; }
        public string CategoryNames { get; set; }
        public string Remark { get; set; }
        public string ApprovalStatus { get; set; }
        public string Involve_Architech { get; set; }
        public string Architech_Name { get; set; }
        public string Architech_No { get; set; }
        public string RequestDate { get; set; }
        public string Available_dt { get; set; }
        public string Available_time { get; set; }
        public string QuotationNo { get; set; }
        public string SalesExName { get; set; }
        public string EmpCode { get; set; }
    }
}