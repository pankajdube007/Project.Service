using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
    public class ListofExecWiseCost
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Branch { get; set; }

    }

    public class ExecWiseCostLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecWiseCostList> data { get; set; }
    }

    public class ExecWiseCostList
    {
        public string slno { get; set; }
        public string salesexnm { get; set; }
        public string JoinDate { get; set; }
        public string cost { get; set; }
        public string expense { get; set; }
        public string lastyearsale { get; set; }
        public string Target { get; set; }
        public string BranchName { get; set; }

    }
}