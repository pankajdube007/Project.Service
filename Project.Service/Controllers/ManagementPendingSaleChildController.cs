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
    public class ManagementPendingSaleChildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetPendingBranchWiseChild")]
        public HttpResponseMessage GetDetails(ListofManagementPendingSaleChild ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "" && ula.branchid !=0)
            {
                try
                {
                    string data1;

                    List<ManagementPendingSaleChilds> alldcr = new List<ManagementPendingSaleChilds>();
                    List<ManagementPendingSaleChild> alldcr1 = new List<ManagementPendingSaleChild>();

                    var dr = g1.return_dr("BranchwisePendingmanagementChild " + ula.branchid+",'"+ula.type+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementPendingSaleChild
                            {
                               
                                itemnm = Convert.ToString(dr["itemnm"].ToString()),
                                qty = Convert.ToString(dr["qty"].ToString()),
                                amount = Convert.ToString(dr["amount"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementPendingSaleChilds
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