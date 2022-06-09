using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class TravelTxnManager
    {
        [Required]
        public int ExId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        public int PendingReqCount { get; set; }
        public int ApprovedReqCount { get; set; }
        public int WithdrawReqCount { get; set; }
        public int RejectReqCount { get; set; }
        public int TravelTxnId { get; set; }
        public int TotalTravelDays { get; set; }
        public string TravelFromDate { get; set; }
        public string TravelToDate { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Stop1 { get; set; }
        public string Stop2 { get; set; }
        public string ReturnSource { get; set; }
        public string ReturnDestination { get; set; }
        public bool PersonalTravel { get; set; }
        public string PersonalTravelString { get; set; }
        public int PersonalTravelDays { get; set; }
        public string PersonalTFromDate { get; set; }
        public string PersonalTToDate { get; set; }
        public int ModeOfTransport { get; set; }
        public string ModeOfTransportString { get; set; }
        public int AccomodationDays { get; set; }
        public string Purpose { get; set; }
        public string PurposeString { get; set; }
        public int ApprovedBy1 { get; set; }
        public string ApprovedBy1String { get; set; }
        public string ApprovedBy2String { get; set; }
        public string ApprovedBy1Date { get; set; }
        public int ApprovedBy2 { get; set; }
        public string ApprovedBy2Date { get; set; }
        public string ApprovedStatus { get; set; }
        public string Withdraw { get; set; }
        public string WithdrawDate { get; set; }
        public string WithdrawRemark { get; set; }
        public string RejectRemark { get; set; }
        public string RequestDate { get; set; }
        public string Employeename { get; set; }
        public string search { get; set; }
    

        
        
    }

    public class getListOfTravelManagerDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TravelTxnManager> data { get; set; }
    }
}