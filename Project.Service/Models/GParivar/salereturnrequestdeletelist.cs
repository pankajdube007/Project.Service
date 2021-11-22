using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class salereturnrequestdeletelist
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
    }

    public class salereturnrequestdeletes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public object data { get; set; }
    }
}