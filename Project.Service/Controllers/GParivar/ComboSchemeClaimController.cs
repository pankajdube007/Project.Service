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
    public class ComboSchemeClaimController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getComboClaim")]
        public HttpResponseMessage GetDetails(ListsofComboClaim ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<ComboClaims> alldcr = new List<ComboClaims>();
                    List<ComboClaim> alldcr1 = new List<ComboClaim>();
                    var dr = g1.return_dr("getcomboclaimbycin '" + ula.CIN + "',"+ula.ComboId);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ComboClaim
                            {
                                itemnm = Convert.ToString(dr["ItemName"].ToString()),
                                finalqty = Convert.ToString(dr["finalqty"].ToString()),
                                totalsale = Convert.ToString(dr["totalsale"].ToString()),
                                difference = Convert.ToString(dr["difference"].ToString()),
                                pending = Convert.ToString(dr["pending"].ToString()),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new ComboClaims
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
                        response.Content = new StringContent(cm.StatusTime(false, "Combo Scheme Time Over!!!!"), Encoding.UTF8, "application/json");

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