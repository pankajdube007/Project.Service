using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListEmpAttendees
    {
    }

    public class ListofEmpAttendees
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string SalesExCodeNm { get; set; }

    }

    public class GetEmpAttendeesLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetEmpAttendeesList> data { get; set; }
    }

    public class GetEmpAttendeesList
    {
        public string SlNo { get; set; }
        public string SalesExCodeName { get; set; }
        
    }
}