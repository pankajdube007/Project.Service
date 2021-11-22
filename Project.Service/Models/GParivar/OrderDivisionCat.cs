using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class OrderDivisionCatAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OrderDivisionCats
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrderDivisionCat> data { get; set; }
    }

    public class OrderDivisionCat
    {
        public int divisionid { get; set; }
        public string divisionnm { get; set; }
        public int catid { get; set; }
        public string catnm { get; set; }
        public string catimage { get; set; }
    }
}