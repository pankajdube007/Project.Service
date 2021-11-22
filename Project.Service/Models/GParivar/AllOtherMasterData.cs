using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ContactModeDetails
    {
        public string result { get; set; }
        public string message { get; set; }
        public List<ContactModeDetail> data { get; set; }
    }

    public class ContactModeDetail
    {
        public int Val { get; set; }
        public string Txt { get; set; }
        public bool flag { get; set; }
    }

    public class DumpOtherMasterData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<DumpOtherMasterDatas> data { get; set; }
    }

    public class DumpOtherMasterDatas
    {
        public List<ContactModeDetail> Category { get; set; }
        public List<ContactModeDetail> Product { get; set; }
        public List<ContactModeDetail> ContactMode { get; set; }
        public List<ContactModeDetail> Purpose { get; set; }
        public List<Areamast> Area { get; set; }
        public List<Citymast> City { get; set; }
        public List<Statemast> State { get; set; }
        public List<Countrymast> Country { get; set; }
    }

    public class Areamast
    {
        public int slno { get; set; }
        public string areaname { get; set; }
        public int cityid { get; set; }
        public bool flag { get; set; }
        //public int stateid { get; set; }
        //public int countryid { get; set; }
    }

    public class Citymast
    {
        public int slno { get; set; }
        public string cityname { get; set; }
        public int stateid { get; set; }
        public bool flag { get; set; }
        //public int countryid { get; set; }
    }

    public class Statemast
    {
        public int slno { get; set; }
        public string statename { get; set; }
        public int countryid { get; set; }
        public bool flag { get; set; }
    }

    public class Countrymast
    {
        public int slno { get; set; }
        public string countryname { get; set; }
        public bool flag { get; set; }
    }

    public class OtherAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please Input Usrer ID")]
        public int userid { get; set; }

        [DateRange]//(Convert.ToDateTime("2015-04-01"),DateTime.Now)]
        public DateTime lastsyncdates { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

  
}