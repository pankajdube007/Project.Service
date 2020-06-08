using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public partial class ValidationOther
    {
        public class UserInfoOther
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public UsersOther data { get; set; }
        }

        public class UsersOther
        {
            public int userlogid { get; set; }
            public string userlognm { get; set; }
            public string usernm { get; set; }
            public int stateid { get; set; }
            public string status { get; set; }
            public bool issuccess { get; set; }
            public bool isblock { get; set; }
            public string uniquekey { get; set; }
        }
    }

    public class ValidationOtherAction
    {
        [Required(ErrorMessage = "Please Input User Name")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string usernm { get; set; }

        [Required(ErrorMessage = "Please Input Password")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "Please Input IP Address ")]
        public string ip { get; set; }
    }
}