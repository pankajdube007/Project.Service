using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofHelpDeskConfiguration
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
        
        [Required]
        public string Cat { get; set; }
    }

    public class GetHelpDeskConfigurationDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetHelpDeskConfigurationDetail> data { get; set; }
    }

    public class GetHelpDeskConfigurationDetail
    {
        public string slno { get; set; }
        public string WorkDesc { get; set; }
        public string EmpId { get; set; }
        public string Status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifyBy { get; set; }
        public string modifyDate { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        public string logno { get; set; }
        public string application { get; set; }
        public string ContactPerson { get; set; }
        public string contactno { get; set; }
        public string contactnocur { get; set; }
        public string email { get; set; }
        public string Department { get; set; }


    }
}