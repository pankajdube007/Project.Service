using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{
  

    public class ListofBranchDivisionWisePending
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int Potype { get; set; }



    }

    public class BranchDivisionWisePendingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchDivisionWisePendingList> data { get; set; }
    }

    public class BranchDivisionWisePendingList
    {
        public string PartyId { get; set; }
        public string Name { get; set; }
        public string Potype { get; set; }
        public string LIGHTS { get; set; }
        public string MCBAndDBS { get; set; }
        public string PIPESAndFITTINGS { get; set; }
        public string WIREAndCABLE { get; set; }
        public string WIRINGDEVICES { get; set; }
        public string FAN { get; set; }

    }
}