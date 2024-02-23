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
    public class SchemeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeDetails")]
        public HttpResponseMessage GetDetails(SchemeAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Schemes> alldcr = new List<Schemes>();
                    List<Scheme> Scheme = new List<Scheme>();
                    //List<RegularS> RegularS = new List<RegularS>();
                    //List<QuantityS> QuantityS = new List<QuantityS>();
                    //List<GroupS> GroupS = new List<GroupS>();
                    //List<ValueS> ValueS = new List<ValueS>();
                    var dr = (SqlDataReader)null;

                    if (ula.Type == 1)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSchemeRegularschemeApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 2)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSchemeQuantityschemeApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 3)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSchemeValueschemeApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 4)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSchemeGroupschemeApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 5)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSUBCATEGORYWISEQTYSCHEMEschemeApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 6)
                    {
                        dr = g1.return_dr("allPartyWiseActiveitemwiseamountperschemApp '" + ula.CIN + "'");
                    }
                    else if (ula.Type == 7)
                    {
                        dr = g1.return_dr("allPartyWiseActiveSpecialschemeApp '" + ula.CIN + "'");
                    }

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Scheme.Add(new Scheme
                            {
                                schemename = dr["schemename"].ToString(),
                                netsale = dr["NetQty"].ToString(),
                                curslab = dr["currentslab"].ToString(),
                                nextslab = dr["greater"].ToString()
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new Schemes
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Scheme,
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