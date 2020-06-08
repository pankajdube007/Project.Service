using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCategoryWiseEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string DistrictId { get; set; }

        [Required]
        public string AreaId { get; set; }

        [Required]
        public string SubExId { get; set; }

        [Required]
        public string BranchId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class CategoryWiseExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CategoryWiseFinalEx> data { get; set; }
    }

    public class CategoryWiseFinalEx
    {
        public List<CategoryWiseEx> CategoryWisedata { get; set; }
        public List<CategoryWiseTotalEx> Totaldata { get; set; }
    }

    public class CategoryWiseEx
    {
        public string partynm { get; set; }
        public string wiredevice { get; set; }
        public string lights { get; set; }
        public string wireandcable { get; set; }
        public string pipingandfitting { get; set; }
        public string mcbanddcb { get; set; }
    }

    public class CategoryWiseTotalEx
    {
        public string wiredevicetotal { get; set; }
        public string lightstotal { get; set; }
        public string wireandcabletotal { get; set; }
        public string pipingandfittingtotal { get; set; }
        public string mcbanddcbtotal { get; set; }
        public string finaltotal { get; set; }
    }
}