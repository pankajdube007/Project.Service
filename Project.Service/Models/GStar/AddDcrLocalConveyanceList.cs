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

        
        public string personaltravl { get; set; }

        [Required]
        public string odomtrkm { get; set; }

     
        public int trvlmodeq { get; set; }

       
        public string claimableamt { get; set; }

       
        public string self { get; set; }

        
        public string train { get; set; }

        
        public string metro { get; set; }

        
        public string rentalcar { get; set; }

       
        public string bus { get; set; }

       
        public string auto { get; set; }

        
        public string tollparking { get; set; }

        
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