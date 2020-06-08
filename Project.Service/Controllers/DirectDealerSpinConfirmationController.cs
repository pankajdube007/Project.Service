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
    public class DirectDealerSpinConfirmationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/DirectDealerSpinAmountConfirmation")]
        public HttpResponseMessage GetDetails(ListsofDirectDealerSpinConf ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DirectDealerSpinConfs> alldcr = new List<DirectDealerSpinConfs>();
                    List<DirectDealerSpinConf> alldcr1 = new List<DirectDealerSpinConf>();
                    var dr = g2.return_dr("createluckydrawparty '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DirectDealerSpinConf
                            {
                                NoOfSpin = Convert.ToString(dr["NoOfSpin"]),
                                RemSpin = Convert.ToString(dr["REM"]),
                                WinAmt = Convert.ToString(dr["totamt"]),
                                SlNo = Convert.ToInt32(dr["slno"]),
                                NxtDrwAmt = Convert.ToInt32(Math.Round(Convert.ToDecimal(dr["newamt"].ToString()))),
                               
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new DirectDealerSpinConfs
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
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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