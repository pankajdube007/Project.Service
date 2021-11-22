using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSchemeGrowth
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string SchemeId { get; set; }
    }
    public class SchemeGrowthHead
    {
        public string SchemeName { get; set; }
        public string BaseQty { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Division { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Branch { get; set; }
    }

    public class SchemeGrowthChild
    {
        public string GrowthPercet { get; set; }
        public string Amount { get; set; }
     
    }

    public class SchemeGrowth
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeGrowthFinal> data { get; set; }
    }

    public class SchemeGrowthFinal
    {
        public List<SchemeGrowthHead> head { get; set; }
        public List<SchemeGrowthChild> child { get; set; }
    }

}