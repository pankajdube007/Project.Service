using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofBranchAction
    {
        [Required]
        public string uniquekey { get; set; }

        [Range(1, 10000, ErrorMessage = "User not Valid!!!")]
        public int userid { get; set; }

        [Required]
        public string usercat { get; set; }
    }

    public class Branches
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Branch> data { get; set; }
    }

    public class Branch
    {
        public List<BranchD> BranchData { get; set; }
        public List<catD> CatData { get; set; }
    }

    public class BranchD
    {
        public int slno { get; set; }
        public string branchnm { get; set; }
    }

    public class catD
    {
        public int catid { get; set; }
        public string catname { get; set; }
    }
}