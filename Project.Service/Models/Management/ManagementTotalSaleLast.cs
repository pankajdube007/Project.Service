using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofManagementTotalSaleLast
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime CurFromDate { get; set; }

        [Required]
        public DateTime CurToDate { get; set; }


        [Required]
        public DateTime LastFromDate { get; set; }

        [Required]
        public DateTime LastToDate { get; set; }
    }

    public class ManagementTotalSaleLasts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementTotalSaleLast> data { get; set; }
    }

    public class ManagementTotalSaleLast
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string curwiringdevices { get; set; }
        public string curlights { get; set; }
        public string curwireandcable { get; set; }
        public string curpipesandfittings { get; set; }
        public string curmcbanddbs { get; set; }
        public string curfan { get; set; }
        public string curhealthcare { get; set; }
        public string curtotalsale { get; set; }
        public string curbranchcontribution { get; set; }
        public string curbranchcontributionpercentage { get; set; }
        public string lastwiringdevices { get; set; }
        public string lastlights { get; set; }
        public string lastwireandcable { get; set; }
        public string lastpipesandfittings { get; set; }
        public string lastmcbanddbs { get; set; }
        public string lastfan { get; set; }
        public string lasthealthcare { get; set; }
        public string lasttotalsale { get; set; }
        public string lastbranchcontribution { get; set; }
        public string lastbranchcontributionpercentage { get; set; }
    }

}