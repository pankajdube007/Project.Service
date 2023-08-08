using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetAddofAutomationLeadGenerationShowroomQuotation
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string HeadSlNo { get; set; }

        [Required]
        public object OrderDetails { get; set; }
    }

    public class GetAddAutomationLeadGenerationShowroomQuotationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetAddAutomationLeadGenerationShowroomQuotationList> data { get; set; }
    }

    public class GetAddAutomationLeadGenerationShowroomQuotationList
    {
        public string message { get; set; }
    }

    //public class AddShowroomQuotationList
    //{
    //    public string HeadID { get; set; }
    //    public string ItemID { get; set; }
    //    public string Rate { get; set; }
    //    public string Qty { get; set; }
    //    public string UnitID { get; set; }
    //    public string Amount { get; set; }
       
    //}

}