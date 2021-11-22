using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListBrandingGetLastJobOfDesigner
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string AssignTo { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class BrandingGetLastJobOfDesignerLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BrandingGetLastJobOfDesignerList> data { get; set; }
    }
    public class BrandingGetLastJobOfDesignerList
    {
        public string DesignerID { get; set; }
        public string DesignerName { get; set; }
        public string DaysFromToday { get; set; }

    }
}