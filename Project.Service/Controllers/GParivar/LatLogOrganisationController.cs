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
    public class LatLogOrganisationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetLatLonOrganisationList")]
        public HttpResponseMessage GetDetails(LatLogOrganisation ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<LatLonOrganisationLists> alldcr = new List<LatLonOrganisationLists>();
                    List<LatLonOrganisationList> alldcr1 = new List<LatLonOrganisationList>();

                    var dr = g1.return_dr("latlonOrganisationList " + ula.Orgid + "," + ula.Catid + "," + ula.ExId + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new LatLonOrganisationList
                            {
                                lat = Convert.ToString(dr["lat"].ToString()),
                                lon = Convert.ToString(dr["lon"].ToString()),
                                ischeckin = Convert.ToString(dr["ischeckin"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new LatLonOrganisationLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "0"), Encoding.UTF8, "application/json");

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