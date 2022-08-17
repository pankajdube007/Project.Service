using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class ListofListLocalConveyance
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public DateTime fdate { get; set; }

        [Required]
        public DateTime tdate { get; set; }

    }

    public class GetLocalConveyanceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetLocalConveyanceList> data { get; set; }
    }

    public class GetLocalConveyanceList
    {
        public string slno { get; set; }
        public string execid { get; set; }
        public string trvldt { get; set; }
        public string TotalKms { get; set; }
        public string PersonalKms { get; set; }
        public string ClaimableKms { get; set; }
        public string claimableamt { get; set; }
        public string ModeofTravel { get; set; }
        public string SelfConveyance { get; set; }
        public string status { get; set; }
        public string TotalApproved { get; set; }
        public string TotalAmt { get; set; }
        
    }
}