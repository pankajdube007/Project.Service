using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class listsofFreePaySucess
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transactionid { get; set; }

        public string freepaytransactionid { get; set; }

        public string reasonoffailed { get; set; }

        public string statuscode { get; set; }
        [Required]
        public string devicetype { get; set; }
        [Required]
        public string deviceid { get; set; }
    }
}