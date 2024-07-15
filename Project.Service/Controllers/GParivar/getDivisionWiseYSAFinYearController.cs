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
    public class getDivisionWiseYSAFinYearController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionWiseYSAFinYear")]
        public HttpResponseMessage GetDetails(getDivisionWiseYSAFinYear ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;
                    decimal total = 0;

                    List<DivisionWiseYsasFinYear> alldcr = new List<DivisionWiseYsasFinYear>();
                    List<DivisionWiseYsaFinYear> alldcr1 = new List<DivisionWiseYsaFinYear>();
                    List<DivisionWiseDetailsFinYear> DivisionWiseDetails = new List<DivisionWiseDetailsFinYear>();
                    List<DivisionWiseTotalFinYear> DivisionWiseTotal = new List<DivisionWiseTotalFinYear>();

                    var dr = (SqlDataReader)null;
                    if (ula.ExecId == 0)
                    {
                        dr = g1.return_dr("App_divisionwisesalefinyear  '" + ula.CIN + "'," + ula.divisionid+",'"+ula.FinYear+"'");

                    }
                    else
                    {
                        dr = g1.return_dr("App_divisionwisesalegstarfinyear  '" + ula.CIN + "'," + ula.divisionid + "," + ula.ExecId + ",'"+ula.FinYear+"'");

                    }



                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DivisionWiseDetails.Add(new DivisionWiseDetailsFinYear
                            {
                                categorynm = dr["categorynm"].ToString(),
                                sale = dr["sale"].ToString(),
                                categoryid = dr["categoryid"].ToString(),
                                divisionid = dr["divisionid"].ToString()
                            });

                            total = total + Convert.ToDecimal(dr["sale"].ToString());
                        }



                        DivisionWiseTotal.Add(new DivisionWiseTotalFinYear
                        {
                            TotalSale = total.ToString()
                        });

                        alldcr1.Add(new DivisionWiseYsaFinYear
                        {
                            DivisionWiseSale = DivisionWiseDetails,
                            DivisionWiseSaleTotal = DivisionWiseTotal
                        });

                        g1.close_connection();
                        alldcr.Add(new DivisionWiseYsasFinYear
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