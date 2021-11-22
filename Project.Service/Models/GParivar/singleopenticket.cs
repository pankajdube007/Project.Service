using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class Listofsingleopenticket
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string slno { get; set; }
        [Required]
        public string uniquekey { get; set; }

    }

    public class AllsingleopenticketLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllsingleopenticketList> data { get; set; }
    }

    public class AllsingleopenticketList
    {
        public string slno { get; set; }
        public string TktNo { get; set; }
        public string Tktdt { get; set; }
        public string TicketOwner { get; set; }
        public string TktDisapproveRejectComment { get; set; }
        public string ProductDescription { get; set; }
        public string CustContactNo { get; set; }
        public string CustName { get; set; }
        public string EmailID { get; set; }
        public string CustAddress { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string statenm { get; set; }
        public string Distrctnm { get; set; }
        public string PersonCallingName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonContactNo { get; set; }
        public string ItemEANNo { get; set; }
        public string ItemSerialNo { get; set; }
        public string ItemQRCode { get; set; }
        public string PurchaseDt { get; set; }
        public string IsProductWarnty { get; set; }
        public string ProductIssue { get; set; }
        public string ProductIssueDesc { get; set; }
        public string WarrantyUpToDt { get; set; }
        public string ProductName { get; set; }
        public string ProductDivision { get; set; }
        public string TktPriorityID { get; set; }
        public string TktStatus { get; set; }
        public string ProductIssues { get; set; }
        public string AssignedToName { get; set; }
        public string AssignTo { get; set; }
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string AssignRemark { get; set; }
        public string PartyTypeName { get; set; }
        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string TktPriority { get; set; }
        public string ProductInputTypeName { get; set; }
        public string TktSource { get; set; }
        public string RejectReason { get; set; }
        public string ReAssignReason { get; set; }
        public string ReScheduleDate { get; set; }
        public string uniquekey { get; set; }
        public string Custuniquekey { get; set; }
        public string Itemuniquekey { get; set; }
        public string ScName { get; set; }
        public string CompanyID { get; set; }
        public string EngineerRemark { get; set; }
        public string VisitStatus { get; set; }
        public string categorynm { get; set; }
        public string IsSCAddressVerified { get; set; }
        public string CustomerCallConfirmation { get; set; }
    }
}