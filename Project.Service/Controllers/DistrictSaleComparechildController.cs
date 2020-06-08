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
using static Project.Service.Models.DistrictSaleComparechild;

namespace Project.Service.Controllers
{
    public class DistrictSaleComparechildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDistrictwiseSaleComparechild")]
        public HttpResponseMessage GetDetails(InputRequest inputRequest)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (inputRequest.CIN != "")
            {
                try
                {
                    string data1;

                    List<OutputResponse> alldcr = new List<OutputResponse>();
                    List<DistrictSaleComparechilds> alldcr1 = new List<DistrictSaleComparechilds>();

                    var dr = g1.return_dr(string.Format("exec DistrictsaleComparechild '{0}','{1}','{2}'", inputRequest.distid, inputRequest.CIN, inputRequest.Category));


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DistrictSaleComparechilds
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                partystatus = Convert.ToString(dr["workstatus"].ToString()),
                                currentyearsale = Convert.ToString(dr["curyear"].ToString()),
                                previousyearsale = Convert.ToString(dr["lstyear"].ToString()),
                                previoustwoyearsale = Convert.ToString(dr["lat2year"].ToString()),

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