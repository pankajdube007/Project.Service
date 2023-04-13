using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers.Vendor
{
    public class WAVendorCQRLabelController : ApiController
    {
        // GET: WAVendorCQRLabel
        [HttpPost]
        [Route("api/WACartonLabelQRPrint")]
        public HttpResponseMessage WACartonLabelQRPrint(GetCQRLabelData objGetCQRLabelData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.GETCartonPrint(objGetCQRLabelData);
            return Response;
        }
    }
}