using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.Management;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class GetStateOrderStatuswiseCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetStateOrderStatuswise")]
        public HttpResponseMessage GetDetails(ListGetStateOrderStatuswiseCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetStateOrderStatuswiseCounts> alldcr = new List<GetStateOrderStatuswiseCounts>();
                    List<GetStateOrderStatuswiseCount> alldcr1 = new List<GetStateOrderStatuswiseCount>();
                    var dr = g1.return_dr($"usp_GetStateOrderStatuswiseCount_API  {ula.StatusId} , {ula.StateId} , '{ula.Category}'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetStateOrderStatuswiseCount
                            {
                                UserCategoryId = Convert.ToInt32(dr["UserCategoryID"]),
                                CategoryName = Convert.ToString(dr["CategoryName"]),
                                StateId = Convert.ToInt32(dr["StateID"]),
                                StateName = Convert.ToString(dr["StateName"]),
                                OrderStatusId = Convert.ToInt32(dr["OrderStatusID"]),
                                SevenToFifteenOrderCount = Convert.ToString(dr["SevenToFifteenOrderCount"]),
                                SixteenToThirtyOrderCount = Convert.ToString(dr["SixteenToThirtyOrderCount"]),
                                ThirtyOneToFortyFiveOrderCount = Convert.ToString(dr["ThirtyOneToFortyFiveOrderCount"]),
                                FortySixToSixtyOrderCount = Convert.ToString(dr["FortySixToSixtyOrderCount"]),
                                SixtyOneTo120DaysOrderCount = Convert.ToString(dr["SixtyOneTo120DaysOrderCount"]),
                                OneTwentyOneTo365DaysOrderCount = Convert.ToString(dr["OneTwentyOneTo365DaysOrderCount"]),
                                MoreThan365DaysOrderCount = Convert.ToString(dr["MoreThan365DaysOrderCount"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetStateOrderStatuswiseCounts
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