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
    public class BranchWiseExeSaleAndCostController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getbranchwiseexesaleandcost")]
        public HttpResponseMessage GetAllUserdetails(ListofExecWiseCost ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<ExecWiseCostLists> alldcr = new List<ExecWiseCostLists>();
                    List<ExecWiseCostList> alldcr1 = new List<ExecWiseCostList>();
                    var dr = g1.return_dr("BranchWiseExeSaleAndCost '" + ula.CIN + "','" + ula.Branch + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecWiseCostList
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                JoinDate = Convert.ToString(dr["JoinDate"].ToString()),
                                cost = Convert.ToString(dr["cost"].ToString()),
                                expense = Convert.ToString(dr["expense"].ToString()),
                                lastyearsale = Convert.ToString(dr["lastyearsale"].ToString()),
                                Target = Convert.ToString(dr["Target"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecWiseCostLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}