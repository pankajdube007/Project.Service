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
    public class ManagementBranchwiseOuststandingChildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetManagementBranchwiseOutstandingChild")]
        public HttpResponseMessage GetDetails(ListofManagementBranchwiseOuststandingChilld ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ManagementBranchwiseOuststandingChillds> alldcr = new List<ManagementBranchwiseOuststandingChillds>();
                    List<ManagementBranchwiseOuststandingChilld> alldcr1 = new List<ManagementBranchwiseOuststandingChilld>();

                    var dr = g1.return_dr("BranchwiseOutstandingManagementChild '"+ula.branchid+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementBranchwiseOuststandingChilld
                            {
                                partynm = Convert.ToString(dr["partynm"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                partystatus = Convert.ToString(dr["workstatus"].ToString()),
                                city = Convert.ToString(dr["city"].ToString()),
                                lstinvoicedt = Convert.ToString(dr["lastinvoicedate"].ToString()),
                                lstpaymentdt = Convert.ToString(dr["lastpaymentdate"].ToString()),
                                lstpaymentamt = Convert.ToString(dr["lastpaymentamt"].ToString()),
                                outstandingamt = Convert.ToString(dr["totaloutstanding"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementBranchwiseOuststandingChillds
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