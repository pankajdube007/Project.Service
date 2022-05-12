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
    public class PartyFianYearWiseDivSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getpartywisefinyearwisedivsale")]
        public HttpResponseMessage GetDetails(ListofPartyFinYearWiseDivSale ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PartyFinYearWiseDivSales> alldcr = new List<PartyFinYearWiseDivSales>();
                    List<PartyFinYearWiseDivSale> alldcr1 = new List<PartyFinYearWiseDivSale>();

                    var dr = g1.return_dr("partywisefinyearwisedivsalemanagement '" + ula.Category + "','" + ula.CIN + "','" + ula.FinYear + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PartyFinYearWiseDivSale
                            {
                                DivId = Convert.ToString(dr["divisionid"].ToString()),
                                MonthName = Convert.ToString(dr["mnth"].ToString()),
                                DivName = Convert.ToString(dr["divisionnm"].ToString()),
                                Sale = Convert.ToString(dr["sale"].ToString()),
                             
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PartyFinYearWiseDivSales
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