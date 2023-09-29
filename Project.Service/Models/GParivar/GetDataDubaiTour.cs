using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class ListGetDataDubaiTour
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }
    public class GetDataDubaiTour
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetDataDubaiTours> data { get; set; }
    }
    public class GetDataDubaiTours
    {
        public string DisplayName { get; set; }
        public string Homebranch { get; set; }
        public string HomebranchId { get; set; }
        public string PartyId { get; set; }
        public string Typecat { get; set; }
        public string Sale { get; set; }
        public string GiftCount { get; set; }
        public string CnAmount { get; set; }
        public string OutStanding { get; set; }
        public string Slab { get; set; }
        public string per { get; set; }
        public string isallow { get; set; }
        public string isactive { get; set; }
        public string pdfurl { get; set; }
    }
}