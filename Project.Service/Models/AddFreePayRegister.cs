using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class AddFreePayRegister
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
    }

    public class FreePayRegisters
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreePayRegister> data { get; set; }
    }

    public class FreePayRegister
    {
        public string output { get; set; }
    }
}