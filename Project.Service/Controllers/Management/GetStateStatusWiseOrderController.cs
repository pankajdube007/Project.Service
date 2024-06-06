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
    public class GetStateStatusWiseOrderController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetStateStatusWise")]
        public HttpResponseMessage GetDetails(ListGetStateStatusWiseOrder ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetStateStatusWiseOrders> alldcr = new List<GetStateStatusWiseOrders>();
                    List<GetStateStatusWiseOrder> alldcr1 = new List<GetStateStatusWiseOrder>();
                    var dr = g1.return_dr($"usp_GetStateStatusWiseOrderDetails_API  {ula.StatusId} , '{ula.PivotHeader}' , {ula.StateId} , {ula.UserCategoryID} , '{ula.Category}' ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetStateStatusWiseOrder
                            {
                                OrderNumber = Convert.ToString(dr["OrderNumber"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                MobileNo = Convert.ToString(dr["MobileNo"]),
                                CatgeoryName = Convert.ToString(dr["CatgeoryName"]),
                                OrderDate = Convert.ToString(dr["OrderDate"]),
                                OrderApprovalDate = Convert.ToString(dr["OrderApprovalDate"]),
                                OrderQuantity = Convert.ToString(dr["OrderQuantity"]),
                                OrderPendingDays = Convert.ToString(dr["OrderPendingDays"]),
                                OrderStatus = Convert.ToString(dr["OrderStatus"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                ProductImage = Convert.ToString(dr["ProductImage"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetStateStatusWiseOrders
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