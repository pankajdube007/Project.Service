﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    

    public class ListofBranchWiseCombo
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ComboId { get; set; }

    }

    public class BranchWiseCombos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseCombo> data { get; set; }
    }

    public class BranchWiseCombo
    {
        public string branchid { get; set; }
        public string branchname { get; set; }
        public string Count { get; set; }
        public string used { get; set; }

        public string ComboIds { get; set; }

    }


}