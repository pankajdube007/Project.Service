using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofExecFanCombo
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int ExecId { get; set; }
     

    }

    public class ExecFanComboLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecFanComboList> data { get; set; }
    }

    public class ExecFanComboList
    {
        public string HomeBranch { get; set; }
        public string noofcombo { get; set; }
        public string slno { get; set; }
        public string displaynmwitharea { get; set; }
        public string salesexname { get; set; }
        public string partycontactno { get; set; }
        public string execcontactno { get; set; }
        public string bookingdate { get; set; }

    }
}