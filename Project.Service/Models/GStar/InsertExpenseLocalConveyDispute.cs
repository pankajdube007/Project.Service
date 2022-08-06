using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofInsertExpenseLocalConveyDispute
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int typeid { get; set; }

        [Required]
        public int refid { get; set; }

        [Required]
        public int disputetypeid { get; set; }
        
        public string upldfile { get; set; }

        [Required]
        public string remark { get; set; }

    }

    public class AddInsertExpenseLocalConveyDisputeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddInsertExpenseLocalConveyDisputeList> data { get; set; }
    }

    public class AddInsertExpenseLocalConveyDisputeList
    {
        public string output { get; set; }
    }
}