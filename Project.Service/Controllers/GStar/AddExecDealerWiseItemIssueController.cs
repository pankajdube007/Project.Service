using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Project.Service.Models.GStar;

namespace Project.Service.Controllers.GStar
{
    public class AddExecDealerWiseItemIssueController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ExecDealerWiseItemIssue")]
        public HttpResponseMessage GetDetails(ExecDealerWiseItemIssueList ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecDealerWiseItemIssue> alldcr = new List<ExecDealerWiseItemIssue>();
                    List<ExecDealerWiseItemIssues> alldcr1 = new List<ExecDealerWiseItemIssues>();
                    var dr = g2.return_dr("addExecDealerWiseItemIssue " + ula.ExId + "," + ula.ItemId + ",'" + ula.Cin + "'," + ula.Issutype + ",'"+ula.Remark+"'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new ExecDealerWiseItemIssues
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new ExecDealerWiseItemIssue
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "lat lan Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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