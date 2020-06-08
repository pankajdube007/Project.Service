using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Controllers
{
    public class ListsofComboBooking
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ComboId { get; set; }

        [Required]
        public string ComboQty { get; set; }

        [Required]
        public string ComboAmount { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ComboBookings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboBooking> data { get; set; }
    }

    public class ComboBooking
    {
        public string output { get; set; }
    }
}