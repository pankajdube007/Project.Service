using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DcrCatExe
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DcrCatExeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DcrCatExeFinalList> data { get; set; }
    }

    public class DcrCatExeList
    {
        public int catid { get; set; }
        public string partycatnm { get; set; }
    }

    public class AreaListOrganationList
    {
        public int areaid { get; set; }
        public string areanm { get; set; }
    }

    public class DcrCatExeFinalList
    {
        public List<DcrCatExeList> catdata { get; set; }
        public List<AreaListOrganationList> areadata { get; set; }
    }
}