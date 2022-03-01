﻿using System.Collections.Generic;

namespace Project.Service.Models
{
    public class InitialValues
    {
        public bool result { get; set; }
        public string servertime { get; set; }
        public string message { get; set; }
        public List<InitialValue> data { get; set; }
    }

    public class InitialValue
    {
        public string versionCode { get; set; }
        public string versionNumber { get; set; }
        public string iosVersion { get; set; }
        public bool forceUpdate { get; set; }
        public bool forceLogout { get; set; }
        public bool NewFeature { get; set; }
        public bool AndroidVisible { get; set; }
        public bool iosvisible { get; set; }
        public string AlertMessage { get; set; }
        public string divisionLastUpdated { get; set; }
        public string enquiryLastUpdated { get; set; }
        public string BaseApi { get; set; }
        public string ValidateUserDealer { get; set; }
        public string ValidateCIN { get; set; }
        public string VerifyOTP { get; set; }
        public string SetPassword { get; set; }
        public string DivisionSalesReport { get; set; }
        public string DispatchedMaterial { get; set; }
        public string PendingOrders { get; set; }
        public string PendingOrdersPDF { get; set; }
        public string Outstanding { get; set; }
        public string PriceListCatalogue { get; set; }
        public string OutstandingReport { get; set; }
        public string CreditLimit { get; set; }
        public string ActiveScheme { get; set; }
        public string SalesPaymentReport { get; set; }
        public string DivisionList { get; set; }
        public string PriceList { get; set; }
        public string SendEnquiry { get; set; }
        public string SendFeedback { get; set; }
        public string SubjectList { get; set; }
        public string SalesPaymentReportDetails { get; set; }
        public string Policy { get; set; }
        public string TechnicalSpecification { get; set; }
        public string BrandingImages { get; set; }
        public string TopProductDealer { get; set; }
        public string TopProductDistrict { get; set; }
        public string DivisionWiseYSA { get; set; }
        public string YSAreport { get; set; }
        public string LastYearSales { get; set; }
        public string PendingOrderDivisionWise { get; set; }
        public string OutstandingDivisionWise { get; set; }
        public string SchemeDetails { get; set; }
        public string SalesSummary { get; set; }
        public string Catalogue { get; set; }
        public string Aging { get; set; }
        public string Document { get; set; }
        public string DiscoverWorld { get; set; }
        public string ComboPlaceOrder { get; set; }
        public string ComboCompare { get; set; }
        public string LedgerAmount { get; set; }
        public string ComboClaim { get; set; }
        public string YoutubeVideo { get; set; }
        public string ComboDetails { get; set; }
        public string ComboSchemes { get; set; }
        public string LedgerAmountDebit { get; set; }
        public string ComboTotalQuantit { get; set; }
        public string SendNotification { get; set; }
        public string AmountConfirmation { get; set; }
        public string WheelSpins { get; set; }
        public string WheelSpinsDetails { get; set; }
        public string ApplyCN { get; set; }
        public string SendNontification { get; set; }
        public string CreditNoteDetails { get; set; }
        public string LogToServer { get; set; }

        public string ReasonSaleReturnRequest { get; set; }
        public string FreePayOTP { get; set; }
        public string DealerBankDetails { get; set; }
        public string allpaymenttypedetails { get; set; }
        public string allpaymenttypedetailslist { get; set; }
        public string FreepayOutstandingReport { get; set; }
        public string CheckOutstandingPaymentDetails { get; set; }
        public string VerifyFreePayOTP { get; set; }
        public string ConfirmPaymentRequest { get; set; }
        public string ResendPaymentRequestOTP { get; set; }
        public string ResendFreePayOTP { get; set; }
        public string CancelFreePayTranstions { get; set; }
        public string PaymentFreepayRequest { get; set; }
        public string RetryFreepayTransaction { get; set; }
        public string FreePayPayment { get; set; }
        public string RetryFreepayPaymentTransaction { get; set; }
        public string InvoicewiseCD { get; set; }
        public string DisputeType { get; set; }
        public string RaiseDispute { get; set; }
        public string mpinchecks { get; set; }
        public string OrderDivisionAndCat { get; set; }
        public string OrderItemDetails { get; set; }
        public string PlaceOrder { get; set; }
        public string OrderDetails { get; set; }
        public string OrderCatItemDetails { get; set; }
        public string OrderItemCatPriceDetails { get; set; }
        public string TargetScheme { get; set; }
        public string SchemeGrowth { get; set; }
        public string salereturnrequestadd { get; set; }
        public string salereturnrequestselects { get; set; }
        public string salereturnrequestdeletes { get; set; }
        public string salereturnrequestupdates { get; set; }
        public string salereturnrequestshows { get; set; }
        public string salerequestbillingupdate { get; set; }
        public string Team { get; set; }
        public string AddPartyFinalMatchSummary { get; set; }
        public string AddPartySemiFinalMatchSummary { get; set; }
        public string SaleWorldCup { get; set; }
        public string MatchSummary { get; set; }
        public string AddPartyMatchSummary { get; set; }
        public string ShowRoom { get; set; }
        public string UnreadNotificationCount { get; set; }
        public string AddLogToRead { get; set; }
        public string mpinadds { get; set; }
        public string ActiveDevice { get; set; }
        public string ActiveDeviceLogout { get; set; }
        public string AnalyticsDataadd { get; set; }
        public string AddLogToServer { get; set; }
        public string ForgetMpin { get; set; }
        public string MatchSummaryExpertOpenion { get; set; }
        public string PartyExpertOpenionPredictions { get; set; }
        public string SaleWorldCuppartB { get; set; }
        public string FreePayFailedTranstions { get; set; }
        public string TodList { get; set; }
        public string TODSalesInfo { get; set; }
        public string submitGroupTOD { get; set; }
        public string AppBanner { get; set; }
        public string DhanbarseQwikpayVideo { get; set; }
        public string DhanbarseQwikpayPriceList { get; set; }
        public string directdealerspin { get; set; }
        public string DirectDealerSpinAmountConfirmation { get; set; }
        public string subcatimgdivwise { get; set; }
        public string getSpinDataApi { get; set; }
        public string setSpinDataApi { get; set; }
        public string ticketListApi { get; set; }
        public string winnerListApi { get; set; }
        public string PayUOutStandingPayload { get; set; }
        public string payupaymentverify { get; set; }
        public string getfancategory { get; set; }
        public string fancobmoadd { get; set; }
        public string gettcsdata { get; set; }
        public string confirmtcsdata { get; set; }
        public string userdataadd { get; set; }
        public string getBannerImages { get; set; }
        public string getinvoicepod { get; set; }
        public string addpod { get; set; }
        public string ledgersignview { get; set; }
        public string ledgersignviewreport { get; set; }
        public string makepayloadtosign { get; set; }
        public string NewYearScheme { get; set; }
        public string DeactivateDealerBank { get; set; }
        public string DeactivateDealerBankHistory { get; set; }
        public string ExecutiveWiseNewYearBonaza { get; set; }
        public string verifysigndocument { get; set; }
        public string ValidateVendorPayment { get; set; }
    }
}