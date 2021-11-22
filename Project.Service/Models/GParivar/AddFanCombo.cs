using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
  
    public class ListAddFanCombo
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string noofcombo { get; set; }
        [Required]
        public string Catids { get; set; }
        [Required]
        public string Qty { get; set; }
        [Required]
        public string totalReward  { get; set; }

    }

    public class AddFanCombos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddFanCombo> data { get; set; }
    }

    public class AddFanCombo
    {
        public string OutPut { get; set; }
    }
}