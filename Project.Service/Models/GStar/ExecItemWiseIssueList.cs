using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ExecItemWiseIssueListe
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int IssueId { get; set; }
    }
    public class ExecItemWiseIssueList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecItemWiseIssueLists> data { get; set; }
    }
    public class ExecItemWiseIssueLists
    {
        public string PartyName { get; set; }
        public string IssueName { get; set; }
        public string Date { get; set; }
        public string Remark { get; set; }
    }
}