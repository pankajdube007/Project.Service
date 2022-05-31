using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class TravelTxn
    {
        [Required]
        public int ExId                    { get; set; }

        [Required]
        public string ClientSecret         { get; set; }
        public int  TotalTravelDays        { get; set; }
        public string TravelFromDate       { get; set; }
        public string TravelToDate         { get; set; }
        public string  Source              { get; set; }
        public string  Destination         { get; set; }
        public string  Stop1               { get; set; }
        public string  Stop2               { get; set; }
        public string  ReturnSource        { get; set; }
        public string ReturnDestination    { get; set; }
        public bool PersonalTravel         { get; set; }
        public int PersonalTravelDays      { get; set; }
        public string PersonalTFromDate    { get; set; }
        public string PersonalTToDate      { get; set; }
        public int ModeOfTransport         { get; set; }
        public int  AccomodationDays       { get; set; }
        public int Purpose                 { get; set; }
        public int  ApprovedBy1            { get; set; }
        public string ApprovedBy1Date      { get; set; }
        public int  ApprovedBy2            { get; set; }
        public string ApprovedBy2Date      { get; set; }
        public string ApprovedStatus       { get; set; }
    }
}