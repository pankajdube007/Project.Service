using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public partial class UserValidation
    {
        public class UserInfo
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public Users data { get; set; }
        }

        public class Users
        {
            public int userlogid { get; set; }
            public string userlognm { get; set; }
            public string usernm { get; set; }
            public string status { get; set; }
            public bool issuccess { get; set; }
            public bool isblock { get; set; }
            public Work workingtime { get; set; }
            public List<Holiday> holidaylist { get; set; }

            //public string servertime { get; set; }
            public string lastsynclead { get; set; }

            public string uniquekey { get; set; }
            public string Usercat { get; set; }
        }

        public class Work
        {
            public string workingtimeto { get; set; }
            public string workingtimefrom { get; set; }
        }

        public class Holiday
        {
            public int slno { get; set; }
            public string holilist { get; set; }
        }
    }

    public class ActionResult
    {
        [Required(ErrorMessage = "Please Input User Name")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string usernm { get; set; }

        [Required(ErrorMessage = "Please Input Password")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "Please Input Device ID")]
        public string deviceid { get; set; }

        [Required(ErrorMessage = "Please Input Pushwoosh ID")]
        public string pushwooshid { get; set; }
    }
}