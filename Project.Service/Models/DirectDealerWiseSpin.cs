﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDirectDealerWiseSpin
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DirectDealerWiseSpins
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DirectDealerWiseSpin> data { get; set; }
    }

    public class DirectDealerWiseSpin
    {
        public string NoOfSpin { get; set; }
        public string RemSpin { get; set; }
        public string WinAmt { get; set; }
        //public int SlNo { get; set; }
        //public int NxtDrwAmt { get; set; }
        public bool Active { get; set; }
        public bool ApplyCN { get; set; }
    }
}