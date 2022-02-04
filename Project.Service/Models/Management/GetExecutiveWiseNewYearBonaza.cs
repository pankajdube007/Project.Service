﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class GetExecutiveWiseNewYearBonaza
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

       
        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class ExecutiveWiseNewYearBonaza
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutiveWiseNewYearBonazaFinal> data { get; set; }
    }

    public class ExecutiveWiseNewYearBonazaFinal
    {
        public List<ExecutiveWiseNewYearBonazas> NewSchemeYearBonaza { get; set; }
        public bool ismore { get; set; }
    }

    public class ExecutiveWiseNewYearBonazas
    {
        public string Party { get; set; }
        public string mobile { get; set; }
        public string areanm { get; set; }
        public string cin { get; set; }
        public string salesexname { get; set; }
        public decimal WD { get; set; }
        public decimal WDPOINTS { get; set; }
        public decimal LI { get; set; }
        public decimal LIPOINTS { get; set; }
        public decimal mcb { get; set; }
        public decimal mcbPOINTS { get; set; }
        public decimal WC { get; set; }
        public decimal WCPOINTS { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalPOINTS { get; set; }
        public string CurrentSlab { get; set; }
        public string nextSlab { get; set; }


    }
}