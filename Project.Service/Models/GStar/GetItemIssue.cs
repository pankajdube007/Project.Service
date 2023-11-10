using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetItemIssueList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class GetItemIssue
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetItemIssues> data { get; set; }
    }
    public class GetItemIssues
    {
        public string SlNo { get; set; }

        public string Issue  { get; set; }
    }
}