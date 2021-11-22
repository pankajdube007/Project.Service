using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofAllOpenTickets
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Type { get; set; }

    }

    public class AllOpenTicketsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllOpenTicketsList> data { get; set; }
    }

    public class AllOpenTicketsList
    {
        public string Number { get; set; }
        public string slno { get; set; }
        public string Tktno { get; set; }
        public string Tktdt { get; set; }
        public string CustName { get; set; }
        public string ProductDivision { get; set; }
        public string ProductName { get; set; }
        public string uniquekey { get; set; }
        public string TktPriority { get; set; }
        public string CustContactNo { get; set; }
        public string ProductIssues { get; set; }
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string TktStatus { get; set; }
        public string ScName { get; set; }
        public string AssignedToName { get; set; }

    }
}