using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class ExecSchemeWiseTargetPastController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExecSchemeWiseTargetPast")]
        public HttpResponseMessage GetDetails(ListExecSchemeWiseTargetPast ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List< ExecSchemeWiseTargetPast > alldcr = new List<ExecSchemeWiseTargetPast>();
                    List< ExecSchemeWiseTargetsPast > alldcr1 = new List<ExecSchemeWiseTargetsPast>();
                    var dr = g1.return_dr("GCSExecSchemeWiseTargetPast> '" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecSchemeWiseTargetsPast
                            {

                                Salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                Q = Convert.ToString(dr["Q"].ToString()),
                                Execid = Convert.ToString(dr["Execid"].ToString()),
                                SchemeName = Convert.ToString(dr["schemename"].ToString()),
                                SchemeId = Convert.ToString(dr["SchemeId"].ToString()),
                                TotalTarget = Convert.ToString(dr["totalTarget"].ToString()),
                                SalesQty = Convert.ToString(dr["SalesQty"].ToString()),
                                EarnAmount = Convert.ToString(dr["EarnAmount"].ToString()),
                                ShortFall = Convert.ToString(dr["ShortFall"].ToString()),
                                Division = Convert.ToString(dr["division"].ToString()),
                                Growth = Convert.ToString(dr["growth"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecSchemeWiseTargetPast
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