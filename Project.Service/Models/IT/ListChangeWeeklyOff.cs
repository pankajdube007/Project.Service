using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{

    public class ListofChangeWeeklyOff
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int EmpID { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string NotificationId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string DeviceType { get; set; }

    }

    public class AddChangeWeeklyOffLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddChangeWeeklyOffList> data { get; set; }
    }

    public class AddChangeWeeklyOffList
    {
        public string output { get; set; }
    }

}