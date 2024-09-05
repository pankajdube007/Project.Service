using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.Management;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class ExecCostingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ExecCosting")]
        public HttpResponseMessage GetDetails(ExecCosting ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ExecCostingLists> alldcr = new List<ExecCostingLists>();
                    List<ExecCostingList> alldcr1 = new List<ExecCostingList>();
                    
                    var dr = g1.return_dr("GetExecCosting " + ula.branchid + " ,'" + ula.Category + "','" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecCostingList
                            {

                                SalesExName = Convert.ToString(dr["salesexnm"].ToString()),
                                Division = Convert.ToString(dr["divinm"].ToString()),
                                Joindt = Convert.ToDateTime(dr["joindt"]).ToString("dd-MM-yyyy"),
                                BranchName = Convert.ToString(dr["branchname"].ToString()),
                                lastyearsale_Hierarchy = Convert.ToString(dr["lastYrSale"].ToString()),
                                TillTillDateSale_Hierarchy = Convert.ToString(dr["TillDateSale"].ToString()),
                                lastYrSalesingle_Own = Convert.ToString(dr["lastYrSalesingle"].ToString()),
                                TillDateSalesingle_Own = Convert.ToString(dr["TillDateSalesingle"].ToString()),
                                costper = string.IsNullOrEmpty(dr["costper"]?.ToString()) ? "0" : dr["costper"].ToString(),
                                LastUpdate= Convert.ToDateTime(dr["lastupdate"]).ToString("dd-MM-yyyy"),
                                Costperhy = string.IsNullOrEmpty(dr["costperhy"]?.ToString()) ? "0" : dr["costperhy"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecCostingLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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
