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
    public class BranchWiseExeWiseTargetController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesExecutivewisetarget")]
        public HttpResponseMessage GetDetails(ListsofBranchWiseExecWiseDivWiseTarget ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.Cin != "")
            {
                try
                {
                    string data1;
                    decimal totalsale = 0;
                    decimal totaltarget = 0;
                    decimal totaldealertarget = 0;
                    List<BranchWiseExecWiseDivWiseTargets> alldcr = new List<BranchWiseExecWiseDivWiseTargets>();
                    List<BranchWiseExecWiseDivWiseTarget> alldcr1 = new List<BranchWiseExecWiseDivWiseTarget>();
                    List<BranchWiseExecWiseDivWiseTargetFinal> Final = new List<BranchWiseExecWiseDivWiseTargetFinal>();
                    var dr = g1.return_dr("branchwisedivwiseexecdetails '" + ula.Cin + "','" + ula.Cat + "','" + ula.BranchId + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.GroupId + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranchWiseExecWiseDivWiseTarget
                            {
                                ExeId = dr["exe"].ToString(),
                                Name = dr["exname"].ToString(),
                                sales = dr["sales"].ToString(),
                                target = dr["salestarget"].ToString(),
                                dealertarget = dr["dealertarget"].ToString()
                            });
                            totalsale = totalsale + Convert.ToDecimal(dr["sales"].ToString());
                            totaltarget = totaltarget + Convert.ToDecimal(dr["salestarget"].ToString());
                            totaldealertarget = totaldealertarget + Convert.ToDecimal(dr["dealertarget"].ToString());
                        }

                        Final.Add(new BranchWiseExecWiseDivWiseTargetFinal
                        {
                            BranchWiseExecWiseDivWiseTargetDetails = alldcr1,
                            TotalSale = totalsale.ToString(),
                            TotalTarget = totaltarget.ToString(),
                            TotaldealerTarget = totaldealertarget.ToString(),
                        });

                        g1.close_connection();
                        alldcr.Add(new BranchWiseExecWiseDivWiseTargets
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Final,
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