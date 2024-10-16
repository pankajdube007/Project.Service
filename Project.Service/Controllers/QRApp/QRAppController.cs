using Project.Service.Models.QRApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Project.Service.Controllers.QRApp
{


    [RoutePrefix("api/qrapp")]
    public class QRAppController : ApiController
    {

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage LoginValidate(LoginValidate objLoginValidate)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.LoginValidate(objLoginValidate);
            return Response;
        }

        [Route("branch")]
        [HttpPost]
        public HttpResponseMessage GetBranch(MasterDetails objMasterDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetBranch(objMasterDetails);
            return Response;
        }

        [Route("vendor")]
        [HttpPost]
        public HttpResponseMessage GetVendorDetails(GetVendorDetails objGetVendorDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetVendorDetails(objGetVendorDetails);
            return Response;
        }


        [Route("purchasein")]
        [HttpPost]
        public HttpResponseMessage GetPurInDetails(GetPurInDetails objGetPurInDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetPurInDetails(objGetPurInDetails);
            return Response;
        }

        [Route("party")]
        [HttpPost]
        public HttpResponseMessage GetPartyDetails(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetPartyDetails(objGetPartyDetails);
            return Response;
        }


        [Route("dcdetails")]
        [HttpPost]
        public HttpResponseMessage GetDCDetails(GetDCDetails objGetDCDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDCDetails(objGetDCDetails);
            return Response;
        }

        [Route("warehouse")]
        [HttpPost]
        public HttpResponseMessage GetWarehouseDetails(GetWarehouseDetails objGetWarehouseDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetWarehouseDetails(objGetWarehouseDetails);
            return Response;
        }

        [Route("division")]
        [HttpPost]
        public HttpResponseMessage GetDivisionDetails(GetDivisionDetails objGetDivisionDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDivisionDetails(objGetDivisionDetails);
            return Response;
        }

        [Route("product")]
        [HttpPost]
        public HttpResponseMessage GetProductDetails(GetProductDetails objGetProductDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetProductDetails(objGetProductDetails);
            return Response;
        }


        [Route("wbin")]
        [HttpPost]
        public HttpResponseMessage GetBinDetails(GetBinDetails objGetBinDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetBinDetails(objGetBinDetails);
            return Response;
        }


        [Route("grnbranch")]
        [HttpPost]
        public HttpResponseMessage GetGRNINBranchDetails(GetGRINUserDetails objGetGRINUserDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetGRNINBranchDetails(objGetGRINUserDetails);
            return Response;
        }


        [Route("grndetails")]
        [HttpPost]
        public HttpResponseMessage GetGRNInDetails(GetGRINDetails objGetGRINDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetGRNInDetails(objGetGRINDetails);
            return Response;
        }



        [Route("invoicebranch")]
        [HttpPost]
        public HttpResponseMessage GetInvoiceBranchDetails(GetGRINUserDetails objGetGRINUserDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetInvoiceBranchDetails(objGetGRINUserDetails);
            return Response;
        }


        [Route("invoicedetails")]
        [HttpPost]
        public HttpResponseMessage GetInvoiceDetails(GetGRINDetails objGetGRINDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetInvoiceDetails(objGetGRINDetails);
            return Response;
        }


        [Route("postinvoice")]
        [HttpPost]
        public HttpResponseMessage PostInvoiceData(PostGRNINDetailslst objPostGRNINDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostInvoiceData(objPostGRNINDetailslst);
            return Response;
        }


        [Route("stocktransfertype")]
        [HttpPost]
        public HttpResponseMessage GetSTFTypeDetails(GetGRINUserDetails objGetGRINUserDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetSTFTypeDetails(objGetGRINUserDetails);
            return Response;
        }

        [Route("stocktransferrefno")]
        [HttpPost]
        public HttpResponseMessage GetSTFRefNo(STFRefnoRequest objSTFRefnoData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetSTFRefno(objSTFRefnoData);
            return Response;
        }

        [Route("salesreturnparty")]
        [HttpPost]
        public HttpResponseMessage GetSLRParty(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetSLRParty(objGetPartyDetails);
            return Response;
        }

        [Route("salesresturnrefno")]
        [HttpPost]
        public HttpResponseMessage GetSLRRefNo(GetDCDetails objGetDCDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetSLRRefNo(objGetDCDetails);
            return Response;
        }

        [Route("purchasereturnparty")]
        [HttpPost]
        public HttpResponseMessage GetPIRParty(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetPIRParty(objGetPartyDetails);
            return Response;
        }

        [Route("purchasereturnrefno")]
        [HttpPost]
        public HttpResponseMessage GetPIRRefNo(GetDCDetails objGetDCDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetPIRRefNo(objGetDCDetails);
            return Response;
        }


        [Route("stocktransferpost")]
        [HttpPost]
        public HttpResponseMessage PostSTData(PostSTDetailslst objPostSTDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostSTData(objPostSTDetailslst);
            return Response;
        }

        [Route("purchasereturnpost")]
        [HttpPost]
        public HttpResponseMessage PostPIRData(PostPIRDetailslst objPostPIRDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostPIRData(objPostPIRDetailslst);
            return Response;
        }

        [Route("salesresturnpost")]
        [HttpPost]
        public HttpResponseMessage PostSLRData(PostSLRDetailslst objPostSLRDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostSLRData(objPostSLRDetailslst);
            return Response;
        }



        [Route("searchdetails")]
        [HttpPost]
        public HttpResponseMessage SearchDetails(SearchDetails objSearchDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.SearchDetails(objSearchDetails);
            return Response;
        }


        [Route("qrdata")]
        [HttpPost]
        public HttpResponseMessage QRDetails(QRDetails objQRDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.QRDetails(objQRDetails);
            return Response;
        }

        [Route("qrdatabintransfer")]
        [HttpPost]
        public HttpResponseMessage QRBINDetails(QRDetails objQRDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.QRBINDetails(objQRDetails);
            return Response;
        }


        [Route("palletdetails")]
        [HttpPost]
        public HttpResponseMessage GetOutPalletDetails(PalletOutRequest objPalletOutRequest)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetOutPalletDetails(objPalletOutRequest);
            return Response;
        }



        [Route("stock")]
        [HttpPost]
        public HttpResponseMessage GetStockDetails(StockDetails objStockDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetStockDetails(objStockDetails);
            return Response;
        }


        [Route("releaserestrictpurindevice")]
        [HttpPost]
        public HttpResponseMessage ReleaseRestrictDevicePurchaseIn(ReleaseDetails objReleaseDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.ReleaseRestrictDevicePurchaseIn(objReleaseDetails);
            return Response;
        }


        [Route("postbintransfer")]
        [HttpPost]
        public HttpResponseMessage PostBinTransferData(PostBinTransferlst objPostBinTransferlst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostBinTransferData(objPostBinTransferlst);
            return Response;
        }



        [Route("postwarehouse")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseData(PostWarehouseDetailslst objPostWarehouseDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostWarehouseData(objPostWarehouseDetailslst);
            return Response;
        }

        [Route("postpurchasein")]
        [HttpPost]
        public HttpResponseMessage PostPurchaseInData(PostPurchaseINDetailslst objPostPurchaseINDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostPurchaseInData(objPostPurchaseINDetailslst);
            return Response;
        }


        [Route("postdc")]
        [HttpPost]
        public HttpResponseMessage PostDCData(PostDCDetailslst objPostDCDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostDCData(objPostDCDetailslst);
            return Response;
        }


        [Route("postgrnin")]
        [HttpPost]
        public HttpResponseMessage PostGRNInData(PostGRNINDetailslst objPostGRNINDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostGRNInData(objPostGRNINDetailslst);
            return Response;
        }


        [Route("poststockmatch")]
        [HttpPost]
        public HttpResponseMessage PostStockMatchData(PostWarehouseDetailslst objPostWarehouseDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostStockMatchData(objPostWarehouseDetailslst);
            return Response;
        }


        [Route("qrstatus")]
        [HttpPost]
        public HttpResponseMessage GetQRStatus(GetQRDetails objGetQRDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetQRStatus(objGetQRDetails);
            return Response;
        }





        [Route("getvendorinvoice")]
        [HttpPost]
        public HttpResponseMessage GetVendorInvoice(GetVendorInvoice objGetVendorInvoice)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetVendorInvoice(objGetVendorInvoice);
            return Response;
        }


        [Route("createvendorinvoice")]
        [HttpPost]
        public HttpResponseMessage CreateVendorInvoice(GetVendorInvoice objGetVendorInvoice)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.CreateVendorInvoice(objGetVendorInvoice);
            return Response;
        }


        [Route("vendorinvoicedetails")]
        [HttpPost]
        public HttpResponseMessage GetVendorInvoiceDetails(GetVendorInvoiceDetails objGetVendorInvoiceDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetVendorInvoiceDetails(objGetVendorInvoiceDetails);
            return Response;
        }


        [Route("vendorqrdetails")]
        [HttpPost]
        public HttpResponseMessage GetVendorQRDetails(GetVendorQrDetails objGetVendorQrDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetVendorQRDetails(objGetVendorQrDetails);
            return Response;
        }

        [Route("postvendorinvoice")]
        [HttpPost]
        public HttpResponseMessage PostVendorQRDetails(PostVendorQRDetailslst objPostVendorQRDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostVendorQRDetails(objPostVendorQRDetailslst);
            return Response;
        }











        [Route("dcwarehouse")]
        [HttpPost]
        public HttpResponseMessage GetDCWarehouseDetails(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDCWarehouseDetails(objGetPartyDetails);
            return Response;
        }



        [Route("dcparty")]
        [HttpPost]
        public HttpResponseMessage GetDCPartyDetails(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDCPartyDetails(objGetPartyDetails);
            return Response;
        }


        [Route("dcdivision")]
        [HttpPost]
        public HttpResponseMessage GetDCDivisionDetails(GetPartyDetails objGetPartyDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDCDivisionDetails(objGetPartyDetails);
            return Response;
        }


        [Route("dcitemlist")]
        [HttpPost]
        public HttpResponseMessage GetDCItemDetails(DCItemRequest objDCItemRequest)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetDCItemDetails(objDCItemRequest);
            return Response;
        }

        [Route("dcpost")]
        [HttpPost]
        public HttpResponseMessage PostDCCreateData(PostDCCreateDetailslst objPostDCCreateDetailslst)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.PostDCCreateData(objPostDCCreateDetailslst);
            return Response;
        }


    }

}
