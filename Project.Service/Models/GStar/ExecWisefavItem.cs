using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListsofExecWisefavItem
    {
        [Required]
        public int ExId { get; set; }

       

        [Required]
        public string ClientSecret { get; set; }


    }

    public class ExecWisefavItemLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecWisefavItemList> data { get; set; }
    }

    public class ExecWisefavItemList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal NetLanding { get; set; }
        
    }
}