using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class AllItemDataActions
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid category id")]
        public int categoryid { get; set; }

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

    public class itemDesc
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<itemDescs> data { get; set; }
    }

    public class itemDescs
    {
        public int slno { get; set; }
        public string ProductCode { get; set; }
        public string detail { get; set; }
        public int divisionid { get; set; }
    }
}