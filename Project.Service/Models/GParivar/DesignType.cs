using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListDesignType
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserCat { get; set; }

    }
    public class DesignTypeLists
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DesignTypeList> data { get; set; }
    }

    public class DesignTypeList
    {
        public int Slno { get; set; }
        public string Name { get; set; }
     

    }
}