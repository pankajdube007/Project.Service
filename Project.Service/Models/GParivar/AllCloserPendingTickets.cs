using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{

    public class ListofAllCloserPendingTickets
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }

    }

    public class AllCloserPendingTicketsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllCloserPendingTicketsList> data { get; set; }
    }

    public class AllCloserPendingTicketsList
    {
        public string Morethan7days { get; set; }
        public string Morethan15days { get; set; }
        public string Morethan30days { get; set; }

    }
}