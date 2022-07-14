using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers.Vendor
{
    public class VendorQRController : ApiController
    {
        VendorQRDetails objVendorQRDetails = new VendorQRDetails();

        [HttpPost]
        public HttpResponseMessage GetQRDetails(GetQRData objGetQRData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            Response = objVendorQRDetails.GetQRDetails(objGetQRData);
            return Response;
        }

        [HttpPost]
        public HttpResponseMessage PostQRMapingDetails(PostQRMapingData objPostQRMapingData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            Response = objVendorQRDetails.PostQRMapingDetails(objPostQRMapingData);
            return Response;
        }


        [HttpPost]
        public HttpResponseMessage QRSyncPostAPIUrl(List<QRSyncUpdateData> objQRSyncUpdateDataList)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            Response = objVendorQRDetails.QRSyncPostAPIUrl(objQRSyncUpdateDataList);
            return Response;
        }
    }
}