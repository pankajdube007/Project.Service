using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class SchemeUploadExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeUploadExcutive")]
        public HttpResponseMessage GetDetails(ListsofSchemeUpload ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    string todate = string.Empty;

                    List<SchemeUploads> alldcr = new List<SchemeUploads>();
                    List<retunvalue> alldcr1 = new List<retunvalue>();
                    string uploadfilename = string.Empty;

                    if (!string.IsNullOrEmpty(ula.File))
                    {
                        string s = ula.File.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                        if (s.Length % 4 > 0)
                        {
                            s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                        }
                        byte[] binPdf = Convert.FromBase64String(s);

                        Stream stream = new MemoryStream(binPdf);
                        string FileName = Guid.NewGuid().ToString();
                        string uniquefoldernm = "othercompanypricelist";
                        var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".pdf", stream, "application/pdf", false, false, true);
                        uploadfilename = FileName + ".pdf";
                    }

                    if (ula.ToDate == null || ula.ToDate.ToString() == "01/01/0001 12:00:00 AM" || ula.ToDate.ToString() == "")
                    {
                        todate = "";
                    }
                    else
                    {
                        todate = ula.ToDate.ToString();
                    }

                    var dr = g2.return_dr("AppOtherCompanyAdd " + ula.ExId + ",'" + ula.CompanyName + "','" + ula.RangeName + "','" + ula.Type + "','" + ula.FromDate + "','" + todate + "','" + uploadfilename + "','" + ula.Remark + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new retunvalue
                            {
                                ouptput = "Data Uploaded",
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new SchemeUploads
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}