using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Listsalereturnrequestfinalize
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int salereturnrequestid { get; set; }
    }

    public class salereturnrequestfinalizeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<salereturnrequestfinalizeList> data { get; set; }
    }

    public class salereturnrequestfinalizeList
    {
        public string output { get; set; }
    }
}