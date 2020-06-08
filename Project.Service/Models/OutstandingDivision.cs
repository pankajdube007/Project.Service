using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class OutstandingDivisionAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OutstandingDivisions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OutstandingDivision> data { get; set; }
    }

    public class OutstandingDivision
    {
        public List<OutstandingDivisionDetails> outstandingdetails { get; set; }
        public List<OutstandingDivisionTotal> outstandingtotal { get; set; }
        public bool OnlinePayment { get; set; }
        public bool isregistered { get; set; }
        public bool IsActive { get; set; }
        public string errormsg { get; set; }
        public bool duesquence { get; set; }
    }

    public class OutstandingDivisionDetails
    {
        public string divisionnm { get; set; }
        public string due { get; set; }
        public string overdue { get; set; }
        public string outstanding { get; set; }
    }

    public class OutstandingDivisionTotal
    {
        public string duetotal { get; set; }
        public string overduetotal { get; set; }
        public string outstandingtotal { get; set; }
    }
}