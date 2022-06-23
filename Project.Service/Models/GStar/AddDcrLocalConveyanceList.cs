using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class ListofAddDcrLocalConveyance
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string trvldt { get; set; }

        [Required]
        public string grossdistance { get; set; }

        [Required]
        public string claimabledistance { get; set; }

        [Required]
        public string personaltravl { get; set; }

        [Required]
        public string odomtrkm { get; set; }

        [Required]
        public int trvlmodeq { get; set; }

        [Required]
        public string claimableamt { get; set; }

        [Required]
        public string self { get; set; }

        [Required]
        public string train { get; set; }

        [Required]
        public string metro { get; set; }

        [Required]
        public string rentalcar { get; set; }

        [Required]
        public string bus { get; set; }

        [Required]
        public string auto { get; set; }

        [Required]
        public string tollparking { get; set; }

        [Required]
        public string totalpayble { get; set; }

        [Required]
        public Boolean IsClaimable { get; set; }

    }

    public class AddDcrLocalConveyanceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddDcrLocalConveyanceList> data { get; set; }
    }

    public class AddDcrLocalConveyanceList
    {
        public string output { get; set; }
    }
}