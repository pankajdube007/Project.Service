using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Controllers.Vendor
{
    public class WAPostMarryQRDetailsController : ApiController
    {
        [HttpPost]
        [Route("api/WAPostMarryQRDetails")]
        public HttpResponseMessage WAPostMarryQRDetails(QRUnMarryDetails objQRUnMarryDetails)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.WAPostMarryQRDetails(objQRUnMarryDetails);
            return Response;
        }
    }
}