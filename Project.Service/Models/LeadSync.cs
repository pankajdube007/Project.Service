using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LeadSyncAction
    {
        [DateRange]
        public DateTime dcrdate { get; set; }

        public double? dcrtime { get; set; }

        [Required]
        public string duration { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input mode within the range")]
        public int modeid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input party cat within the range")]
        public int partycategoryid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input org within the range")]
        public int organizationid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input addr within the range")]
        public int addressid { get; set; }

        [Required]
        public string cp { get; set; }

        [Required]
        public string purposeid { get; set; }

        [Required]
        public string productcategoryid { get; set; }

        [Required]
        public string dcrpriority { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please Input ref cat within the range")]
        public int refcat { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please Input ref within the range")]
        public int refid { get; set; }

        [Required]
        public string remark { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input user ID")]
        public int userid { get; set; }

        [DateRange]
        public DateTime lastsyncdt { get; set; }

        [Required]
        public string uniquekey { get; set; }

        [Range(1, 2, ErrorMessage = "Please Input flag within the range")]
        public int flag { get; set; }
    }

    public class DcrDetailsByUser
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DcrDetailsByUsers> data { get; set; }
    }

    public class DcrDetailsByUsers
    {
        public int slno { get; set; }
        public string excutivenm { get; set; }
        public string dcrdate { get; set; }
        public string dcrtime { get; set; }

        // public string stat { get; set; }
        public string duration { get; set; }

        public string remark { get; set; }
        public string contactmode { get; set; }
        public string partycat { get; set; }
        public string orgnm { get; set; }
        public string addr { get; set; }
        public string purpose { get; set; }
        public string name { get; set; }
        public string priority { get; set; }
        public string product { get; set; }
        public string areanm { get; set; }
        public string citynm { get; set; }
    }
}