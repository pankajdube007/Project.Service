using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ExewiseitemwisesaleList
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

    public class Exewiseitemwisesale
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Exewiseitemwisesales> data { get; set; }
    }

    public class Exewiseitemwisesales
    {
        public string MonthName { get; set; }

        public string CurrentQuantity { get; set; }

        public string CurrentAmount { get; set; }

        public string lastYearQuantity { get; set; }

        public string lastYearAmount { get; set; }

        public string AmountDiffPer { get; set; }
    }
}