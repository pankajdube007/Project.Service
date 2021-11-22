using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class CustomerReciptDetailsAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int SlNo { get; set; }
    }

    public class CustomerReciptDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CustomerReciptDetail> data { get; set; }
    }

    public class CustomerReciptDetail
    {
        public string Invoice { get; set; }
        public string InvoiceDate { get; set; }
        public string Type { get; set; }
        public string AdjustedAmount { get; set; }
    }
}