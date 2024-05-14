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


        [Route("bin")]
        [HttpPost]
        public HttpResponseMessage GetBinDetails(GetBinDetails objGetBinDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetBinDetails(objGetBinDetails);
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


        [Route("stock")]
        [HttpPost]
        public HttpResponseMessage GetStockDetails(StockDetails objStockDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            QRAppDetails objQRAppDetails = new QRAppDetails();
            Response = objQRAppDetails.GetStockDetails(objStockDetails);
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
    }

}
