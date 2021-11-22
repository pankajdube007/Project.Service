using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofEmployeeContact
    {
        [Required]
        public string ClientSecret { get; set; }
    }

    public class EmployeeContacts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EmployeeContact> data { get; set; }
    }

    public class EmployeeContact
    {
        public string workdescription { get; set; }
        public string contactperson { get; set; }
        public string mobile { get; set; }
        public string landline { get; set; }
        public string email { get; set; }
        public string department { get; set; }
    }
}