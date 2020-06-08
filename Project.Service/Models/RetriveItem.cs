using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class RetriveItemAction
    {
        [Required]
        public string categoryid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid branch id")]
        public int branchid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid lbranch id")]
        public int lbranchid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid party cat")]
        public int partycat { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid party id")]
        public int partyid { get; set; }

        [Required]
        public string pocat { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid sub tax type")]
        public int subtaxtype { get; set; }

        [Required]
        public string searchtwxt { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class itemsDesc
    {
        // public int slno { get; set; }
        public string ProductCode { get; set; }

        public string detail { get; set; }
        // public int divisionid { get; set; }
    }
}