using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class ExecItemWiseIssueCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ExecItemWiseIssueCount")]
        public HttpResponseMessage GetDetails(ListExecItemWiseIssueCount ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecItemWiseIssueCount> alldcr = new List<ExecItemWiseIssueCount>();
                    List<ExecItemWiseIssueCounts> alldcr1 = new List<ExecItemWiseIssueCounts>();
                    var dr = g1.return_dr($"getissuecount {ula.ExId} , {ula.ItemId} , {ula.Hierarchy}");
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecItemWiseIssueCounts
                            {

                                Issueid = Convert.ToInt32(dr["Issuetype"].ToString()),
                                IssueName = Convert.ToString(dr["issunm"].ToString()),
                                ExeId = Convert.ToInt32(dr["ExeId"].ToString()),
                                ExecutiveName = Convert.ToString(dr["salenm"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Issuecount = Convert.ToInt32(dr["issuecount"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecItemWiseIssueCount
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available !!!"), Encoding.UTF8, "application/json");

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