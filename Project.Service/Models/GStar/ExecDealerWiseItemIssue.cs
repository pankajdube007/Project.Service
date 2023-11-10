using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ExecDealerWiseItemIssueList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string Cin { get; set; }

        [Required]
        public int Issutype { get; set; }

        public string Remark { get; set; }


    }

    public class ExecDealerWiseItemIssue
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecDealerWiseItemIssues> data { get; set; }
    }

    public class ExecDealerWiseItemIssues
    {
        public string output { get; set; }
    }
}