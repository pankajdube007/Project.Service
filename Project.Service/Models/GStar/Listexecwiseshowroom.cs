using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class Listexecwiseshowroom
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class Listexecwiseshowrooms
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Listexecwiseshowroomes> data { get; set; }

    }
    public class Listexecwiseshowroomes
    {
        public int Slno { get; set; }

        public string Name { get; set; }
    }
}