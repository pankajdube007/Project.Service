using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofUserLoginData
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string AppType { get; set; }

        [Required]
        public string deviceid { get; set; }

        [Required]
        public string DeviceType { get; set; }

        [Required]
        public string AppVersion { get; set; }


        [Required]
        public string OSVersion { get; set; }

        [Required]
        public string Apphittime { get; set; }

        [Required]
        public string pooswooshid { get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        public string Lat { get; set; }

        [Required]
        public string lng { get; set; }

        [Required]
        public string ModalType { get; set; }

        public string address { get; set; }

    }

    //public class UserLoginDatas
    //{
    //    public bool result { get; set; }
    //    public string message { get; set; }
    //    public string servertime { get; set; }
    //    public List<UserLoginData> data { get; set; }
    //}

    //public class UserLoginData
    //{
    //    public string output { get; set; }
    //}
}