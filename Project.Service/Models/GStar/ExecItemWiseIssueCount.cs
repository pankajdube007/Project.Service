using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListExecItemWiseIssueCount
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class ExecItemWiseIssueCount
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecItemWiseIssueCounts> data { get; set; }
    }

    public class ExecItemWiseIssueCounts
    {
        public int Issueid { get; set; }

        public string IssueName { get; set; }

        public int ExeId { get; set; }

        public string ExecutiveName { get; set; }

        public string ItemName { get; set; }

        public int Issuecount { get; set; }


    }
}