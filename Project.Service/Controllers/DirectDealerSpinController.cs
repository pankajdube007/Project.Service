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
    public class DirectDealerSpinController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getdirectdealerspin")]
        public HttpResponseMessage GetDetails(ListsofDirectDealerWiseSpin ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DirectDealerWiseSpins> alldcr = new List<DirectDealerWiseSpins>();
                    List<DirectDealerWiseSpin> alldcr1 = new List<DirectDealerWiseSpin>();
                    var dr = g2.return_dr("beforeluckydrawparty '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DirectDealerWiseSpin
                            {
                                NoOfSpin = Convert.ToString(dr["NoOfSpin"]),
                                RemSpin = Convert.ToString(dr["REM"]),
                                WinAmt = Convert.ToString(dr["totamt"]),
                                //SlNo = Convert.ToInt32(dr["slno"]),
                                //NxtDrwAmt = Convert.ToInt32(Math.Round(Convert.ToDecimal(dr["NxtDrwAmt"].ToString()))),
                                Active = Convert.ToBoolean(dr["Active"]),
                                ApplyCN = Convert.ToBoolean(dr["apcn"]),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new DirectDealerWiseSpins
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