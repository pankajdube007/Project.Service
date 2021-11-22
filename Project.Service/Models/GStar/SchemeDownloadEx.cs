using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofSchemeDownloadEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class SchemeDownloadExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeDownloadEx> data { get; set; }
    }

    public class SchemeDownloadEx
    {
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string withamturl { get; set; }
        public string withoutamturl { get; set; }
    }
}