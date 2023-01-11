using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class updateinspectionbyuexec
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string RefID { get; set; }

        [Required]
        public object Details { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class updateinspectionbyuexecs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<updateinspectionbyuexeces> data { get; set; }
    }

    public class updateinspectionbyuexeces
    {
        public int type { get; set; }
        public string message { get; set; }
    }
}