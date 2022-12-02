using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class peechedekhoapp
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class Getpeechedekhoapp
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Getpeechedekhoapp1> data { get; set; }
    }

    public class Getpeechedekhoapp1
    {
        public string displaynm { get; set; }
        public string HomeBranch { get; set; }
        public string cin { get; set; }
        public string salesexname { get; set; }
        public string runningtarget { get; set; }
        public string target1 { get; set; }
        public string target2 { get; set; }
        public string target3 { get; set; }
        public string nexttarget { get; set; }
        public string octto15novNormal { get; set; }
        public string octto15novBonus { get; set; }
        public string oct16todec31Normal { get; set; }
        public string oct16todec31Bonus { get; set; }
        public string q2Bonus { get; set; }
        public string Total { get; set; }
    }
}
