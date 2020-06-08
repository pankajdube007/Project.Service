using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAddLatLonexecadd
    {
        [Required]
        public int ExecId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string officelat { get; set; }
        [Required]
        public string officelan { get; set; }
        [Required]
        public string officeadd { get; set; }
      
    }

    public class AddLatLonexecadds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddLatLonexecadd> data { get; set; }
    }

    public class AddLatLonexecadd
    {
        public string output { get; set; }
    }
}