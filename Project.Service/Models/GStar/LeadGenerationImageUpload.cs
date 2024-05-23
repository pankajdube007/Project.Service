using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class LeadGenerationImageUpload
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string LeadId { get; set; }
        
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Images { get; set; }
    }
    public class LeadGenerationImageUploadList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LeadGenerationImageUploadLists> data { get; set; }
    }
    public class LeadGenerationImageUploadLists
    {
        public string output { get; set; }
    }
}