using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofEnquiryDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int Visitorid { get; set; }


        public string SearchBy { get; set; }
    }

    public class GetListofEnquiryDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListofEnquiryDetail> data { get; set; }
    }

    public class GetListofEnquiryDetail
    {

        public int ExId { get; set; }
        public string salesexnm { get; set; }
        public string enquiryStatus { get; set; }
        public string enquiryCode { get; set; }
        public int enquirytypeID { get; set; }
        public string enquirytypename { get; set; }
        public int cityid { get; set; }
        public string citynm { get; set; }
        public string onsitepersonname { get; set; }
        public string contactno { get; set; }
        public string pincoide { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string comment { get; set; }
      
    }
}