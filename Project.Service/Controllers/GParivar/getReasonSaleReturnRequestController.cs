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
    public class getReasonSaleReturnRequestController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getReasonSaleReturnRequest")]
        public HttpResponseMessage GetDetails(getReasonSaleReturnRequestList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<getReasonSaleReturnRequests> alldcr = new List<getReasonSaleReturnRequests>();
                    List<getReasonSaleReturnRequest> alldcr1 = new List<getReasonSaleReturnRequest>();

                    var dr = g1.return_dr("getReasonSaleReturnRequest");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new getReasonSaleReturnRequest
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                reason = Convert.ToString(dr["reason"].ToString())
                            });
                        }
                        g1.close_connection();

                        alldcr.Add(new getReasonSaleReturnRequests
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
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}