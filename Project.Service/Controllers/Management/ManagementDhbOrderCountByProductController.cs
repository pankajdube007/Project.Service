using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models.Management;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Project.Service.Controllers.Management
{
    public class ManagementDhbOrderCountByProductController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ManagementDhbOrderCountByProduct")]
        public HttpResponseMessage GetDetails(ManagementDhbOrderCountByProduct ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DataManagementDhbOrderCountByProducts> alldcr = new List<DataManagementDhbOrderCountByProducts>();
                    List<DataManagementDhbOrderCountByProduct> alldcr1 = new List<DataManagementDhbOrderCountByProduct>();

                    var dr = g1.return_dr("GetDhanabarseOrderCountByProduct '" + ula.Fromdate + "','" + ula.Todate + "'," + ula.StateId + ",'" + ula.Category +"'" );

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DataManagementDhbOrderCountByProduct
                            {                               
                                Productid = Convert.ToInt32(dr["ProductId"].ToString()),
                                Stateid = Convert.ToInt32(dr["StateID"].ToString()),
                                Productname = Convert.ToString(dr["ProductName"].ToString()),
                                Totalorder = Convert.ToInt32(dr["TotalOrders"].ToString()),
                                Approvalpending = Convert.ToInt32(dr["ApprovalPending"].ToString()),
                                Delivered = Convert.ToInt32(dr["Delivered"].ToString()),
                                Deliveredpending = Convert.ToInt32(dr["DeliveredPending"].ToString())

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DataManagementDhbOrderCountByProducts
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