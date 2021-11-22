using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class AllMaster
    {
        // [Required(ErrorMessage ="Please Input Usrer Name",AllowEmptyStrings =false)]
        [Range(1, int.MaxValue, ErrorMessage = "Please Input Usrer ID")]
        public int userid { get; set; }

        //[Range(1, DateTime.Now, ErrorMessage = "Please Input Usrer Name")]
        [DateRange]
        public DateTime lastdate { get; set; }

        [Required]
        public string uniquekey { get; set; }

        public string lat { get; set; }
        public string longi { get; set; }
        public DateTime? latlongtmdt { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string remarks { get; set; }

        [Range(1, 3, ErrorMessage = "Please Input Flag valid range")]
        public int flag { get; set; }
    }

    public class DumpMasterData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<OrgName> data { get; set; }
    }

    public class OrgName
    {
        public int slno { get; set; }
        public string compname { get; set; }
        public int categoryid { get; set; }
        public bool flag { get; set; }
        public List<OrgAddr> Addr { get; set; }
    }

    public class OrgAddr
    {
        public int slno { get; set; }
        public string address { get; set; }
        public int areaid { get; set; }
        public List<OrgContact> Cont { get; set; }
    }

    public class OrgContact
    {
        public int slno { get; set; }

        //public int orgnizationid { get; set; }
        //public int orgaddmastid { get; set; }
        public string contactperson { get; set; }
    }

    public class Urls
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public Url data { get; set; }
    }

    public class Url
    {
        public string urldnld { get; set; }
    }
}