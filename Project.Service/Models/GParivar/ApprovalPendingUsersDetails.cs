using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListApprovalPendingUsersDetails
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int DaysPeriod { get; set; }
        [Required]
        public string ProfileFromDate { get; set; }
        [Required]
        public string ProfileToDate { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class ApprovalPendingUsersDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ApprovalPendingUsersDetailsList> data { get; set; }
    }
    public class ApprovalPendingUsersDetailsList
    {
        public string SlNo { get; set; }
        public string categorynm { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string UniqueKey { get; set; }
        public string UserMasterID { get; set; }
        public string ApprovalStatus { get; set; }
        public string DateOfBirth { get; set; }
        public string ShopName { get; set; }
        public string MembershipID { get; set; }
        public string KycVerified { get; set; }
        public string IsPaytmVerified { get; set; }
        public string IsScanAvailable { get; set; }
        public string Statenm { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string RegDate { get; set; }
        public string ProfileCompleteDate { get; set; }

    }
}