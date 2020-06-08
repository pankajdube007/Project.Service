using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class salerequestbillingupdateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/salerequestbillingupdate")]
        public HttpResponseMessage GetDetails(salerequestbillingupdate ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<salerequestbillings> alldcr = new List<salerequestbillings>();

                    string Image1 = getimage1(ula.invoiceImage);

                    if (!string.IsNullOrEmpty(Image1))
                    {
                        string lrdt1 = null;

                        if (ula.lrDate != "")
                        {
                            string[] st1 = ula.lrDate.Split('/');

                            if (st1.Count() > 2)
                            {
                                lrdt1 = st1[0] + "/" + st1[1] + "/" + st1[2];
                            }
                        }

                        string docdt1 = null;

                        if (ula.docDate != "")
                        {
                            string[] st1 = ula.docDate.Split('/');

                            if (st1.Count() > 2)
                            {
                                docdt1 = st1[0] + "/" + st1[1] + "/" + st1[2];
                            }
                        }

                        var dr = g2.return_dr("salerequestbillingupdate " + ula.lrNo + ",'" + lrdt1 + "'," + ula.docNo + ",'" + docdt1
                                                                          + "'," + ula.docType + "," + ula.amount + ",'" + ula.transporter + "','" + Image1
                                                                          + "',1,1" + "," + ula.slNo);

                        if (dr.HasRows)
                        {
                            g2.close_connection();

                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "Updated successfully."), Encoding.UTF8, "application/json");

                            return response;
                        }
                        else
                        {
                            g2.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Not Updated.'"), Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                        response.Content = new StringContent(cm.StatusTime(false, "Please select image."), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }

        protected string getimage1(string img1)
        {
            GoldMedia _goldMedia = new GoldMedia();
            string result = "";

            if (!string.IsNullOrEmpty(img1))
            {
                string s = img1.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0)
                {
                    s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                }
                byte[] binPdf = Convert.FromBase64String(s);

                Stream stream = new MemoryStream(binPdf);
                string FileName = Guid.NewGuid().ToString();
                string uniquefoldernm = "salereturnrequest";

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false, false, true);

                result = FileName + ".jpg";
            }

            return result;
        }
    }
}