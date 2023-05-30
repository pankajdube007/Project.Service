using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class ManagementDhbOrderDetailsByProductandStateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ManagementDhbOrderDetailsByProductandState")]
        public HttpResponseMessage GetDetails(ManagementDhbOrderDetailsByProductandState ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DataManagementDhbOrderDetailsByProductandStates> alldcr = new List<DataManagementDhbOrderDetailsByProductandStates>();
                    List<DataManagementDhbOrderDetailsByProductandState> alldcr1 = new List<DataManagementDhbOrderDetailsByProductandState>();

                    var dr = g1.return_dr("GetOrderDetailsByProductandState " + ula.ProductId + "," + ula.StateId + ",'" + ula.Fromdate + "','" + ula.Todate + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DataManagementDhbOrderDetailsByProductandState
                            {
                               
                                ProductName = Convert.ToString(dr["ProductName"].ToString()),
                                OrderNumber = Convert.ToString(dr["OrderNumber"].ToString()),
                                ShopName = Convert.ToString(dr["ShopName"].ToString()),
                                CIN = Convert.ToString(dr["CIN"].ToString()),
                                Orderstatus = Convert.ToString(dr["OrderStatus"].ToString()),
                                CustomerName = Convert.ToString(dr["CustomerName"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                Workstate = Convert.ToString(dr["WorkState"].ToString())

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DataManagementDhbOrderDetailsByProductandStates
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