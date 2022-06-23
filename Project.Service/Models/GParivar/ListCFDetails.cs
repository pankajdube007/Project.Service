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
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DivisionName { get; set; }
        public string InvoiceAmt { get; set; }
        //public string OuststandingAmt { get; set; }
        public string DueDate { get; set; }
        public string Interestdate { get; set; }
        
    }

    public class TotalCFDetailsList
    {
        public string Dealerssanctionlimits { get; set; }
        public string AvailableLimits { get; set; }
        public string BalanceOSwiththebank { get; set; }
    }
    
}