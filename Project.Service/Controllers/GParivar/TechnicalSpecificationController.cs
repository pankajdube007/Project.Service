using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TechnicalSpecificationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTechnicalSpecification")]
        public HttpResponseMessage GetDetails(ListsofTechnicalSpecification ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<TechnicalSpecification> alldcr = new List<TechnicalSpecification>();
                    List<TechnicalSpecifications> alldcr1 = new List<TechnicalSpecifications>();
                    var dr = g1.return_dt("App_TechnicalSpecificationActive");

                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new TechnicalSpecifications
                            {
                                Code = Convert.ToString(dr.Rows[i]["TechnicalSpecCode"].ToString()),
                                Name = Convert.ToString(dr.Rows[i]["TechnicalSpecification"].ToString()),
                                url = string.IsNullOrEmpty(dr.Rows[i]["UploadFiles"].ToString()) ? "" : (baseurl + "technicalspecification/" + dr.Rows[i]["UploadFiles"].ToString().Replace(".jpeg", ".pdf")),
                                imgurl = string.IsNullOrEmpty(dr.Rows[i]["AppImages"].ToString()) ? "" : (baseurl + "technicalspecification/" + dr.Rows[i]["AppImages"].ToString()),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new TechnicalSpecification
                        {
                            result = true,
                            message = "",
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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
    }
}