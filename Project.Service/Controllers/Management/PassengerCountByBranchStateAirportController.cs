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
    public class PassengerCountByBranchStateAirportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpassengerCountByBranchStateAirport")]
        public HttpResponseMessage GetDetails(ListofPassengerCountByBranchStateAirport ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PassengerCountByBranchStateAirports> alldcr = new List<PassengerCountByBranchStateAirports>();
                    List<PassengerCountByBranchStateAirport> alldcr1 = new List<PassengerCountByBranchStateAirport>();

                    var dr = g1.return_dr("GetPassengerCountByBranchStateAirport '" + ula.CIN + "','" + ula.Category + "','"+ula.Type+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PassengerCountByBranchStateAirport
                            {
                                BranchwisePassengerCount = Convert.ToString(dr["BranchwisePassengerCount"].ToString()),
                                Name = Convert.ToString(dr["Name"].ToString()),
                                Type = Convert.ToString(dr["Type"].ToString()),
                                TypeId = Convert.ToString(dr["TypeId"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PassengerCountByBranchStateAirports
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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