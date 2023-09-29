using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Controllers.Vendor
{

    public class WAVendorBAGQRLabelController : ApiController
    {
        // GET: WAVendorBAGQRLabelController
        [HttpPost]
        [Route("api/WABagLabelQRPrint")]
        public HttpResponseMessage WABagLabelQRPrint(GetBagQRLabelData objGetBagQRLabelData)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.GETBagQRPrint(objGetBagQRLabelData);
            return Response;
        }
    }
}
