using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofBlockedParty
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public string clientsecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int count { get; set; }
    }

    public class BlockedPartys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BlockedPartyfinal> data { get; set; }
    }

    public class BlockedPartyfinal
    {
        public List<BlockedParty> BlockedPartyDetails { get; set; }
        public bool ismore { get; set; }
    }

    public class BlockedParty
    {
        public string partyName { get; set; }
        public string exnm { get; set; }
        public string partyCategory { get; set; }
        public string permanentLimit { get; set; }
        public string outstanding { get; set; }
        public string templimitleft { get; set; }
    }
}