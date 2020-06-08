using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofNEWDEALERAPPOINTMENTREPORT
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class NEWDEALERAPPOINTMENTREPORTLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NEWDEALERAPPOINTMENTREPORTList> data { get; set; }
    }

    public class NEWDEALERAPPOINTMENTREPORTList
    {
        public string Branch { get; set; }
        public string Date { get; set; }
        public string Count { get; set; }
        public string branchid { get; set; }
        public string month { get; set; }
        public string year { get; set; }


    }
}