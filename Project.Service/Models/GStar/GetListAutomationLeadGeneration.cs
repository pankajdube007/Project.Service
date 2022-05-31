using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    

    public class GetAutomationLeadGeneration
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string CategoryType { get; set; }
    }

    public class GetListAutomationLeadGenerations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGeneration> data { get; set; }
    }

    public class GetListAutomationLeadGeneration
    {
        public string Cust_Mob_No { get; set; }
        public string Cust_Name { get; set; }
        public string Pincode { get; set; }
        public string Add_Line_1 { get; set; }
        public string Add_Line_2 { get; set; }
        public string State_ID { get; set; }
        public string statenm { get; set; }
        public string Distrctnm { get; set; }
        public string City_ID { get; set; }
        public string categorynm { get; set; }
        public string Involve_Architech { get; set; }
        public string Architech_No { get; set; }
        public string Architech_Name { get; set; }
        public string Remark { get; set; }
        public string Project_name { get; set; }
        public string ApprovalStatus { get; set; }
        public string Reference_NO { get; set; }
        public string Available_dt { get; set; }
        public string Available_time { get; set; }
        public string CategoryType { get; set; }
       
    }
}