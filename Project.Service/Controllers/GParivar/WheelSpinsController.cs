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

namespace Project.Service
{
    public class WheelSpinsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getWheelSpins")]
        public HttpResponseMessage GetDetails(ListsofWheelSpin ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<WheelSpins> alldcr = new List<WheelSpins>();
                    List<WheelSpin> alldcr1 = new List<WheelSpin>();
                    var dr = g2.return_dr("AppGetSpinWheel '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new WheelSpin
                            {
                                NoOfSpin = Convert.ToString(dr["NoOfSpin"]),
                                RemSpin = Convert.ToString(dr["RemSpin"]),
                                WinAmt = Convert.ToString(dr["WinAmt"]),
                                SlNo = Convert.ToInt32(dr["slno"]),
                                NxtDrwAmt = Convert.ToInt32(Math.Round(Convert.ToDecimal(dr["NxtDrwAmt"].ToString()))),
                                Active = Convert.ToBoolean(dr["Active"]),
                                ApplyCN = Convert.ToBoolean(dr["ApplyCN"]),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new WheelSpins
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
                        g2.close_connection();
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