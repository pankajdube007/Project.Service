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
    public class GetSaleReturnRequestController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/salereturnrequestselects")]
        public HttpResponseMessage GetDetails(GetSaleReturnRequestList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<GetSaleReturnRequests> alldcr = new List<GetSaleReturnRequests>();
                    List<GetSaleReturnRequest> alldcr1 = new List<GetSaleReturnRequest>();

                    var dr = g1.return_dr("salereturnrequestselect " + ula.CIN);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetSaleReturnRequest
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                requestno = Convert.ToString(dr["requestno"].ToString()),
                                requestdt = Convert.ToString(dr["requestdt"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                qty = Convert.ToInt32(dr["qty"].ToString()),
                                qtytype = Convert.ToString(dr["qtytype"].ToString()),
                                rtype = Convert.ToString(dr["rtype"].ToString()),
                                level = Convert.ToString(dr["level"].ToString()),
                                levelid = Convert.ToString(dr["levelid"].ToString()),
                                amount = Convert.ToInt32(dr["amount"].ToString()),
                            });
                        }
                        g1.close_connection();

                        alldcr.Add(new GetSaleReturnRequests
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