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
    public class TodayPartywisesaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTodaySalePartywise")]
        public HttpResponseMessage GetDetails(ListofTodayPartywiseSale ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.Category != "")
            {
                try
                {
                    string data1;

                    List<TodayPartywiseSales> alldcr = new List<TodayPartywiseSales>();
                    List<TodayPartywiseSale> alldcr1 = new List<TodayPartywiseSale>();

                    var dr = g1.return_dr("TodayInvoiceReportManagementpartywsise '" + ula.Branch + "','" + ula.fromdate + "','" + ula.todate + "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TodayPartywiseSale
                            {
                                Party = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                sale = Convert.ToString(dr["saleamt"].ToString()),
                                TypeCat = Convert.ToString(dr["typecat"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodayPartywiseSales
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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