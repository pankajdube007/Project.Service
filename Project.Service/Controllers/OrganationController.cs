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
    public class OrganationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrganation")]
        public HttpResponseMessage GetDetails(ListsofOrganation ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<Organations> alldcr = new List<Organations>();
                    List<Organation> alldcr1 = new List<Organation>();
                    string uploadfilename = string.Empty;
                    if (!string.IsNullOrEmpty(ula.img))
                    {
                        string s = ula.img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                        if (s.Length % 4 > 0)
                        {
                            s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                        }
                        byte[] binPdf = Convert.FromBase64String(s);

                        Stream stream = new MemoryStream(binPdf);
                        string FileName = Guid.NewGuid().ToString();
                        string uniquefoldernm = "organationimage";
                        var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "application/pdf", false, false, true);
                        uploadfilename = FileName + ".jpg";
                    }
                    var dr = g2.return_dr("apporganation '" + ula.CatId + "','" + ula.CompanyName + "','" + ula.Name + "','" + ula.MobileNo + "','" + ula.Address + "','" + ula.AreaId + "','" + uploadfilename + "',"+ula.ExId);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Organation
                            {
                                OrgId = Convert.ToInt32(dr["orgid"])
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new Organations
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Organation Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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