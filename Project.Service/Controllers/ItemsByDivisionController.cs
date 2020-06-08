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
    public class ItemsByDivisionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getItemByDivision")]
        public HttpResponseMessage GetAllUserLatLong(ListofItemByDivision ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.DivisionId != 0)
            {
                try
                {
                    string data1;
                    List<ItemByDivisions> alldcr = new List<ItemByDivisions>();
                    List<ItemByDivision> alldcr1 = new List<ItemByDivision>();
                    var dr = g1.return_dr("AppItemByDivision " + ula.DivisionId);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemByDivision
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                itemnmnm = Convert.ToString(dr["itemnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ItemByDivisions
                        {
                            result = "True",
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Items available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

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