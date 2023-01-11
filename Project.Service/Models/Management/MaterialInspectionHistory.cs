using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Management
{
    public class MaterialInspectionHistory
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int Vendorid { get; set; }

        [Required]
        public string Category { get; set; }

     
    }


    public class MaterialInspectionHistorye
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MaterialInspectionHistoryes> data { get; set; }
    }

    public class MaterialInspectionHistoryes
    {
        public string Slno { get; set; }
        public string ReferenceNo { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Remark { get; set; }
        public string Cnt { get; set; }

    }
}