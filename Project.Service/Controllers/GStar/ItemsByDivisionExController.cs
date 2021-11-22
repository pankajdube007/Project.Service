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
    public class ItemsByDivisionExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getItemByDivisionEx")]
        public HttpResponseMessage GetAllUserdetails(ListofItembyDivisionEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<ItembyDivisionExs> alldcr = new List<ItembyDivisionExs>();
                    List<ItembyDivisionEx> alldcr1 = new List<ItembyDivisionEx>();
                    var dr = g1.return_dr("AppItemByDivisionEx '" + ula.CIN+"',"+ula.DivisionId);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItembyDivisionEx
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                itemnmnm = Convert.ToString(dr["itemnm"].ToString()),
                                previnvoice = Convert.ToString(dr["previnvoice"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ItembyDivisionExs
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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