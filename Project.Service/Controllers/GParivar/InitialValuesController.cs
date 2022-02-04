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

namespace Project.Service
{
    public class InitialValuesController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getInitialValue")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<InitialValues> alldcr = new List<InitialValues>();
                List<InitialValue> alldcr1 = new List<InitialValue>();

                var data = g1.return_dt("dbo.InitialApi");

                alldcr1.Add(new InitialValue
                {
                    versionCode = data.Rows[0]["versionCode"].ToString(),
                    versionNumber = data.Rows[0]["versionNumber"].ToString(),
                    iosVersion = data.Rows[0]["iosVersion"].ToString(),


                    forceUpdate = Convert.ToBoolean(data.Rows[0]["forceUpdate"]),
                    forceLogout = Convert.ToBoolean(data.Rows[0]["forceLogout"]),
                    NewFeature = Convert.ToBoolean(data.Rows[0]["NewFeature"]),
                    AndroidVisible = Convert.ToBoolean(data.Rows[0]["AndroidVisible"]),
                    iosvisible = Convert.ToBoolean(data.Rows[0]["iosvisible"]),
                    AlertMessage = data.Rows[0]["AlertMessage"].ToString(),



                    divisionLastUpdated = "12/19/2019",
                    enquiryLastUpdated = "09/20/2018",


                    BaseApi = WebConfigurationManager.AppSettings["ApiUrl"].ToString(),


                    ValidateUserDealer = "ValidateUserDealer",
                    ValidateCIN = "ValidateCIN",
                    VerifyOTP = "VerifyOTP",
                    SetPassword = "SetPassword",
                    DivisionSalesReport = "getDivisionSalesReport",
                    DispatchedMaterial = "getDispatchedMaterial",
                    PendingOrders = "getPendingOrders",
                    PendingOrdersPDF = "getPendingOrdersPDF",
                    Outstanding = "getOutstanding",
                    PriceListCatalogue = "getPriceListCatalogue",
                    OutstandingReport = "getOutstandingReport",
                    CreditLimit = "getCreditLimit",
                    ActiveScheme = "getActiveScheme",
                    SalesPaymentReport = "getSalesPaymentReport",
                    DivisionList = "GetDivisionList",
                    PriceList = "GetPriceList",
                    SendEnquiry = "SendEnquiry",
                    SendFeedback = "SendFeedback",
                    SubjectList = "GetSubjectList",
                    SalesPaymentReportDetails = "getSalesPaymentReportDetails",
                    Policy = "getPolicy",
                    TechnicalSpecification = "getTechnicalSpecification",
                    BrandingImages = "getBrandingImages",
                    TopProductDealer = "getTopProductDealer",
                    TopProductDistrict = "getTopProductDistrict",
                    DivisionWiseYSA = "getDivisionWiseYSA",
                    YSAreport = "getYSAreport",
                    LastYearSales = "getLastYearSales",
                    PendingOrderDivisionWise = "getPendingOrderDivisionWise",
                    OutstandingDivisionWise = "getOutstndingDivisionWise",
                    SchemeDetails = "getSchemeDetails",
                    SalesSummary = "getSalesSummary",
                    Catalogue = "GetPriceListCatalogue",
                    Aging = "getAging",
                    Document = "getLoyalty",
                    DiscoverWorld = "getDiscoverWorld",
                    ComboPlaceOrder = "getComboPlaceOrder",
                    ComboCompare = "getComboCompare",
                    LedgerAmount = "getLedgerAmount",
                    ComboClaim = "getComboClaim",
                    YoutubeVideo = "getYoutubeVideo",
                    ComboDetails = "getComboDetails",
                    ComboSchemes = "getComboSchemes",
                    LedgerAmountDebit = "getLedgerAmountDebit",
                    ComboTotalQuantit = "getComboTotalQuantity",
                    SendNotification = "getSendNontification",
                    AmountConfirmation = "SpinAmountConfirmation",
                    WheelSpins = "getWheelSpins",
                    WheelSpinsDetails = "getWheelSpinsDetails",
                    ApplyCN = "ApplyCN",
                    SendNontification = "getSendNontification",
                    CreditNoteDetails = "getCreditNoteDetailsExcutive",
                    LogToServer = "AddLogToServer",
                    ReasonSaleReturnRequest = "getReasonSaleReturnRequest",
                    FreePayOTP = "GetFreePayOTP",
                    DealerBankDetails = "GetDealerBankDetails",
                    allpaymenttypedetails = "Getallpaymenttypedetails",
                    allpaymenttypedetailslist = "Getallpaymenttypedetailslist",
                    FreepayOutstandingReport = "getFreepayOutstandingReport",
                    CheckOutstandingPaymentDetails = "CheckOutstandingPaymentDetails",
                    VerifyFreePayOTP = "VerifyFreePayOTP",
                    ConfirmPaymentRequest = "ConfirmPaymentRequest",
                    ResendPaymentRequestOTP = "GetResendPaymentRequestOTP",
                    ResendFreePayOTP = "GetResendFreePayOTP",
                    CancelFreePayTranstions = "CancelFreePayTranstions",
                    PaymentFreepayRequest = "PaymentFreepayRequest",
                    RetryFreepayTransaction = "RetryFreepayTransaction",
                    FreePayPayment = "GetFreePayPayment",
                    RetryFreepayPaymentTransaction = "RetryFreepayPaymentTransaction",
                    InvoicewiseCD = "getInvoicewiseCD",
                    DisputeType = "getDisputeType",
                    RaiseDispute = "RaiseDispute",
                    mpinchecks = "mpinchecks",
                    OrderDivisionAndCat = "getOrderDivisionAndCat",
                    OrderItemDetails = "getOrderItemDetails",
                    PlaceOrder = "PlaceOrder",
                    OrderDetails = "GetOrderDetails",
                    OrderCatItemDetails = "getOrderCatItemDetails",
                    OrderItemCatPriceDetails = "getOrderItemCatPriceDetails",
                    TargetScheme = "GetTargetScheme",
                    SchemeGrowth = "getSchemeGrowth",
                    salereturnrequestadd = "salereturnrequestadd",
                    salereturnrequestselects = "salereturnrequestselects",
                    salereturnrequestdeletes = "salereturnrequestdeletes",
                    salereturnrequestupdates = "salereturnrequestupdates",
                    salereturnrequestshows = "salereturnrequestshows",
                    salerequestbillingupdate = "salerequestbillingupdate",
                    Team = "GetTeam",
                    AddPartyFinalMatchSummary = "AddPartyFinalMatchSummary",
                    AddPartySemiFinalMatchSummary = "AddPartySemiFinalMatchSummary",
                    SaleWorldCup = "GetSaleWorldCup",
                    MatchSummary = "GetMatchSummary",
                    AddPartyMatchSummary = "AddPartyMatchSummary",
                    ShowRoom = "getShowRoom",
                    UnreadNotificationCount = "UnreadNotificationCount",
                    AddLogToRead = "AddLogToRead",
                    mpinadds = "mpinadds",
                    ActiveDevice = "getActiveDevice",
                    ActiveDeviceLogout = "ActiveDeviceLogout",
                    AnalyticsDataadd = "getAnalyticsDataadd",
                    AddLogToServer = "AddLogToServer",
                    ForgetMpin = "ForgetMpin",
                    MatchSummaryExpertOpenion = "GetMatchSummaryExpertOpenion",
                    PartyExpertOpenionPredictions = "PartyWiseExpertOpenionPredictions",
                    SaleWorldCuppartB = "GetSaleWorldCuppartB",
                    FreePayFailedTranstions = "CheckFreePayFailedTranstions",
                    TodList = "getTodList",
                    TODSalesInfo = "getTODSalesInfo",
                    submitGroupTOD = "submitGroupTOD",
                    AppBanner = "GetAppBanner",
                    DhanbarseQwikpayVideo = "getDhanbarseQwikpayVideo",
                    DhanbarseQwikpayPriceList = "GetDhanbarseQwikpayPriceList",
                    directdealerspin = "getdirectdealerspin",
                    DirectDealerSpinAmountConfirmation = "DirectDealerSpinAmountConfirmation",
                    subcatimgdivwise = "getsubcatimgdivwise",
                    getSpinDataApi = "getdealerluckdrawdetail",
                    setSpinDataApi = "getdealerluckdrawspin",
                    ticketListApi = "getluckydrawtickdetail",
                    winnerListApi = "getluckydrawwinnerlist",
                    PayUOutStandingPayload = "PayUOutStandingPayload",
                    payupaymentverify = "payu-payment-verify/txnid",
                    getfancategory = "getfancategory",
                    fancobmoadd = "fancobmoadd",
                    gettcsdata = "gettcsdata",
                    confirmtcsdata = "confirmtcsdata",
                    userdataadd = "userdataadd",
                    getBannerImages = "getBannerImages",
                    getinvoicepod = "getinvoicepod",
                    addpod = "addpod",
                    ledgersignview = "manch/ledger-sign-view",
                    ledgersignviewreport = "manch/ledger-sign-viewreport",
                    makepayloadtosign = "manch/make-payload-to-sign",
                    NewYearScheme = "getNewYearScheme",
                    DeactivateDealerBank = "DeactivateDealerBank",
                    DeactivateDealerBankHistory = "DeactivateDealerBankHistory",

                   
                });

                // g1.close_connection();
                alldcr.Add(new InitialValues
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