using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class UserValidationParty
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
            public string userlogid { get; set; }
            public string usernm { get; set; }
            public string mobile { get; set; }
            public string firmname { get; set; }
            public string exname { get; set; }
            public string exmobile { get; set; }
            public string exhead { get; set; }
            public string exheadmobile { get; set; }
            public string gstno { get; set; }
            public string email { get; set; }
            public int slno { get; set; }
            public int branchid { get; set; }
            public string branchnm { get; set; }
            public int stateid { get; set; }
            public string status { get; set; }
            public bool issuccess { get; set; }
            public bool isblock { get; set; }
            public string lastsynclead { get; set; }
            public string uniquekey { get; set; }
            public string Usercat { get; set; }
            public string branchadd { get; set; }
            public string branchphone { get; set; }
            public string branchemail { get; set; }
            public string joiningdt { get; set; }
            public string dob { get; set; }
            public string designation { get; set; }
            public string lstlogin { get; set; }
            public string workingarea { get; set; }
            public string immediatehead { get; set; }
            public string immediatehdmobile { get; set; }
            public string module { get; set; }
        }
    }

    public class ActionResultParty
    {
        [Required(ErrorMessage = "Please Input CIN")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Please Input Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Input Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please Input ClientSecret")]
        public string ClientSecret { get; set; }

        [Required(ErrorMessage = "Please Input Device ID")]
        public string Deviceid { get; set; }

        [Required(ErrorMessage = "Please Input Device ID")]
        public string DeviceType { get; set; }

        public string ModalType { get; set; }

        [Required(ErrorMessage = "Please Input Device Type")]
        public string AppVerion { get; set; }

        [Required(ErrorMessage = "Please Input App Version")]
        public string OsVersion { get; set; }

        [Required(ErrorMessage = "Please Input Os Version")]
        public string Pushwooshid { get; set; }

        public string IP { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }
    }
}