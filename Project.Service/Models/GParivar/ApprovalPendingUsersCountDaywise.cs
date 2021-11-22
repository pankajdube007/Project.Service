using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 
    public class ListApprovalPendingUsersCountDaywise
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ProfileFromDate { get; set; }
        [Required]
        public string ProfileToDate { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class ApprovalPendingUsersCountDaywiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ApprovalPendingUsersCountDaywiseList> data { get; set; }
    }
    public class ApprovalPendingUsersCountDaywiseList
    {
        public string StateName { get; set; }
        public string StateId { get; set; }
        public string FourToSeven { get; set; }
        public string SixteenToThirty { get; set; }
        public string EightToFifteen { get; set; }
        public string MoreThanThirty { get; set; }

    }

}