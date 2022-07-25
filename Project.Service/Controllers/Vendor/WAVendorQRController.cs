using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers.Vendor
{
    public class WAVendorQRController : ApiController
    {
        

        [HttpPost]
        [Route("api/WAGetQRDetails")]
        public HttpResponseMessage WAGetQRDetails(GetQRData objGetQRData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.GetQRDetails(objGetQRData);
            return Response;
        }

        
    }
}