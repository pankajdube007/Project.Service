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
    public class PolicyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPolicy")]
        public HttpResponseMessage GetDetails(ListsofPolicy ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Policy> alldcr = new List<Policy>();
                    List<Policys> alldcr1 = new List<Policys>();
                    var dr = g1.return_dt("App_Policy '" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new Policys
                            {
                                PolicyName = Convert.ToString(dr.Rows[i]["policyname"].ToString()),
                                FromDate = Convert.ToString(dr.Rows[i]["fromdate"].ToString()),
                                Todate = Convert.ToString(dr.Rows[i]["todate"].ToString()),
                                PDF = string.IsNullOrEmpty(dr.Rows[i]["links"].ToString()) ? string.Empty : (baseurl + "schemefiles/tradepolicy/" + Convert.ToString(dr.Rows[i]["links"])),
                                imgurl = string.IsNullOrEmpty(dr.Rows[i]["AppImages"].ToString()) ? string.Empty : (baseurl + "schemefiles/tradepolicy/" + Convert.ToString(dr.Rows[i]["AppImages"])),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new Policy
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