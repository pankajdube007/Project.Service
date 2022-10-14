using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class DealerLstyearSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getLastYearSales")]
        public HttpResponseMessage GetDetails(DealerLstYearSalesAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DealerLstYearSaless> alldcr = new List<DealerLstYearSaless>();
                    List<DealerLstYearSales> alldcr1 = new List<DealerLstYearSales>();

                    var dr = (SqlDataReader)null;
                    if (ula.ExecId==0)
                    {
                         dr = g1.return_dr("App_dealerlstyearsale '" + ula.CIN + "'");

                    }
                    else
                    {
                         dr = g1.return_dr("App_dealerlstyearsalegstar '" + ula.CIN + "','"+ula.ExecId+"'");

                    }
                  

      
                  

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DealerLstYearSales
                            {
                                lstyearsale = Convert.ToString(dr["lstyrsale"].ToString()),
                                curyearsale = Convert.ToString(dr["curyrsale"].ToString()),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new DealerLstYearSaless
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