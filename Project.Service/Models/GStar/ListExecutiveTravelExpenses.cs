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

        [Required]
        public DateTime fdate { get; set; }

        [Required]
        public DateTime tdate { get; set; }

        public string search { get; set; }

    }

    public class GetExecutiveTravelExpensesLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecutiveTravelLists> data { get; set; }
    }

    


    public class GetExecutiveTravelLists
    {
        public List<GetExecutiveTravelExpensesList> ExecutiveTravelExpensesList { get; set; }
        public List<GetTotalList> TotalList { get; set; }
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
        public string reimbursementamt { get; set; }
        public string catimg { get; set; }
        public string TravelRefNo { get; set; }
        public string Travel { get; set; }
        public string TravelFromDate { get; set; }
        public string TravelToDate { get; set; }

        public string Remark { get; set; }
        


        public bool ShowEdit            { get; set; }
        public string MonthlyReport       { get; set; }
        public string PaidBY              { get; set; }
        public string EmployeeGeplCodes    { get; set; }
        public string EmployeeID          { get; set; }
        public string Description         { get; set; }
        public string IGSTper             { get; set; }
        public string CGSTamt             { get; set; }
        public string SGSTamt             { get; set; }
        public string IGSTamt             { get; set; }
        public string CGSTper             { get; set; }
        public string SGSTper             { get; set; }
        public string CostBeforeTax       { get; set; }
        public string CategoryName        { get; set; }
        public string CategoryId          { get; set; }
        public string MerchantId          { get; set; }
        public string TravelID            { get; set; }
        public string BillNo               { get; set; }

        public string SlNoAttendees { get; set; }
        public string TaxPer { get; set; }
        public string GSTType { get; set; }
        public string SlNoExecutiveTravelExpenses { get; set; }
        

    }

    public class GetTotalList
    {
        public string TotalAmount { get; set; }
        public string TotalReimbursableAmount { get; set; }
    }

}