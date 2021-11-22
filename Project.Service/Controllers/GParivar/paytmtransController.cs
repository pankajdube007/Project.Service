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
    public class paytmtransController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ListpaytmTransaction")]
        public HttpResponseMessage GetAllUserLatLong(Paytmtranslist ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ClientSecret!="")
            {
                try
                {
                    string data1;
                    
                    List<Paytmtranss> alldcr = new List<Paytmtranss>();
                    List<Paytmtrans> alldcr1 = new List<Paytmtrans>();
                    var dr = g1.return_dr("GePayatmTransaction ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Paytmtrans
                            {
                                mobile = Convert.ToString(dr["MobileNo"].ToString()),
                                amount = Convert.ToDecimal(dr["Amount"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Paytmtranss
                        {
                            result =true,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  State available"), Encoding.Unicode);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.Unicode);

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.Unicode);

                return response;
            }
        }
    }
}