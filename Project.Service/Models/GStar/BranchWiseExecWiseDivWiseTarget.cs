using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{

    public class ListsofBranchWiseExecWiseDivWiseTarget
    {
        [Required]
        public int BranchId { get; set; }


        [Required]
        public string  Cin { get; set; }


        [Required]
        public string Cat { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public string ClientSecret { get; set; }

     
    }

    public class BranchWiseExecWiseDivWiseTargets
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseExecWiseDivWiseTargetFinal> data { get; set; }
    }

    public class BranchWiseExecWiseDivWiseTargetFinal
    {
        public List<BranchWiseExecWiseDivWiseTarget> BranchWiseExecWiseDivWiseTargetDetails { get; set; }
        public string TotalSale { get; set; }
        public string TotalTarget { get; set; }
        public string TotaldealerTarget { get; set; }
    }

    public class BranchWiseExecWiseDivWiseTarget
    {
        public string ExeId { get; set; }
        public string Name { get; set; }
        public string sales { get; set; }
        public string target { get; set; }
        public string dealertarget { get; set; }
    }
}
