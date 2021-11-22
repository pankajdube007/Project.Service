using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class GetListofCityStateController : ApiController
    {
        

        [HttpPost]
        [ValidateModel]
        [Route("api/getlistofcitystate")]
        public HttpResponseMessage GetDetails(ListofCitystate ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetListofCitystates> alldcr = new List<GetListofCitystates>();
                    List<GetListofCitystate> alldcr1 = new List<GetListofCitystate>();
                    var dr = g1.return_dr("dbo.Getlistofstateandcity '" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListofCitystate
                            {

                                CityID = Convert.ToInt32(dr["CityID"].ToString()),
                                CityName = Convert.ToString(dr["CityName"].ToString()),
                                StateID = Convert.ToInt32(dr["StateID"].ToString()),
                                StateName = Convert.ToString(dr["StateName"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetListofCitystates
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