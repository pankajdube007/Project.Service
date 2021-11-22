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
    public class SchemeGrowthController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeGrowth")]
        public HttpResponseMessage GetDetails(ListsofSchemeGrowth ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<SchemeGrowth> alldcr = new List<SchemeGrowth>();
                    List<SchemeGrowthHead> head = new List<SchemeGrowthHead>();
                    List<SchemeGrowthChild> child = new List<SchemeGrowthChild>();
                    List<SchemeGrowthFinal> schemegrowthfinal = new List<SchemeGrowthFinal>();

                    var dr = g1.return_dr("AppSchemeGrowthPercentageHead " + ula.SchemeId + "");
                    var dr1 = g1.return_dr("AppSchemeGrowthPercentagechild " + ula.SchemeId + "");

                    
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            head.Add(new SchemeGrowthHead
                            {
                                SchemeName = Convert.ToString(dr["SchemeName"].ToString()),
                                BaseQty = Convert.ToString(dr["BaseQty"].ToString()),
                                FromDate = Convert.ToString(dr["FromDate"].ToString()),
                                ToDate = Convert.ToString(dr["ToDate"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                Category = Convert.ToString(dr["categorynm"].ToString()),
                                Item = Convert.ToString(dr["item"].ToString()),
                                Branch = Convert.ToString(dr["branch"].ToString()),
                            });
                        }
                    }

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            child.Add(new SchemeGrowthChild
                            {
                                GrowthPercet = Convert.ToString(dr1["GrowthPercet"].ToString()),
                                Amount = Convert.ToString(dr1["Amount"].ToString()),
                               
                            });
                        }
                    }




                    schemegrowthfinal.Add(new SchemeGrowthFinal
                    {
                        head = head,
                        child = child,
                       
                    });

                    g1.close_connection();
                    alldcr.Add(new SchemeGrowth
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = schemegrowthfinal,
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
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