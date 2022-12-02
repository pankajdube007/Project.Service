using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models.GStar
{
    public class peechedekholightapp
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class Getpeechedekholightapp
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Getpeechedekholightapp1> data { get; set; }
    }
    public class Getpeechedekholightapp1
    {
        public string displaynm { get; set; }
        public string HomeBranch { get; set; }
        public string cin { get; set; }
        public string target1 { get; set; }
        public string target2 { get; set; }
        public string target3 { get; set; }
        public string salesexname { get; set; }
        public string runningtarget { get; set; }
        public string nexttarget { get; set; }
        public string OctNormal { get; set; }
        public string OctBonus { get; set; }
        public string NovNormal { get; set; }
        public string NovBonus { get; set; }
        public string DecNormal { get; set; }
        public string DecBonus { get; set; }
        public string Bonusq2 { get; set; }
        public string Total { get; set; }

    }


}