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
    public class UserAprStatusCityWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getuseraprstatuscitywisecnt")]
        public HttpResponseMessage GetDetails(ListUserAprStatusCityWise ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<UserAprStatusCityWiseLists> alldcr = new List<UserAprStatusCityWiseLists>();
                    List<UserAprStatusCityWiseList> alldcr1 = new List<UserAprStatusCityWiseList>();

                    var dr = g1.return_dr("Userapprovalstatusandcitytwisecount '" + ula.FromDate + "','" + ula.ToDate + "','" + ula.ApproveStatus + "','" + ula.DistrictId + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new UserAprStatusCityWiseList
                            {

                                City = Convert.ToString(dr["City"].ToString()),
                                Retailer = Convert.ToInt32(dr["Retailer"].ToString()),
                                Electrician = Convert.ToInt32(dr["Electrician"].ToString()),
                                CounterBoy = Convert.ToInt32(dr["Counter Boy"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new UserAprStatusCityWiseLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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