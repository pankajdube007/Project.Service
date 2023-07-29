using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{

    public class InsertAutomationLeadGenerationStatusUpdate
    {
        [Required]
        public string Cin { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Remark { get; set; }

        [Required]
        public string Project_name { get; set; }

        public string FollowUpDate { get; set; }

        [Required]
        public int SlNo { get; set; }
    }

    public class AddAutomationLeadGenerationStatusUpdates
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddAutomationLeadGenerationStatusUpdate> data { get; set; }
    }
    public class AddAutomationLeadGenerationStatusUpdate
    {
        public string output { get; set; }
    }
}