using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListEmpAttDash
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int emptype { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class EmpAttDash
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EmpAttDashs> data { get; set; }
    }

    public class EmpAttDashs
    {
        public string Attendence { get; set; }
        public string Time { get; set; }
    }
}