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
    public class UserDistrictwiseCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getuserdistrictwisecnt")]
        public HttpResponseMessage GetDetails(ListUserdistrictwisecount ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<UserdistrictwisecountLists> alldcr = new List<UserdistrictwisecountLists>();
                    List<UserdistrictwisecountList> alldcr1 = new List<UserdistrictwisecountList>();

                    var dr = g1.return_dr("Userdistrictwisecount'" + ula.CIN + "','" + ula.Cat + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.Districtid + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new UserdistrictwisecountList
                            {

                                DistrictName = Convert.ToString(dr["District"].ToString()),
                                AprRetailer = Convert.ToInt32(dr["AprRetailer"].ToString()),
                                AprElectrician = Convert.ToInt32(dr["AprElectrician"].ToString()),
                                AprCounterBoy = Convert.ToInt32(dr["AprCounter Boy"].ToString()),

                                RejRetailer = Convert.ToInt32(dr["RejRetailer"].ToString()),
                                RejElectrician = Convert.ToInt32(dr["RejElectrician"].ToString()),
                                RejCounterBoy = Convert.ToInt32(dr["RejCounter Boy"].ToString()),


                                PenRetailer = Convert.ToInt32(dr["PenRetailer"].ToString()),
                                PenElectrician = Convert.ToInt32(dr["PenElectrician"].ToString()),
                                PenCounterBoy = Convert.ToInt32(dr["PenCounter Boy"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new UserdistrictwisecountLists
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