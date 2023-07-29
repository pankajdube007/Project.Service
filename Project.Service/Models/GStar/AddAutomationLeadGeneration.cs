using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    
    public class InsertAutomationLeadGeneration
    {
        [Required]
        public string Cin { get; set; }
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Purpose { get; set; }
        [Required]
        public string CustomerMobileNo { get; set; }
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string EmailID { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }

        [Required]
        public string Pincode { get; set; }
        
        [Required]
        public int StateID { get; set; }
        [Required]
        public int DistrictID { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string AutomationCategoryID { get; set; }
        [Required]
        public string ItemID { get; set; }

        public bool IsInvloveArchitect { get; set; }

        public string ArchitectMobileNo { get; set; }

        public string ArchitectName { get; set; }

        [Required]
        public string Available_dt { get; set; }

        [Required]
        public string Available_time { get; set; }
        [Required]
        public string Remark { get; set; }
        //[Required]
        //public string CategoryType { get; set; }
        [Required]
        public string Project_name { get; set; }

        public string ArchitectID { get; set; }
        
    }

    public class AddAutomationLeadGenerations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddAutomationLeadGeneration> data { get; set; }
    }
    public class AddAutomationLeadGeneration
    {
        public string output { get; set; }
    }
}