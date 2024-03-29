﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class TotalTodayInvoiceAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class TotalTodayInvoices
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TotalTodayInvoice> data { get; set; }
    }

    public class TotalTodayInvoice
    {
        public string totalamt { get; set; }
    }
}