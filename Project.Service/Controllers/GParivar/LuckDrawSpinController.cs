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
    public class LuckDrawSpinController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getdealerluckdrawspin")]
        public HttpResponseMessage GetDetails(ListsLuckDrawSpin ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<LuckDrawSpins> alldcr = new List<LuckDrawSpins>();
                    List<LuckDrawSpin> alldcr1 = new List<LuckDrawSpin>();
                    var dr = g2.return_dr("luckydrawspin '" + ula.CIN + "','" + ula.GiftId + "','" + ula.RandomNo + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new LuckDrawSpin
                            {
                                Result = Convert.ToString(dr["result"]),
                                ResultId = Convert.ToString(dr["resultid"]),
                                Price = Convert.ToString(dr["price"]),
                                Name = Convert.ToString(dr["name"]),
                                TotalChance = Convert.ToString(dr["totalchance"]),
                                Used = Convert.ToString(dr["used"]),
                                Remaining = Convert.ToString(dr["pending"]),
                                giftid = Convert.ToString(dr["giftid"]),
                                randomno = Convert.ToString(dr["randomno"]),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new LuckDrawSpins
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