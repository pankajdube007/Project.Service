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
    public class GstInformationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getGstInformation")]
        public HttpResponseMessage GetDetails(ListofGstInformation ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetGstInformationLists> alldcr = new List<GetGstInformationLists>();
                    List<GetGstInformationList> alldcr1 = new List<GetGstInformationList>();
                    var dr = g1.return_dr("GstInformation '" + ula.SlnoState + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetGstInformationList
                            {
                                printnm = Convert.ToString(dr["printnm"].ToString()),
                                FullAddress = Convert.ToString(dr["FullAddress"].ToString()),
                                GSTNo = Convert.ToString(dr["GSTNo"].ToString()),
                                mobile = Convert.ToString(dr["mobile"].ToString()),
                                email = Convert.ToString(dr["email"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetGstInformationLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");

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