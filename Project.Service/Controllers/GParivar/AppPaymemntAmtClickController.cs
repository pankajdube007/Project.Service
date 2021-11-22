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
    public class AppPaymemntAmtClickController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getAppPaymentAmtClick")]
        public HttpResponseMessage GetDetails(ListAppPaymentAmtClick ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<AppPaymentAmtClickLists> alldcr = new List<AppPaymentAmtClickLists>();
                    List<AppPaymentAmtClickList> alldcr1 = new List<AppPaymentAmtClickList>();

                    var dr = g1.return_dr("getAppPaymentAmtClick '" + ula.Cat + "','" + ula.type + "','" + ula.Date + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new AppPaymentAmtClickList
                            {

                                partyname = Convert.ToString(dr["partyname"].ToString()),
                                Chequeamt = Convert.ToString(dr["Chequeamt"].ToString()),
                                locnm = Convert.ToString(dr["locnm"].ToString()),
                                bankfrom = Convert.ToString(dr["bankfrom"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AppPaymentAmtClickLists
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