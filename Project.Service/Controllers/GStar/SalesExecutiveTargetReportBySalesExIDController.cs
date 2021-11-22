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
    public class SalesExecutiveTargetReportBySalesExIDController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesExecutiveTargetReportBySalesExID")]
        public HttpResponseMessage GetDetails(ListsofSalesExecutiveTargetReportBySalesExID ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<SalesExecutiveTargetReportBySalesExIDLists> alldcr = new List<SalesExecutiveTargetReportBySalesExIDLists>();
                    List<SalesExecutiveTargetReportBySalesExIDList> alldcr1 = new List<SalesExecutiveTargetReportBySalesExIDList>();
                    var dr = g1.return_dr("SalesExecutiveTargetReportBySalesExID '" + ula.FinYear + "','" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new SalesExecutiveTargetReportBySalesExIDList
                            {

                                branchids= Convert.ToString(dr["branchids"].ToString()),
                                locnm = Convert.ToString(dr["locnm"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                CurrentYrsSales = Convert.ToString(dr["CurrentYrsSales"].ToString()),
                                TargetGivenByBranch = Convert.ToString(dr["TargetGivenByBranch"].ToString()),
                                TargetAchived = Convert.ToString(dr["TagetAchived"].ToString()),
                                HOTarget = Convert.ToString(dr["HOTarget"].ToString()),
                                Achived = Convert.ToString(dr["Achived"].ToString()),
                                ShortFall = Convert.ToString(dr["ShortFall"].ToString()),
                                Q1CurrentYrsSales = Convert.ToString(dr["Q1CurrentYrsSales"].ToString()),
                                Q1TargetGivenByBranch = Convert.ToString(dr["Q1TargetGivenByBranch"].ToString()),
                                Q1HOTarget = Convert.ToString(dr["Q1HOTarget"].ToString()),
                                Q1Achived = Convert.ToString(dr["Q1Achived"].ToString()),
                                Q1ShortFall = Convert.ToString(dr["Q1ShortFall"].ToString()),
                                Q2CurrentYrsSales = Convert.ToString(dr["Q2CurrentYrsSales"].ToString()),
                                Q2TargetGivenByBranch = Convert.ToString(dr["Q2TargetGivenByBranch"].ToString()),
                                Q2HOTarget = Convert.ToString(dr["Q2HOTarget"].ToString()),
                                Q2Achived = Convert.ToString(dr["Q2Achived"].ToString()),
                                Q2ShortFall = Convert.ToString(dr["Q2ShortFall"].ToString()),
                                Q3CurrentYrsSales = Convert.ToString(dr["Q3CurrentYrsSales"].ToString()),
                                Q3TargetGivenByBranch = Convert.ToString(dr["Q3TargetGivenByBranch"].ToString()),
                                Q3HOTarget = Convert.ToString(dr["Q3HOTarget"].ToString()),
                                Q3Achived = Convert.ToString(dr["Q3Achived"].ToString()),
                                Q3ShortFall = Convert.ToString(dr["Q3ShortFall"].ToString()),
                                Q4CurrentYrsSales = Convert.ToString(dr["Q4CurrentYrsSales"].ToString()),
                                Q4TargetGivenByBranch = Convert.ToString(dr["Q4TargetGivenByBranch"].ToString()),
                                Q4HOTarget = Convert.ToString(dr["Q4HOTarget"].ToString()),
                                Q4Achived = Convert.ToString(dr["Q4Achived"].ToString()),
                                Q4ShortFall = Convert.ToString(dr["Q4ShortFall"].ToString()),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new SalesExecutiveTargetReportBySalesExIDLists
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