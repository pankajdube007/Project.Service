using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAddLatLonexecaddhome
    {
        [Required]
        public int ExecId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string homelat { get; set; }
        [Required]
        public string homelan { get; set; }
        [Required]
        public string homeadd { get; set; }

    }

    public class AddLatLonexecaddhomes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddLatLonexecaddhome> data { get; set; }
    }

    public class AddLatLonexecaddhome
    {
        public string output { get; set; }
    }
}