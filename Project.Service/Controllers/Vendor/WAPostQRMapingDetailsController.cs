using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers.Vendor
{
    public class WAPostQRMapingDetailsController : ApiController
    {
        [HttpPost]
        [Route("api/WAPostQRMapingDetails")]
        public HttpResponseMessage WAPostQRMapingDetails(PostQRMapingData objPostQRMapingData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.PostQRMapingDetails(objPostQRMapingData);
            return Response;
        }


        [HttpPost]
        [Route("api/WAPostQRFANDetails")]
        public HttpResponseMessage WAFANQRDetails(FANQRData objFANQRData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.FANQRDetails(objFANQRData);
            return Response;
        }
    }
}