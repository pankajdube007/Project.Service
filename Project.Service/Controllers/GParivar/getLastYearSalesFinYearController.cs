using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Project.Service.Models.GParivar;

namespace Project.Service.Controllers.GParivar
{
    public class getLastYearSalesFinYearController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getLastYearSalesFinYear")]
        public HttpResponseMessage GetDetails(getLastYearSalesFinYear ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<getLastYearSalesFinYearss> alldcr = new List<getLastYearSalesFinYearss>();
                    List<getLastYearSalesFinYears> alldcr1 = new List<getLastYearSalesFinYears>();

                    var dr = (SqlDataReader)null;
                    if (ula.ExecId == 0)
                    {
                        dr = g1.return_dr("App_dealerlstyearsalefinyear '" + ula.CIN + "','" + ula.FinYear + "'");

                    }
                    else
                    {
                        dr = g1.return_dr("App_dealerlstyearsaleGstarfinyear '" + ula.CIN + "','" + ula.ExecId + "','"+ ula.FinYear+"'");

                    }





                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new getLastYearSalesFinYears
                            {
                                lstyearsale = Convert.ToString(dr["lstyrsale"].ToString()),
                                curyearsale = Convert.ToString(dr["curyrsale"].ToString()),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new getLastYearSalesFinYearss
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