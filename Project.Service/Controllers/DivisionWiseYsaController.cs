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
    public class DivisionWiseYsaController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionWiseYSA")]
        public HttpResponseMessage GetDetails(DivisionWiseYsaAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;
                    decimal total = 0;

                    List<DivisionWiseYsas> alldcr = new List<DivisionWiseYsas>();
                    List<DivisionWiseYsa> alldcr1 = new List<DivisionWiseYsa>();
                    List<DivisionWiseDetails> DivisionWiseDetails = new List<DivisionWiseDetails>();
                    List<DivisionWiseTotal> DivisionWiseTotal = new List<DivisionWiseTotal>();

                    var dr = g1.return_dr("App_divisionwisesale  '" + ula.CIN + "'," + ula.divisionid);
                    //  DataTable dr1 = g1.return_dt("App_dealerlstyearsale '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DivisionWiseDetails.Add(new DivisionWiseDetails
                            {
                                categorynm = dr["categorynm"].ToString(),
                                sale = dr["sale"].ToString()
                            });

                            total = total + Convert.ToDecimal(dr["sale"].ToString());
                        }

                        // var dr1 = g1.return_dr("App_divisionwisesale '" + ula.CIN + "',0");
                        //if (dr1.HasRows)
                        //{
                        //    while (dr1.Read())
                        //    {
                        //        DivisionWiseTotal.Add(new DivisionWiseTotal
                        //        {
                        //            TotalSale = dr1["sale"].ToString()

                        //        });
                        //    }
                        //}

                        DivisionWiseTotal.Add(new DivisionWiseTotal
                        {
                            TotalSale = total.ToString()
                        });

                        alldcr1.Add(new DivisionWiseYsa
                        {
                            DivisionWiseSale = DivisionWiseDetails,
                            DivisionWiseSaleTotal = DivisionWiseTotal
                        });

                        g1.close_connection();
                        alldcr.Add(new DivisionWiseYsas
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