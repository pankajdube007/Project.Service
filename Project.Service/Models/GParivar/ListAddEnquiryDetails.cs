using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListAddEnquiryDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int enquirytype { get; set; }

       
        public string onsitepersonname { get; set; }

        
        public string contactno { get; set; }

        [Required]
        public string pincoide { get; set; }

        

        [Required]
        public int cityid { get; set; }
      
        public string Addressline1 { get; set; }
     
        public string Addressline2 { get; set; }
        [Required]
        public string comment { get; set; }

        [Required]
        public int visitorid { get; set; }

    }

    public class AddEnquiryDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddEnquiryDetail> data { get; set; }
    }

    public class AddEnquiryDetail
    {
        public string output { get; set; }
    }

}