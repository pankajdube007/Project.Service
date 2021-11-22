using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListsLuckDrawSpin
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string GiftId { get; set; }
        [Required]
        public string RandomNo { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class LuckDrawSpins
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LuckDrawSpin> data { get; set; }
    }

    public class LuckDrawSpin
    {
        public string Result { get; set; }
        public string ResultId { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }
        public string TotalChance { get; set; }
        public string Used { get; set; }
        public string Remaining { get; set; }
        public string giftid { get; set; }
        public string randomno { get; set; }

    }
}