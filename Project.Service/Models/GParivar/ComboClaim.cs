using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofComboClaim
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ComboId { get; set; }
    }

    public class ComboClaims
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboClaim> data { get; set; }
    }

    public class ComboClaim
    {
        public string itemnm { get; set; }
        public string finalqty { get; set; }
        public string totalsale { get; set; }
        public string difference { get; set; }
        public string pending { get; set; }
    }
}