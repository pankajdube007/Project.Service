using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TodMDWDExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTODMDWDExcutive")]
        public HttpResponseMessage GetDetails(ListsofTodmdwdEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<TodmdwdExs> alldcr = new List<TodmdwdExs>();
                    List<TodmdwdEx> alldcr1 = new List<TodmdwdEx>();
                    var dr = g1.return_dr("AppTodMdWdExcutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TodmdwdEx
                            {
                                partynm = Convert.ToString(dr["displaynm"]),
                                groupnm = Convert.ToString(dr["CatGroupNm"]),
                                tod = Convert.ToString(dr["tod"]),
                                md = Convert.ToString(dr["md"]),
                                wd = Convert.ToString(dr["wd"]),
                                url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "/PartyLimitPrint.aspx?allcases=TODMDWD&recid=" + Convert.ToString(dr["SlNo"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodmdwdExs
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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