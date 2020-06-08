using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class PendingOrderDivisionAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class PendingOrderDivisions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PendingOrderDivision> data { get; set; }
    }

    public class PendingOrderDivision
    {
        public List<PendingOrderDivisionDetails> pendingdetails { get; set; }
        public List<PendingOrderDivisionTotal> pendingtotal { get; set; }
    }

    public class PendingOrderDivisionDetails
    {
        public string divisionnm { get; set; }
        public string pending { get; set; }
    }

    public class PendingOrderDivisionTotal
    {
        public string pendingtotal { get; set; }
    }
}