using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
   


    public class ListAppDissAppMpr
    {
        [Required]
        public string Cin { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public int MprId { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class AppDissAppMprs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppDissAppMpr> data { get; set; }
    }
    public class AppDissAppMpr
    {
        public string output { get; set; }
    }
}