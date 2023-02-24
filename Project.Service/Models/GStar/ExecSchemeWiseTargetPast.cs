using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models.GStar
{
    public class ListExecSchemeWiseTargetPast
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExecSchemeWiseTargetPast
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecSchemeWiseTargetsPast> data { get; set; }
    }

    public class ExecSchemeWiseTargetsPast
    {
        public string Salesexnm { get; set; }
        public string Q { get; set; }
        public string Execid { get; set; }
        public string SchemeName { get; set; }
        public string SchemeId { get; set; }
        public string TotalTarget { get; set; }
        public string SalesQty { get; set; }
        public string EarnAmount { get; set; }
        public string ShortFall { get; set; }
        public string Division { get; set; }
        public string Growth { get; set; }
    }
}