using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class AddMerchantController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddMerchant")]
        public HttpResponseMessage GetDetails(ListofAddMerchant ula)
        {

            DataConection g1 = new DataConection();
            Common cm = new Common();

            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddMerchants> alldcr = new List<AddMerchants>();
                    List<AddMerchant> alldcr1 = new List<AddMerchant>();

                    string uploadPDF = string.Empty;
                    string uploadPDF1 = string.Empty;

                    if (ula.gstcertificate != "") uploadPDF = GetImage(ula.gstcertificate, 1);
                    if (ula.bankdetails != "") uploadPDF1 = GetImage(ula.bankdetails, 1);

                    var dr = g1.return_dt("AddMerchantApp'" + ula.suppliername + "','" + ula.gstno + "','" + ula.addline1 + "','" + ula.addline2 + "','" + ula.cityid + "','" + ula.areaid + "','" + ula.stateid + "','" + ula.countryid + "','" + ula.pinno + "','" + ula.ConcernedPerson + "','" + ula.email + "','" + ula.mobile + "','" + ula.merchanttype + "','" + uploadPDF + "','" + uploadPDF1 + "'");
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["RES"]) == 2)
                        {
                            alldcr.Add(new AddMerchants
                            {
                                result = false,
                                message = "Supplier already Exist!!",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        else if (Convert.ToInt32(dr.Rows[0]["RES"]) == 1)
                        {

                            alldcr1.Add(new AddMerchant
                            {
                                output = "Data Sucessfully inserted"
                            });

                            g1.close_connection();
                            alldcr.Add(new AddMerchants
                            {
                                result = true,
                                message = string.Empty,
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;


                        }

                        else
                        {
                            g1.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

                            return response;

                        }
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

                        return response;
                    }

                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    return response;
                }

            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }

        protected string GetImage(string img, int folderCreation)
        {
            var _goldMedia = new GoldMedia();
            var result = "";
            string uniquefoldernm = "";
            if (folderCreation == 1)
            {
                uniquefoldernm = "addmerchantpdf";
            }


            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName = Guid.NewGuid().ToString();

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".pdf", stream, "application/pdf", false,
                    false, true);
                result = _goldMedia.MapPathToPublicUrl(uniquefoldernm + '/' + FileName + ".pdf");
            }
            return result;
        }
    }
}