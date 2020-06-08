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
    public class ListofWarehouseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ListOfWarehouse")]
        public HttpResponseMessage GetAllUserLatLong(ListofWareHoseAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;

                    List<Warehouses> alldcr = new List<Warehouses>();
                    List<Warehouse> alldcr1 = new List<Warehouse>();
                    var dr = g1.return_dr("App_WarehoseDeatils '" + ula.Branchid + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Warehouse
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                warehousenm = Convert.ToString(dr["locnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Warehouses
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  Warehouse available"), Encoding.UTF8, "application/json");

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