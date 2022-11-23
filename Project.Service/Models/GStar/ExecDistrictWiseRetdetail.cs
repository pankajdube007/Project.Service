using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models.GStar
{
    public class ExecDistrictWiseRetdetail
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int districtid { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class GetExecDistrictWiseRetdetail
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecDistrictWiseRetdetail1> data { get; set; }
    }

    public class GetExecDistrictWiseRetdetail1
    {
        //public string districtid { get; set; }
        public string name { get; set; }
        public string Pincode { get; set; }
        public string Distrctnm { get; set; }
        public string MobileNo { get; set; }
        public string address { get; set; }
        public string lat { get; set; }
        public string Long { get; set; }
    }

}