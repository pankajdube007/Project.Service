using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models.GParivar
{
    public class ListCFDetails
    {
    }

    public class ListofCFDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class CFDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FinalData> data { get; set; }
    }

   
    public class FinalData
    {
        public List<CFDetailsList> CFDetailsListdata { get; set; }
        public List<TotalCFDetailsList> TotalCFDetailsListdata { get; set; }
        public bool ismore { get; set; }
    }

    public class CFDetailsList
    {
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public string TranAmount { get; set; }
        public string BalanceOutstanding { get; set; }
        public string DueDate { get; set; }
        //public string OverdueWithinCureINR { get; set; }
        //public string OverdueWithinCureNoOfDays { get; set; }
        //public string OverdueBeyondCureINR { get; set; }
        //public string OverdueBeyondCureNoOfDays { get; set; }

    }

    public class TotalCFDetailsList
    {
        public string Dealerssanctionlimits { get; set; }
        public string AvailableLimits { get; set; }
        public string BalanceOSwiththebank { get; set; }

        public string OverdueWithinCureINR { get; set; }
        public string OverdueWithinCureNoOfDays { get; set; }
        public string OverdueBeyondCureINR { get; set; }
        public string OverdueBeyondCureNoOfDays { get; set; }
        public bool IsFreeze { get; set; }
        public string ZeroTo30 { get; set; }
        public string ThirtyOneTo60 { get; set; }
        public string SixtyOneTo90 { get; set; }
        public string NinetyOneAbove { get; set; }
    }
    
}