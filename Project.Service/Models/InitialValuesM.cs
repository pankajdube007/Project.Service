using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class InitialValuesMs
    {
        public bool result { get; set; }
        public string servertime { get; set; }
        public string message { get; set; }
        public List<InitialValuesM> data { get; set; }
    }

    public class InitialValuesM
    {
        public string versionCode { get; set; }
        public string versionNumber { get; set; }
        public string iosVersion { get; set; }
        public string divisionLastUpdated { get; set; }
        public string enquiryLastUpdated { get; set; }
        public string BaseApi { get; set; }
        public string ValidateUserDealer { get; set; }
        public string ValidateCIN { get; set; }
        public string VerifyOTP { get; set; }
        public string SetPassword { get; set; }
        public string TodaySale { get; set; }
        public string TodayPayment { get; set; }
        public string TotalPaymentBranchWise { get; set; }
        public string TotalSaleBranchWise { get; set; }
        public string TotalSaleDivisionWiseManagement { get; set; }
        public string TotalPaymentDivisionWiseManagement { get; set; }
        public string NDAReport { get; set; }
        public string NDAReportAll { get; set; }
        public string OutstandingbyDays { get; set; }
        public string ListsofAllBranch { get; set; }
        public string BranchNonMovementStockValuation { get; set; }
        public string BranchNonMovementStockValuationDetails { get; set; }
        public string BranchwiseSalesCompare { get; set; }
        public string PendingBranchWise { get; set; }
        public string PendingBranchWiseChild { get; set; }
        public string BranchwiseAllTransaction { get; set; }
        public string ManagementBranchwiseOutstanding { get; set; }
        public string ManagementUserType { get; set; }
        public string ManagementBranchwiseOutstandingChild { get; set; }
        public string ManagementBranchwiseSalesCompareChild { get; set; }
        public string ManagementManagementDealerDetails { get; set; }
        public string ManagementTodaySaleDivisionwise { get; set; }
        public string ManagementTodaySaleBranchwise { get; set; }
        public string ManagementBranchwiseExpense { get; set; }
        public string ManagementBranchwiseExpenseChild { get; set; }
        public string ManagementInvoiceReportManagementStatewise { get; set; }
        public string ManagementStatewiseSalesCompare { get; set; }
        public string ManagementStatewiseSaleComparechild { get; set; }
        public string ManagementDistrictwiseSalesCompare { get; set; }
        public string ManagementDistrictwiseSaleComparechild { get; set; }
        public string ManagementTotalSaleBranchWiseLast { get; set; }
        public string ManagementManagementHeadwiseExpense { get; set; }
        public string ManagementDealerListManagment { get; set; }
        public string ManagementDateWiseSale { get; set; }
        public string ManagementExpenseChildAll { get; set; }
        public string ManagementDivisionListManagementt { get; set; }
        public string ManagementCategoryWiseSalesCompare { get; set; }
        public string ManagementCategoryWiseSalesCompareChild { get; set; }
        public string ManagementLedgerwiseExpense { get; set; }
        public string ManagementLedgerwiseBranchExpense { get; set; }
        public string ManagementExpenseChildAllSubChild { get; set; }
        public string ManagementPartyStatus { get; set; }
        public string ManagementPartyDeactive { get; set; }
        public string ManagementVendor { get; set; }
        public string ManagementSupplier { get; set; }
        public string Managementbranchwisesecuredamt { get; set; }
        public string Managementpartywisesecuredamt { get; set; }
        public string ManagementVendorPurchaseAndLedger { get; set; }
        public string ManagementSupplierPurchaseAndLedger { get; set; }
        public string ManagementVendorPurchaseAndLedgerBalanceInvoiceNo { get; set; }
        public string ManagementSupplierPurchaseAndLedgerBalanceInvoiceNo { get; set; }
        public string ManagementVendorPurchaseAndLedgerBalanceOrderNo { get; set; }
        public string ManagementVendorPurchaseAndLedgerBalancePaymente { get; set; }
        public string ManagementSupplierPurchaseAndLedgerBalancePayment { get; set; }
        public string ManagementEmployeeList { get; set; }
        public string ManagementEmployeeListDetails { get; set; }
        public string Managementcatwisepurchaseamt { get; set; }
        public string Managementdeptwiseemployeecount { get; set; }
        public string Managementdepartmentwiseempdetail { get; set; }
        public string Managementdesignationemployeecount { get; set; }
        public string Managementdesignationwiseempdetail { get; set; }
        public string Managementjoindatewiseempcount { get; set; }
        public string Managementjoindatewiseempdata { get; set; }
        public string Managementmonthwiseempjoincount { get; set; }
        public string ManagementlocationEmployeeList { get; set; }
        public string ManagementlocationEmployeeListDetails { get; set; }
        public string ManagementemployeeallList { get; set; }
        public string ManagementemployeedetailList { get; set; }
        public string Managementbranchwiseattcount { get; set; }
        public string Managementlocationwiseattcount { get; set; }
        public string Managementbranchwiseattdetails { get; set; }
        public string Managementlocwiseattdetails { get; set; }
        public string divisionSalesReport { get; set; }
        public string dispatchedMaterial { get; set; }
        public string pendingOrders { get; set; }
        public string pendingOrdersPDF { get; set; }
        public string outstanding { get; set; }
        public string outstandingReport { get; set; }
        public string creditLimit { get; set; }
        public string activeScheme { get; set; }
        public string salesPaymentReport { get; set; }
        public string divisionList { get; set; }
        public string priceList { get; set; }
        public string sendEnquiry { get; set; }
        public string sendFeedback { get; set; }
        public string subjectList { get; set; }
        public string salesPaymentReportDetails { get; set; }
        public string policy { get; set; }
        public string technicalSpecification { get; set; }
        public string brandingImages { get; set; }
        public string topProductDealer { get; set; }
        public string topProductDistrict { get; set; }
        public string divisionWiseYSA { get; set; }
        public string ysAreport { get; set; }
        public string lastYearSales { get; set; }
        public string pendingOrderDivisionWise { get; set; }
        public string outstandingDivisionWise { get; set; }
        public string schemeDetails { get; set; }
        public string salesSummary { get; set; }
        public string catalogue { get; set; }
        public string aging { get; set; }
        public string document { get; set; }
        public string discoverWorld { get; set; }
        public string comboPlaceOrder { get; set; }
        public string comboCompare { get; set; }
        public string ledgerAmount { get; set; }
        public string comboClaim { get; set; }
        public string youtubeVideo { get; set; }
        public string comboDetails { get; set; }
        public string comboSchemes { get; set; }
        public string ledgerAmountDebit { get; set; }
        public string comboTotalQuantit { get; set; }
        public string sendNotification { get; set; }
        public string amountConfirmation { get; set; }
        public string wheelSpins { get; set; }
        public string wheelSpinsDetails { get; set; }
        public string applyCN { get; set; }
        public string sendNontification { get; set; }
        public string creditNoteDetails { get; set; }
        public string logToServer { get; set; }
        public string reasonSaleReturnRequest { get; set; }
        public string freePayOTP { get; set; }
        public string dealerBankDetails { get; set; }
        public string allpaymenttypedetails { get; set; }
        public string allpaymenttypedetailslist { get; set; }
        public string freepayOutstandingReport { get; set; }
        public string checkOutstandingPaymentDetails { get; set; }
        public string verifyFreePayOTP { get; set; }
        public string confirmPaymentRequest { get; set; }
        public string resendPaymentRequestOTP { get; set; }
        public string resendFreePayOTP { get; set; }
        public string cancelFreePayTranstions { get; set; }
        public string paymentFreepayRequest { get; set; }
        public string retryFreepayTransaction { get; set; }
        public string freePayPayment { get; set; }
        public string retryFreepayPaymentTransaction { get; set; }
        public string invoicewiseCD { get; set; }
        public string disputeType { get; set; }
        public string raiseDispute { get; set; }
        public string mpinchecks { get; set; }
        public string orderDivisionAndCat { get; set; }
        public string orderItemDetails { get; set; }
        public string placeOrder { get; set; }
        public string orderDetails { get; set; }
        public string orderCatItemDetails { get; set; }
        public string orderItemCatPriceDetails { get; set; }
        public string targetScheme { get; set; }
        public string schemeGrowth { get; set; }
        public string salereturnrequestadd { get; set; }
        public string salereturnrequestselects { get; set; }
        public string salereturnrequestdeletes { get; set; }
        public string salereturnrequestupdates { get; set; }
        public string salereturnrequestshows { get; set; }
        public string salerequestbillingupdate { get; set; }
        public string team { get; set; }
        public string addPartyFinalMatchSummary { get; set; }
        public string addPartySemiFinalMatchSummary { get; set; }
        public string saleWorldCup { get; set; }
        public string matchSummary { get; set; }
        public string addPartyMatchSummary { get; set; }
        public string showRoom { get; set; }
        public string unreadNotificationCount { get; set; }
        public string addLogToRead { get; set; }
        public string mpinadds { get; set; }
        public string activeDevice { get; set; }
        public string activeDeviceLogout { get; set; }
        public string analyticsDataadd { get; set; }
        public string addLogToServer { get; set; }
        public string forgetMpin { get; set; }
        public string matchSummaryExpertOpenion { get; set; }
        public string partyExpertOpenionPredictions { get; set; }
        public string saleWorldCuppartB { get; set; }
        public string freePayFailedTranstions { get; set; }
        public string todList  { get; set; }
        public string todSalesInfo { get; set; }
        public string submitGroupTOD { get; set; }
        public string appBanner { get; set; }
        public string dhanbarseQwikpayVideo { get; set; }
        public string dhanbarseQwikpayPriceList { get; set; }
        public string directdealerspin { get; set; }
        public string directDealerSpinAmountConfirmation { get; set; }
        public string subcatimgdivwise { get; set; }
        public string priceListCatalogue { get; set; }













        public bool forceUpdate { get; set; }
    }
}