﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSpinAmountConf
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string SlNo { get; set; }

        [Required]
        public string prizemoney { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SpinAmountConfs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SpinAmountConf> data { get; set; }
    }

    public class SpinAmountConf
    {
        public int SlNo { get; set; }
        public int amount { get; set; }
    }
}