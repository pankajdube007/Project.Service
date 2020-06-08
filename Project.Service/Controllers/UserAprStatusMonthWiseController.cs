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
    public class UserAprStatusMonthWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getuseraprstatusmonthwisecnt")]
        public HttpResponseMessage GetDetails(ListUserAprStatusMonthWise ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<UserAprStatusMonthWiseLists> alldcr = new List<UserAprStatusMonthWiseLists>();
                    List<UserAprStatusMonthWiseList> alldcr1 = new List<UserAprStatusMonthWiseList>();

                    var dr = g1.return_dr("Userapprovalstatusandmonthwisecount '" + ula.FromDate + "','" + ula.ToDate + "','" + ula.ApproveStatus + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new UserAprStatusMonthWiseList
                            {

                                Month = Convert.ToString(dr["Month"].ToString()),
                                Retailer = Convert.ToInt32(dr["Retailer"].ToString()),
                                Electrician = Convert.ToInt32(dr["Electrician"].ToString()),
                                CounterBoy = Convert.ToInt32(dr["Counter Boy"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new UserAprStatusMonthWiseLists
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