using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{

    public class ListofAddDcrLocalConveyancenew
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

        public string AppRemark { get; set; }

        [Required]
        public int slno { get; set; }

        public string food { get; set; }
        public string outstation { get; set; }
        public string fixamt { get; set; }
        public string samedayamt { get; set; }
        public string isfix { get; set; }

        public string trainimg { get; set; }
        public string metroimg { get; set; }
        public string rentalcarimg { get; set; }
        public string tollparkingimg { get; set; }
        public string autoimg { get; set; }
        public string busimg { get; set; }
        public string outstationimg { get; set; }



    }

    public class AddDcrLocalConveyanceListsnew
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddDcrLocalConveyanceListnew> data { get; set; }
    }

    public class AddDcrLocalConveyanceListnew
    {
        public string output { get; set; }
    }
}