using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListCheckFreePayRegister
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CheckFreePayRegisters
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CheckFreePayRegister> data { get; set; }
    }

    public class CheckFreePayRegister
    {
        public bool status { get; set; }
        public string desc { get; set; }
        public bool duesequence { get; set; }
    }
}