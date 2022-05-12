using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class InitialValuesManagmentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getInitialValueManagement")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<InitialValuesMs> alldcr = new List<InitialValuesMs>();
                List<InitialValuesM> alldcr1 = new List<InitialValuesM>();

                var data = g1.return_dt("dbo.InitialApiManagement");

                alldcr1.Add(new InitialValuesM
                {
                    versionCode = data.Rows[0]["versionCode"].ToString(),
                    versionNumber = data.Rows[0]["versionNumber"].ToString(),
                    iosVersion = data.Rows[0]["iosVersion"].ToString(),

                    forceUpdate = Convert.ToBoolean(data.Rows[0]["forceUpdate"]),

                    divisionLastUpdated = "02/18/2019",
                    enquiryLastUpdated = "02/18/2019",
                    BaseApi = WebConfigurationManager.AppSettings["ApiUrl"].ToString(),
                    ValidateUserDealer = "ValidateUserDealer",
                    ValidateCIN = "ValidateCIN",
                    VerifyOTP = "VerifyOTP",
                    SetPassword = "SetPassword",
                    TodaySale = "GetTodaySale",
                    TodayPayment = "GetTodayPayment",
                    TotalSaleBranchWise = "GetTotalSaleBranchWise",
                    TotalPaymentBranchWise = "GetTotalPaymentBranchWise",
                    TotalSaleDivisionWiseManagement= "getTotalSaleDivisionWiseManagement",
                    TotalPaymentDivisionWiseManagement = "getTotalPaymentDivisionWiseManagement",
                    NDAReport = "GetNDAReport",
                    NDAReportAll = "GetNDAReportAll",
                    OutstandingbyDays = "GetOutstandingbyDays",
                    ListsofAllBranch = "getListsofAllBranch",
                    BranchNonMovementStockValuation = "GetBranchNonMovementStockValuation",
                    BranchNonMovementStockValuationDetails = "GetBranchNonMovementStockValuationDetails",
                    BranchwiseSalesCompare = "GetBranchwiseSalesCompare",
                    PendingBranchWise = "GetPendingBranchWise",
                    PendingBranchWiseChild = "GetPendingBranchWiseChild",
                    BranchwiseAllTransaction = "GetBranchwiseAllTransaction",
                    ManagementBranchwiseOutstanding = "GetManagementBranchwiseOutstanding",
                    ManagementUserType = "GetUserType",
                    ManagementBranchwiseOutstandingChild = "GetManagementBranchwiseOutstandingChild",
                    ManagementBranchwiseSalesCompareChild = "GetBranchwiseSalesCompareChild",
                    ManagementManagementDealerDetails = "getManagementDealerDetails",
                    ManagementTodaySaleDivisionwise = "GetTodaySaleDivisionwise",
                    ManagementTodaySaleBranchwise = "GetTodaySaleBranchwise",
                    ManagementBranchwiseExpense = "getManagementBranchwiseExpense",
                    ManagementBranchwiseExpenseChild = "getManagementBranchwiseExpenseChild",
                    ManagementInvoiceReportManagementStatewise = "GetInvoiceReportManagementStatewise",
                    ManagementStatewiseSalesCompare = "GetStatewiseSalesCompare",
                    ManagementStatewiseSaleComparechild = "getStatewiseSaleComparechild",
                    ManagementDistrictwiseSalesCompare = "GetDistrictwiseSalesCompare",
                    ManagementDistrictwiseSaleComparechild = "getDistrictwiseSaleComparechild",
                    ManagementTotalSaleBranchWiseLast = "GetTotalSaleBranchWiseLast",
                    ManagementManagementHeadwiseExpense = "getManagementHeadwiseExpense",
                    ManagementDealerListManagment = "GetDealerListManagment",
                    ManagementDateWiseSale = "GetDateWiseSale",
                    ManagementExpenseChildAll = "getExpenseChildAll",
                    ManagementDivisionListManagementt = "GetDivisionListManagement",
                    ManagementCategoryWiseSalesCompare = "GetCategoryWiseSalesCompare",
                    ManagementCategoryWiseSalesCompareChild = "GetCategoryWiseSalesCompareChild",
                    ManagementLedgerwiseExpense = "getManagementLedgerwiseExpense",
                    ManagementLedgerwiseBranchExpense = "getManagementLedgerwiseBranchExpense",
                    ManagementExpenseChildAllSubChild = "getExpenseChildAllSubChild",
                    ManagementPartyStatus = "getPartyStatus",
                    ManagementPartyDeactive = "PartyDeactive",
                    ManagementVendor = "getManagementVendor",
                    ManagementSupplier = "getManagementSupplier",
                    Managementbranchwisesecuredamt = "getManagementbranchwisesecuredamt",
                    Managementpartywisesecuredamt = "getManagementpartywisesecuredamt",
                    ManagementVendorPurchaseAndLedger = "getVendorPurchaseAndLedger",
                    ManagementSupplierPurchaseAndLedger = "getSupplierPurchaseAndLedger",
                    ManagementVendorPurchaseAndLedgerBalanceInvoiceNo = "getVendorPurchaseAndLedgerBalanceInvoiceNo",
                    ManagementSupplierPurchaseAndLedgerBalanceInvoiceNo = "getSupplierPurchaseAndLedgerBalanceInvoiceNo",
                    ManagementVendorPurchaseAndLedgerBalanceOrderNo = "getVendorPurchaseAndLedgerBalanceOrderNo",
                    ManagementVendorPurchaseAndLedgerBalancePaymente = "getVendorPurchaseAndLedgerBalancePayment",
                    ManagementSupplierPurchaseAndLedgerBalancePayment = "getSupplierPurchaseAndLedgerBalancePayment",
                    ManagementEmployeeList = "getEmployeeList",
                    ManagementEmployeeListDetails = "getEmployeeListDetails",
                    Managementcatwisepurchaseamt = "getcatwisepurchaseamt",
                    Managementdeptwiseemployeecount = "getdeptwiseemployeecount",
                    Managementdepartmentwiseempdetail = "getdepartmentwiseempdetail",
                    Managementdesignationemployeecount = "getdesignationemployeecount",
                    Managementdesignationwiseempdetail = "getdesignationwiseempdetail",
                    Managementjoindatewiseempcount = "getjoindatewiseempcount",
                    Managementjoindatewiseempdata = "getjoindatewiseempdata",
                    Managementmonthwiseempjoincount = "getmonthwiseempjoincount",
                    ManagementlocationEmployeeList = "getlocationEmployeeList",
                    ManagementlocationEmployeeListDetails = "getlocationEmployeeListDetails",
                    ManagementemployeeallList = "getemployeeallList",
                    ManagementemployeedetailList = "getemployeedetailList",
                    Managementbranchwiseattcount = "getbranchwiseattcount",
                    Managementlocationwiseattcount = "getlocationwiseattcount",
                    Managementbranchwiseattdetails = "getbranchwiseattdetails",
                    Managementlocwiseattdetails = "getlocwiseattdetails",




                    divisionSalesReport= "getDivisionSalesReport",
                    dispatchedMaterial= "getDispatchedMaterial",
                    pendingOrders= "getPendingOrders",
                    pendingOrdersPDF= "getPendingOrdersPDF",
                    outstanding= "getOutstanding",
                    priceListCatalogue= "getPriceListCatalogue",
                    outstandingReport= "getOutstandingReport",
                    creditLimit= "getCreditLimit",
                    activeScheme= "getActiveScheme",
                    salesPaymentReport= "getSalesPaymentReport",
                    divisionList= "GetDivisionList",
                    priceList= "GetPriceList",
                    sendEnquiry= "SendEnquiry",
                    sendFeedback= "SendFeedback",
                    subjectList= "GetSubjectList",
                    salesPaymentReportDetails= "getSalesPaymentReportDetails",
                    policy= "getPolicy",
                    technicalSpecification= "getTechnicalSpecification",
                    brandingImages= "getBrandingImages",
                    topProductDealer= "getTopProductDealer",
                    topProductDistrict= "getTopProductDistrict",
                    divisionWiseYSA= "getDivisionWiseYSA",
                    ysAreport= "getYSAreport",
                    lastYearSales= "getLastYearSales",
                    pendingOrderDivisionWise= "getPendingOrderDivisionWise",
                    outstandingDivisionWise= "getOutstndingDivisionWise",
                    schemeDetails= "getSchemeDetails",
                    salesSummary= "getSalesSummary",
                    catalogue= "GetPriceListCatalogue",
                    aging= "getAging",
                    document= "getLoyalty",
                    discoverWorld= "getDiscoverWorld",
                    comboPlaceOrder= "getComboPlaceOrder",
                    comboCompare= "getComboCompare",
                    ledgerAmount= "getLedgerAmount",
                    comboClaim= "getComboClaim",
                    youtubeVideo= "getYoutubeVideo",
                    comboDetails= "getComboDetails",
                    comboSchemes= "getComboSchemes",
                    ledgerAmountDebit= "getLedgerAmountDebit",
                    comboTotalQuantit= "getComboTotalQuantity",
                    sendNotification= "getSendNontification",
                    amountConfirmation= "SpinAmountConfirmation",
                    wheelSpins= "getWheelSpins",
                    wheelSpinsDetails= "getWheelSpinsDetails",
                    applyCN= "ApplyCN",
                    sendNontification= "getSendNontification",
                    creditNoteDetails= "getCreditNoteDetailsExcutive",
                    logToServer= "AddLogToServer",
                    reasonSaleReturnRequest= "getReasonSaleReturnRequest",
                    freePayOTP= "GetFreePayOTP",
                    dealerBankDetails= "GetDealerBankDetails",
                    allpaymenttypedetails= "Getallpaymenttypedetails",
                    allpaymenttypedetailslist= "Getallpaymenttypedetailslist",
                    freepayOutstandingReport= "getFreepayOutstandingReport",
                    checkOutstandingPaymentDetails= "CheckOutstandingPaymentDetails",
                    verifyFreePayOTP= "VerifyFreePayOTP",
                    confirmPaymentRequest= "ConfirmPaymentRequest",
                    resendPaymentRequestOTP= "GetResendPaymentRequestOTP",
                    resendFreePayOTP= "GetResendFreePayOTP",
                    cancelFreePayTranstions= "CancelFreePayTranstions",
                    paymentFreepayRequest= "PaymentFreepayRequest",
                    retryFreepayTransaction= "RetryFreepayTransaction",
                    freePayPayment= "GetFreePayPayment",
                    retryFreepayPaymentTransaction= "RetryFreepayPaymentTransaction",
                    invoicewiseCD= "getInvoicewiseCD",
                    disputeType= "getDisputeType",
                    raiseDispute= "RaiseDispute",
                    mpinchecks= "mpinchecks",
                    orderDivisionAndCat= "getOrderDivisionAndCat",
                    orderItemDetails= "getOrderItemDetails",
                    placeOrder= "PlaceOrder",
                    orderDetails= "GetOrderDetails",
                    orderCatItemDetails= "getOrderCatItemDetails",
                    orderItemCatPriceDetails= "getOrderItemCatPriceDetails",
                    targetScheme= "GetTargetScheme",
                    schemeGrowth= "getSchemeGrowth",
                    salereturnrequestadd= "salereturnrequestadd",
                    salereturnrequestselects= "salereturnrequestselects",
                    salereturnrequestdeletes= "salereturnrequestdeletes",
                    salereturnrequestupdates= "salereturnrequestupdates",
                    salereturnrequestshows= "salereturnrequestshows",
                    salerequestbillingupdate= "salerequestbillingupdate",
                    team= "GetTeam",
                    addPartyFinalMatchSummary= "AddPartyFinalMatchSummary",
                    addPartySemiFinalMatchSummary= "AddPartySemiFinalMatchSummary",
                    saleWorldCup= "GetSaleWorldCup",
                    matchSummary= "GetMatchSummary",
                    addPartyMatchSummary= "AddPartyMatchSummary",
                    showRoom= "getShowRoom",
                    unreadNotificationCount= "UnreadNotificationCount",
                    addLogToRead= "AddLogToRead",
                    mpinadds= "mpinadds",
                    activeDevice= "getActiveDevice",
                    activeDeviceLogout= "ActiveDeviceLogout",
                    analyticsDataadd= "getAnalyticsDataadd",
                    addLogToServer= "AddLogToServer",
                    forgetMpin= "ForgetMpin",
                    matchSummaryExpertOpenion= "GetMatchSummaryExpertOpenion",
                    partyExpertOpenionPredictions= "PartyWiseExpertOpenionPredictions",
                    saleWorldCuppartB= "GetSaleWorldCuppartB",
                    freePayFailedTranstions= "CheckFreePayFailedTranstions",
                    todList= "getTodList",
                    todSalesInfo= "getTODSalesInfo",
                    submitGroupTOD= "submitGroupTOD",
                    appBanner= "GetAppBanner",
                    dhanbarseQwikpayVideo= "getDhanbarseQwikpayVideo",
                    dhanbarseQwikpayPriceList= "GetDhanbarseQwikpayPriceList",
                    directdealerspin= "getdirectdealerspin",
                    directDealerSpinAmountConfirmation= "DirectDealerSpinAmountConfirmation",
                    subcatimgdivwise= "getsubcatimgdivwise",
                    
                  
                });

                // g1.close_connection();
                alldcr.Add(new InitialValuesMs
                {
                    result = true,
                    servertime = DateTime.Now.ToString(),
                    message = string.Empty,
                    data = alldcr1
                });
                data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}