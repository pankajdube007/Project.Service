using System.Collections.Generic;

namespace Project.Service.Models
{
    public class Subjects
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Subject> data { get; set; }
    }

    public class Subject
    {
        public int slno { get; set; }
        public string Subjectnm { get; set; }
    }
}