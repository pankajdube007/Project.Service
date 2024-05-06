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

        //[Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Customer MobileNo must contain only numeric digits.")]
        //[StringLength(10, ErrorMessage = "Customer MobileNo must be up to 10 digits.")]
        //public int CustomerMobileNo { get; set; }

        [Required]
        public string CustomerName { get; set; }

        //[Required]
        public string EmailID { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        //[Required]
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

        public string Architech_CompanyName { get; set; }

        [Required]
        public string Available_dt { get; set; }

        [Required]
        public string Available_time { get; set; }
        //[Required]
        public string Remark { get; set; }
        //[Required]
        //public string CategoryType { get; set; }
        [Required]
        public string Project_name { get; set; }

        public string ArchitectID { get; set; }


        public string BuilderMobileNo { get; set; }

        public string BuilderName { get; set; }

        public string Builder_CompanyName { get; set; }

        public string BuilderID { get; set; }

        public string ElectricalMobileNo { get; set; }

        public string ElectricalName { get; set; }

        public string Electrical_CompanyName { get; set; }

        public string ElectricalID { get; set; }

        public string OtherMobileNo { get; set; }

        public string OtherName { get; set; }

        public string Other_CompanyName { get; set; }

        public string OtherID { get; set; }


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