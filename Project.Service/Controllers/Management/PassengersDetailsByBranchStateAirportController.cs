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
    public class PassengersDetailsByBranchStateAirportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpassengersDetailsByBranchStateAirport")]
        public HttpResponseMessage GetDetails(ListofPassengersDetailsByBranchStateAirport ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PassengersDetailsByBranchStateAirports> alldcr = new List<PassengersDetailsByBranchStateAirports>();
                    List<PassengersDetailsByBranchStateAirport> alldcr1 = new List<PassengersDetailsByBranchStateAirport>();

                    var dr = g1.return_dr("GetPassengersDetailsByBranchStateAirport '" + ula.CIN + "','" + ula.Category + "','" + ula.Type + "','" + ula.Typeid + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PassengersDetailsByBranchStateAirport
                            {
                                PassengerName = Convert.ToString(dr["PassengerName"].ToString()),
                                RelationName = Convert.ToString(dr["RelationName"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                TravelIDNo = Convert.ToString(dr["TravelIDNo"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                CategoryName = Convert.ToString(dr["CategoryName"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PassengersDetailsByBranchStateAirports
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