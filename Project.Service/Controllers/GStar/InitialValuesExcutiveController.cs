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
    public class InitialValuesExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getInitialValueExcutive")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<InitialValueExs> alldcr = new List<InitialValueExs>();
                List<InitialValueEx> alldcr1 = new List<InitialValueEx>();

                var data = g1.return_dt("dbo.InitialApiExe");

                alldcr1.Add(new InitialValueEx
                {
                    versionCode = data.Rows[0]["versionCode"].ToString(),
                    versionNumber = data.Rows[0]["versionNumber"].ToString(),
                    iosVersion = data.Rows[0]["iosVersion"].ToString(),

                    divisionLastUpdated = "08/13/2019",
                    dealerByExecutiveLastUpdated = "04/08/2020",
                    enquiryLastUpdated = "08/13/2019",


                    BaseApi = WebConfigurationManager.AppSettings["ApiUrl"].ToString(),


                    InitialValueExcutive = "getInitialValueExcutive",
                    ValidateUserDealer = "ValidateUserDealer",
                    ValidateCIN = "ValidateCIN",
                    VerifyOTP = "VerifyOTP",
                    SetPassword = "SetPassword",
                    SalesExecutive = "getSalesExecutive",
                    SalesPaymentReportExcutive = "getSalesPaymentReportExcutive",
                    SalesExcutiveAging = "getSalesExcutiveAging",
                    SchemeDownloadExcutive = "getSchemeDownloadExcutive",
                    DealerContactExcutive = "getDealerContactExcutive",
                    ChequeReturnExecutive = "getChequeReturnExecutive",
                    LastPaymentExecutive = "getLastPaymentExecutive",
                    DealerByEx = "getDealerByEx",
                    ExByEx = "getExByEx",
                    ActiveScheme = "getActiveScheme",
                    PendingOrderDivisionWiseExcutive = "getPendingOrderDivisionWiseExcutive",
                    SchemeUploadExcutive = "getSchemeUploadExcutive",
                    SchemeUploaddetails = "getSchemeUploaddetails",
                    EmployeeContact = "getEmployeeContact",
                    LedgerAmount = "getLedgerAmount",
                    LedgerAmountDebit = "getLedgerAmountDebit",
                    CategoryWiseSaleExcutive = "getCategoryWiseSaleExcutive",
                    BlockedParty = "getBlockedParty",
                    PaymentExecutive = "getPaymentExecutive",
                    StockAvailibilityExcutive = "getStockAvailibilityExcutive",
                    ItemByDivision = "getItemByDivision",
                    TODMDWDExcutive = "getTODMDWDExcutive",
                    DealerDetails = "getDealerDetails",
                    AddLatLong = "AddLatLong",
                    mpinchecks = "mpinchecks",
                    ForgetMpin = "ForgetMpin",
                    PlaceOrder = "PlaceOrder",
                    getOrderItemDetails = "getOrderItemDetails",
                    getOrderDivisionAndCat = "getOrderDivisionAndCat",
                    getOrderItemCatPriceDetails = "getOrderItemCatPriceDetails",
                    getOrderCatItemDetails = "getOrderCatItemDetails",
                    GetLatLong = "GetLatLong",
                    AnalyticsDataadd = "getAnalyticsDataadd",
                    Organation = "getOrganation",
                    ExeAttList = "GetExeAttList",
                    DivisionWiseExTarget = "getDivisionWiseExTarget",
                    ExecCheckInCheckOutList = "GetExecCheckInCheckOutList",
                    Executivepresent = "IsExecutivepresent",
                    ExeLatLngList = "GetExeLatLngList",
                    DcrCatandAreaList = "GetDcrCatandAreaList",
                    LatLonOrganationAdd = "LatLonOrganationAdd",
                    LatLonOrganationUpdate = "LatLonOrganationUpdate",
                    LatLonOrganisationList = "GetLatLonOrganisationList",
                    AddExecCheckINCheckOut = "AddExecCheckINCheckOut",
                    salereturnrequestselectexecwise = "Getsalereturnrequestselectexecwise",
                    ExecutiveSchduleList = "GetExecutiveSchduleList",
                    DcrOrganationList = "GetDcrOrganationList",
                    salereturnrequestitemshow = "Getsalereturnrequestitemshow",
                    ItemByDivisionEx = "getItemByDivisionEx",
                    NearByOrgList = "GetNearByOrgList",
                    ExecutivePresentAbsentList = "GetExecutivePresentAbsentList",
                    DcrInputExcutive = "getDcrInputExcutive",
                    InsertDCRExecutive = "InsertDCRExecutive",
                    salereturnrequestfinalize = "salereturnrequestfinalize",
                    ItemByDivisionDetailsEx = "getItemByDivisionDetailsEx",
                    ExecutiveDCRList = "GetExecutiveDCRList",
                    OrgAddDetailsExcutive = "getOrgAddDetailsExcutive",
                    InsertDCRScheduleExecutive = "InsertDCRScheduleExecutive",
                    Addsalereturnrequestitem = "Addsalereturnrequestitem",
                    LastYearSales = "getLastYearSales",
                    BrandingImages = "getBrandingImages",
                    DivisionWiseYSA = "getDivisionWiseYSA",
                    YSAreport = "getYSAreport",
                    PendingOrderDivisionWise = "getPendingOrderDivisionWise",
                    PendingOrdersPDF = "getPendingOrdersPDF",
                    DispatchedMaterial = "getDispatchedMaterialExcutive",
                    SalesPaymentReport = "getSalesPaymentReport",
                    SalesPaymentReportDetails = "getSalesPaymentReportDetails",
                    AgingReport = "getAging",
                    OutstandingDivisionWise = "getOutstndingDivisionWise",
                    SchemeDetails = "getSchemeDetails",
                    DebitNote = "getDebitNoteExcutive",
                    CreditNote = "getCreditNoteExcutive",
                    StarRewards = "getSalesSummary",
                    DiscoverTheWorld = "getDiscoverWorld",
                    Policy = "getPolicy",
                    PriceList = "GetPriceList",
                    Catalogue = "GetPriceListCatalogue",
                    TechnicalSpecification = "getTechnicalSpecification",
                    SendNotification = "getSendNontification",
                    SendFeedback = "SendFeedback",
                    DivisionList = "GetDivisionList",
                    OutstandingPayment = "getOutstandingReportExcutive",
                    ComboClaim = "getComboClaim",
                    SalesandTargetExcutive = "getSalesandTargetExcutive",
                    CreditNoteDetails = "getCreditNoteDetailsExcutive",
                    CategoryWiseFilter = "getCategoryWiseFilter",
                    PendingOrders = "getPendingOrders",
                    allpaymenttypedetails = "Getallpaymenttypedetails",
                    allpaymenttypedetailslist = "Getallpaymenttypedetailslist",
                    mpinadd = "mpinadds",
                    OrderDetails = "GetOrderDetailsExecutive",
                    ActiveDevice = "getActiveDeviceExecutive",
                    ActiveDeviceLogout = "ActiveDeviceLogoutExecutive",
                    ExecutiveWisePartyMatchSelection = "GetExecutiveWisePartyMatchSelection",
                    ShowAllExpensesByExecutive = "ShowAllExpensesByExecutive",
                    ExpenseType = "getExpenseType",
                    ExpenseAfterDCRFillup = "ExpenseAfterDCRFillup",
                    UnplannedExpensesExecutive = "UnplannedExpensesExecutive",
                    ActiveSchemeExecutive = "getActiveSchemeExecutive",
                    TODSalesExecutive = "getTODSalesExecutive",
                    TODGroupSalesExecutive = "getTODGroupSalesExecutive",
                    FreePayPayment = "GetFreePayPayment",
                    TodList = "getTodList",
                    TODSalesInfo = "getTODSalesInfo",
                    subcatimgdivwise = "subcatimgdivwise",
                    YoutubeVideo = "getYoutubeVideo",
                    ServerTime = "getServerTime",
                    CategoryDivisionWise= "getCategoryDivisionWise",
                    netlandingcatwise= "getnetlandingcatwise",
                    execpartywiseitemqty = "getexecpartywiseitemqty",
                    netlandinglikeitem= "getnetlandinglikeitem",

                    getleadtype = "getleadtype",
                    getvisitortype = "getvisitortype",
                    getenquirytype = "getenquirytype",
                    AddEnquiryDetails = "AddEnquiryDetails",
                    AddVisitorDetails = "AddVisitorDetails",
                    getlistofcitystate = "getlistofcitystate",
                    getlistofvisitor = "getlistofvisitor",
                    getsubcatlistforvisitor = "getsubcatlistforvisitor",
                    getitemlistforvisitor = "getitemlistforvisitor",
                    getlistofvisitorenquiry = "getlistofvisitorenquiry",

                    forceUpdate = Convert.ToBoolean(data.Rows[0]["forceUpdate"]),
                    enableNetLanding = Convert.ToBoolean(data.Rows[0]["enableNetLanding"]),
                    showAnnouncements= Convert.ToBoolean(data.Rows[0]["showAnnouncements"]),
                    showMenu = Convert.ToBoolean(data.Rows[0]["showMenu"]),
                    showWorldcup= Convert.ToBoolean(data.Rows[0]["showWorldcup"]),
                    useplacesapi = Convert.ToBoolean(data.Rows[0]["useplaceapi"]),
                    showLeadGenerationMenu = Convert.ToBoolean(data.Rows[0]["showLeadGenerationMenu"])
                });

                // g1.close_connection();
                alldcr.Add(new InitialValueExs
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