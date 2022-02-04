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
    public class TodayDivisionwiseSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTodaySaleDivisionwise")]
        public HttpResponseMessage GetDetails(ListofTodayDivisionwiseSale ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
          
                try
                {
                    string data1;

                    List<TodayDivisionwiseSales> alldcr = new List<TodayDivisionwiseSales>();
                    List<TodayDivisionwiseSale> alldcr1 = new List<TodayDivisionwiseSale>();

                    var dr = g1.return_dr("TodayInvoiceReportManagementdivisionwsise '" + ula.fromdate+"','"+ula.todate+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TodayDivisionwiseSale
                            {
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                amount = Convert.ToString(dr["saleamt"].ToString()),
                                divisionid= Convert.ToInt32(dr["divisionid"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodayDivisionwiseSales
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
    }
}