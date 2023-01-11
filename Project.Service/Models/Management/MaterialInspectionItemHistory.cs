using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Management
{
    public class MaterialInspectionItemHistory
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int Vendorid { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string slno { get; set; }
    }

    public class MaterialInspectionItemHistoryes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MaterialInspectionItemHistorye> data { get; set; }
    }

    public class MaterialInspectionItemHistorye
    {
        public string ProductCode { get; set; }
        public string Status { get; set; }
       

    }
}