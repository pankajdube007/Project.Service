using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ExewiseitemwisepartywisesaleList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string Todate { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }
    public class Exewiseitemwisepartywisesale
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Exewiseitemwisepartywisesales> data { get; set; }

    }

    public class Exewiseitemwisepartywisesales
    {
        public string Displayname { get; set; }

        public string Cin { get; set; }

        public string CurrentSale { get; set; }

        public string lastYearSale{ get; set; }

        public string DiffPer { get; set; }

    }
}