using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace Project.Service.Controllers
{
    public class TargetSchemeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTargetScheme")]
        public HttpResponseMessage GetDetails(ListsofTargetScheme ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<TargetSchemeLists> alldcr = new List<TargetSchemeLists>();
                    List<TargetSchemeList> alldcr1 = new List<TargetSchemeList>();

                    var dr = g1.return_dr("Apptargetscheme '" + ula.Cin + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TargetSchemeList
                            {
                                Scheme = Convert.ToString(dr["schemename"].ToString()),
                                TargetQty = Convert.ToString(dr["quantity"].ToString()),
                                Growth = Convert.ToString(dr["growth"].ToString()),
                                SchemeId = Convert.ToString(dr["schemeid"].ToString()),
                                Category = Convert.ToString(dr["categorynm"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                qty = Convert.ToString(dr["saleqty"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TargetSchemeLists
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