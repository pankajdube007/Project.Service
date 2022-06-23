using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofExecutiveTravelExpenses
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public string search { get; set; }

    }

    public class GetExecutiveTravelExpensesLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecutiveTravelExpensesList> data { get; set; }
    }

    public class GetExecutiveTravelExpensesList
    {
        public string Execid { get; set; }
        public string ExpenseNo { get; set; }
        public string TravelDate { get; set; }
        public string ImgBill { get; set; }
        public string SupplierName { get; set; }
        public string GSTIN { get; set; }
        public string TotalAmt { get; set; }
        public string createdt { get; set; }
        public string ApprovalStatus { get; set; }
        public string TotalAmount { get; set; }
        public string TotalReimbursableAmount { get; set; }
    }
}