
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class EnquiryTypeItem
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


    public class EnquiryTypeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EnquiryTypeList> data { get; set; }
    }

    public class EnquiryTypeList
    {
        public int Slno { get; set; }
        public string Enquirytype { get; set; }
        

    }
}