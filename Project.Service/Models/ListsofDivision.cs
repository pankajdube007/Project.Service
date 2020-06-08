using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDivisionAction
    {
        [Required]
        public DateTime Date { get; set; }
    }

    public class Divisions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Division> data { get; set; }
    }

    public class Division
    {
        public int slno { get; set; }
        public string Divisionnm { get; set; }
    }
}