using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{

    public class ListUserAprStatusMonthWise
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Cat { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string ApproveStatus { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class UserAprStatusMonthWiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UserAprStatusMonthWiseList> data { get; set; }
    }
    public class UserAprStatusMonthWiseList
    {
        public string Month { get; set; }
        public int Retailer { get; set; }
        public int Electrician { get; set; }
        public int CounterBoy { get; set; }

    }
}