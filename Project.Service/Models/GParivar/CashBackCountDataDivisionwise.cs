using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  



    public class ListCashBackCountDataDivisionwisedetails
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Stateid { get; set; }
        //[Required]
        //public string DivisionId { get; set; }
        [Required]
        public int  StatusType { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class CashBackCountDataDivisionwiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CashBackCountDataDivisionwiseList> data { get; set; }
    }
    public class CashBackCountDataDivisionwiseList
    {
       
        public string SateId { get; set; }
        public string SateName { get; set; }
        public string WIRINGDEVICES { get; set; }
        public string WIRECABLE { get; set; }
        public string MCBDBS { get; set; }


    }
}