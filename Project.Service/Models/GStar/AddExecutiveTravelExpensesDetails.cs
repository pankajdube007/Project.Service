using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{

    public class ListofAddExecTravelExpDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string TravelDate { get; set; }

        //[Required]
        //public string BillNo { get; set; }

        [Required]
        public int TravelReqid { get; set; }

        [Required]
        public int MerchantCategoryid { get; set; }

        [Required]
        public int MerchantTypeid { get; set; }

        [Required]
        public string GSTIN { get; set; }

        [Required]
        public int GSTType { get; set; }

        [Required]
        public decimal TaxPer { get; set; }

        [Required]
        public decimal Cost { get; set; }


        public decimal CGSTamt { get; set; }

       
        public decimal SGSTamt { get; set; }

        
        public decimal IGSTamt { get; set; }

       
        public decimal CGSTper { get; set; }

        
        public decimal SGSTper { get; set; }

        
        public decimal IGSTper { get; set; }

        [Required]
        public decimal RoundOff { get; set; }

        [Required]
        public decimal TotalAmt { get; set; }

        
        public string Description { get; set; }

        [Required]
        public string ImgBill { get; set; }

        
        public string EmpIds { get; set; }

        [Required]
        public string PaidBy { get; set; }

        [Required]
        public string MonthlyReport { get; set; }

    }

    public class AddExecTravelExpDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddExecTravelExpDetailsList> data { get; set; }
    }

    public class AddExecTravelExpDetailsList
    {
        public string output { get; set; }
    }
}