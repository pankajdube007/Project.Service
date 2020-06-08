using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSchemeUploadDetails
    {
        [Required]
        public string ClientSecret { get; set; }
    }

    public class SchemeUploadDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeUploadDetailFinal> data { get; set; }
    }

    public class SchemeUploadDetailFinal
    {
        public List<SchemeUploadDetailCompany> allcompanyname { get; set; }
        public List<SchemeUploadDetailRange> allrangename { get; set; }
        public List<SchemeUploadDetailType> alltype { get; set; }
    }

    public class SchemeUploadDetailCompany
    {
        public string companyname { get; set; }
    }

    public class SchemeUploadDetailRange
    {
        public string rangename { get; set; }
    }

    public class SchemeUploadDetailType
    {
        public string type { get; set; }
    }

    public class ListsofSchemeUpload
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string RangeName { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Remark { get; set; }

        [Required]
        public string File { get; set; }
    }

    public class SchemeUploads
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<retunvalue> data { get; set; }
    }

    public class retunvalue
    {
        public string ouptput { get; set; }
    }
}