using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofDCRExecutiveSchedule
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int SlNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public DateTime dcrdate { get; set; }

        [Required]
        public string dcrtime { get; set; }

        [Required]
        public int modeid { get; set; }

        [Required]
        public int catid { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int addressid { get; set; }

    
        public string contactperson { get; set; }

        [Required]
        public string purposeid { get; set; }

        [Required]
        public string productid { get; set; }

        [Required]
        public string priority { get; set; }

        public string reason { get; set; }
        public string remark { get; set; }
    }
}