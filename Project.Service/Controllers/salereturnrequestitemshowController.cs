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
    public class salereturnrequestitemshowController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getsalereturnrequestitemshow")]
        public HttpResponseMessage GetDetails(Listsalereturnrequestitemshow ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<salereturnrequestitemshows> alldcr = new List<salereturnrequestitemshows>();
                    List<salereturnrequestitemshow> alldcr1 = new List<salereturnrequestitemshow>();

                    var dr = g1.return_dr("salereturnrequestitemshow " + ula.salereturnrequestid + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new salereturnrequestitemshow
                            {
                                itemnmnm = Convert.ToString(dr["itemnm"].ToString()),
                                qty = Convert.ToDecimal(dr["qty"].ToString()),
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new salereturnrequestitemshows
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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