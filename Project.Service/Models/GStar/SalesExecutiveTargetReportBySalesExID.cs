using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListsofSalesExecutiveTargetReportBySalesExID
    {
        [Required]
        public string FinYear { get; set; }

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SalesExecutiveTargetReportBySalesExIDLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SalesExecutiveTargetReportBySalesExIDList> data { get; set; }
    }

    public class SalesExecutiveTargetReportBySalesExIDList
    {
        public string branchids { get; set; }
        public string locnm { get; set; }
        public string salesexnm { get; set; }
        public string divisionnm { get; set; }
        public string SlNo { get; set; }
        public string CurrentYrsSales { get; set; }
        public string TargetGivenByBranch { get; set; }
        public string TargetAchived { get; set; }
        public string HOTarget { get; set; }
        public string Achived { get; set; }
        public string ShortFall { get; set; }
        public string Q1CurrentYrsSales { get; set; }
        public string Q1TargetGivenByBranch { get; set; }
        public string Q1HOTarget { get; set; }
        public string Q1Achived { get; set; }
        public string Q1ShortFall { get; set; }
        public string Q2CurrentYrsSales { get; set; }
        public string Q2TargetGivenByBranch { get; set; }
        public string Q2HOTarget { get; set; }
        public string Q2Achived { get; set; }
        public string Q2ShortFall { get; set; }
        public string Q3CurrentYrsSales { get; set; }
        public string Q3TargetGivenByBranch { get; set; }
        public string Q3HOTarget { get; set; }
        public string Q3Achived { get; set; }
        public string Q3ShortFall { get; set; }
        public string Q4CurrentYrsSales { get; set; }
        public string Q4TargetGivenByBranch { get; set; }
        public string Q4HOTarget { get; set; }
        public string Q4Achived { get; set; }
        public string Q4ShortFall { get; set; }

    }
}