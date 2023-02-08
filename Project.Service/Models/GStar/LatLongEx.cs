using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofLatLongEx
    {
        [Required]
        public int ExId { get; set; }

        public int EmpType { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Lat { get; set; }

        [Required]
        public string Long { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Distance { get; set; }

        [Required]
        public string BatteryStraingth { get; set; }

        [Required]
        public string SignalStraingth { get; set; }

        [Required]
        public string DevoiceIsOnline { get; set; }

        [Required]
        public string Speed { get; set; }

    }

    public class LatLongExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LatLongEx> data { get; set; }
    }

    public class LatLongEx
    {
        public int totalcount { get; set; }
        public string LastInserted { get; set; }
    }

    public class ListofGetLatLongEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string FromTime { get; set; }

        [Required]
        public string ToTime { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class GetLatLongExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetLatLongEx> data { get; set; }
    }

    //public class GetLatLongExFinal
    //{
    //    public List<GetLatLongEx> latdata { get; set; }
    //    public string Address { get; set; }
    //}


    public class GetLatLongEx
    {
        public string lat { get; set; }
        public string Long { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string WorkAddress { get; set; }

    }
}