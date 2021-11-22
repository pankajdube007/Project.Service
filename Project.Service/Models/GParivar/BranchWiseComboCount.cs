using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofBranchWiseComboCount
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }

    }

    public class BranchWiseComboCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseComboCountList> data { get; set; }
    }

    public class BranchWiseComboCountList
    {
        public string HomeBranch { get; set; }
        public string noofcombo { get; set; }
        

    }
}