using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDivisionWiseExTarget
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DivisionWiseExTargets
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionWiseExTarget> data { get; set; }
    }

    public class DivisionWiseExTarget
    {
        public int ExId { get; set; }
        public string salesexnm { get; set; }
        public string wiredevice { get; set; }
        public string lights { get; set; }
        public string wireandcable { get; set; }
        public string pipingandfitting { get; set; }
        public string mcbanddcb { get; set; }
    }
}