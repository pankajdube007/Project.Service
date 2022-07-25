using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers.Vendor
{
    public class WAQRSyncPostAPIUrlController : ApiController
    {
        [HttpPost]
        [Route("api/WAQRSyncPostAPIUrl")]
        public HttpResponseMessage WAQRSyncPostAPIUrl(List<QRSyncUpdateData> objQRSyncUpdateDataList)
        {
            HttpResponseMessage Response = new HttpResponseMessage();
            VendorQRDetails objVendorQRDetails = new VendorQRDetails();
            Response = objVendorQRDetails.QRSyncPostAPIUrl(objQRSyncUpdateDataList);
            return Response;
        }
    }
}