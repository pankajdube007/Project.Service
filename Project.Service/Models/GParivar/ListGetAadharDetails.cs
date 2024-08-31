using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class  GetListAadhar
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetListAadharDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAadharDetail> data { get; set; }
    }

    public class GetListAadharDetail
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Pancard { get; set; }
        public string AadharNo { get; set; }
        public string IsUpdated { get; set; }
    }
}