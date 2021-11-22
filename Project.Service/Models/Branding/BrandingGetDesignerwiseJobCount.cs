using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListBrandingGetDesignerwiseJobCount
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class BrandingGetDesignerwiseJobCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BrandingGetDesignerwiseJobCountList> data { get; set; }
    }
    public class BrandingGetDesignerwiseJobCountList
    {
   
        public string DesignerID { get; set; }
    public string DesignerName  { get; set; }
    public string Total { get; set; }
         public string Pending { get; set; }
public string Complete { get; set; }
public string DaysFromToday { get; set; }

    }
}