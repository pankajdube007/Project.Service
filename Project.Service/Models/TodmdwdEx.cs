using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofTodmdwdEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class TodmdwdExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodmdwdEx> data { get; set; }
    }

    public class TodmdwdEx
    {
        public string partynm { get; set; }
        public string groupnm { get; set; }
        public string tod { get; set; }
        public string wd { get; set; }
        public string md { get; set; }
        public string url { get; set; }
    }
}