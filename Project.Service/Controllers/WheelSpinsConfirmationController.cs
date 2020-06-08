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
    public class WheelSpinsConfirmationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/SpinAmountConfirmation")]
        public HttpResponseMessage GetDetails(ListsofSpinAmountConf ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<SpinAmountConfs> alldcr = new List<SpinAmountConfs>();
                    List<SpinAmountConf> alldcr1 = new List<SpinAmountConf>();
                    var dr = g2.return_dr("AppGetWinSpinWheelNext '" + ula.CIN + "'," + Convert.ToInt32(ula.SlNo) + "," + Convert.ToDecimal(ula.prizemoney));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new SpinAmountConf
                            {
                                SlNo = Convert.ToInt32(dr["slno"]),
                                amount = Convert.ToInt32(Math.Round(Convert.ToDecimal(dr["NxtDrwAmt"].ToString()))),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new SpinAmountConfs
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