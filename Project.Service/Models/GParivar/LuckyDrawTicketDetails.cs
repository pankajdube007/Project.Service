using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   
    public class ListsLuckyDrawTicketDetail
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class LuckyDrawTicketDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LuckyDrawTicketDetail> data { get; set; }
    }

    public class LuckyDrawTicketDetail
    {
        public string Name { get; set; }
        public string ticketno { get; set; }
        public string useddate { get; set; }
        public string gift { get; set; }

    }
}