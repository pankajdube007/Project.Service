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
using static Project.Service.Models.DistrictSaleCompare;

namespace Project.Service.Controllers
{
    public class DistrictSaleCompareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetDistrictwiseSalesCompare")]
        public HttpResponseMessage GetDetails(InputRequest inputRequest)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (inputRequest.Category == "Management")
            {
                try
                {
                    string data1;

                    List<OutputResponse> alldcr = new List<OutputResponse>();
                    List<DistrictSaleCompares> alldcr1 = new List<DistrictSaleCompares>();

                    var dr = g1.return_dr("DistrictSaleCompare "+inputRequest.stateid+",'"+inputRequest.CIN+ "','" + inputRequest.Category + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DistrictSaleCompares
                            {
                                distid = Convert.ToString(dr["distid"].ToString()),
                                distnm = Convert.ToString(dr["Distrctnm"].ToString()),
                                currentyearsale = Convert.ToString(dr["currentyearsale"].ToString()),
                                previousyearsale = Convert.ToString(dr["previousyearsale"].ToString()),
                                previoutwoyearsale = Convert.ToString(dr["previou2yearsale"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OutputResponse
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