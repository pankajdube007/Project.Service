using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofMerchant
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int MerchantType { get; set; }
        
    }

    public class GetMerchantLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetMerchantList> data { get; set; }
    }

    public class GetMerchantList
    {
        public string SlNo { get; set; }
        public string SupplierName { get; set; }
        public string merchanttype { get; set; }
        
    }
}