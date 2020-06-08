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
    public class NearByOrgController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetNearByOrgList")]
        public HttpResponseMessage GetDetails(ListsofNearByOrgList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<NearByOrgLists> alldcr = new List<NearByOrgLists>();
                    List<NearByOrgList> alldcr1 = new List<NearByOrgList>();

                    //var dr = g1.return_dr("app_nearbyorg " + ula.StateId + "," + ula.Lat + "," + ula.Lan);
                    var dr = g1.return_dr("app_nearbyorg " + ula.ExId + ",'" + ula.Lat + "','" + ula.Lan + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new NearByOrgList
                            {
                                orgname = Convert.ToString(dr["orgnm"]),
                                orgid = Convert.ToString(dr["orgid"]),
                                catid = Convert.ToString(dr["orgcat"]),
                                catname = Convert.ToString(dr["partycatnm"]),
                                Orglat = Convert.ToString(dr["lat"]),
                                Orglan = Convert.ToString(dr["lon"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new NearByOrgLists
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