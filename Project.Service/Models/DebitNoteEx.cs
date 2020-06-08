﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDebitNoteEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public string FinYear { get; set; }

        public string searchtxt { get; set; }

        [Required]
        public int ReportValue { get; set; }

        [Required]
        public int ReportType { get; set; }
    }

    public class DebitNoteExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DebitNoteEx> data { get; set; }
    }

    public class DebitNoteEx
    {
        public string partynm { get; set; }
        public string referenceno { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string ledgerdec { get; set; }
        public string url { get; set; }
    }
}