using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofFreePayCancelTrans
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transactionid { get; set; }

        [Required]
        public string reasonoffailed { get; set; }
    }
}