using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListNukkadMeetRequestByID
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }
    public class NukkadMeetRequestByIDS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkadMeetRequestByID> data { get; set; }
    }
    public class NukkadMeetRequestByID
    {
        public string Slno { get; set; }
                               
        public string TypeOfMeet { get; set; }

        public string Meetname { get; set; }

        public string Meetdate { get; set; }

        public string MeetVenueAddTypename { get; set; }

        public string MeetVenueAdd { get; set; }

        public string MeetPincode { get; set; }

        public string Meetstate { get; set; }

        public string Meetdistrict { get; set; }

        public string Meetcity { get; set; }

        public string AddtenceTotalCount { get; set; }

        public string ExpectedExpense { get; set; }
       
        public string ListGiftItem { get; set; }

        public string PurposeName { get; set; }

        public string ListaddcomsalesExnm { get; set; }

        public string Meetremark { get; set; }

        
    }
}