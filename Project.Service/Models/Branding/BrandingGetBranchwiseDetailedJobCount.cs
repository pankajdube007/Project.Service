using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListBrandingGetBranchwiseDetailedJobCount
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string BranchId { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class BrandingGetBranchwiseDetailedJobCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BrandingGetBranchwiseDetailedJobCountList> data { get; set; }
    }
    public class BrandingGetBranchwiseDetailedJobCountList
    {
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string Total { get; set; }
        public string Pending { get; set; }
        public string Approved { get; set; }
        public string DisApproved { get; set; }
        public string Cancel { get; set; }
        public string Assigned { get; set; }
        public string DesignComplete { get; set; }
        public string DesignApprovalPending { get; set; }
        public string DesignApproved { get; set; }
        public string DesignDisApproved { get; set; }
        public string SentToPrinter { get; set; }
        public string SentByPrinter { get; set; }
        public string PrinterWorkApproved { get; set; }
        public string SentToFabricator { get; set; }
        public string SentByFabricator { get; set; }
        public string FabricatorWorkApproved { get; set; }
        public string FinalAssembly { get; set; }
        public string ClosedJobs { get; set; }


    }
}