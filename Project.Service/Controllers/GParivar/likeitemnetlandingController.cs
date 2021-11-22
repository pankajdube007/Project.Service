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
    public class likeitemnetlandingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getnetlandinglikeitem")]
        public HttpResponseMessage GetDetails(Listslikeitemnetlanding ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<likeitemnetlandings> alldcr = new List<likeitemnetlandings>();
                    List<likeitemnetlanding> alldcr1 = new List<likeitemnetlanding>();

                    var dr = g1.return_dr("execlikeitemnetlanding'" + ula.ExId + "','" + ula.ItemId + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new likeitemnetlanding
                            {

                                ItemId = Convert.ToInt32(dr["slno"].ToString()),
                                ItemName = Convert.ToString(dr["ProductCode"].ToString()),
                                CatName = Convert.ToString(dr["categorynm"].ToString()),
                                Subcatname = Convert.ToString(dr["rangenm"].ToString()),
                                NetLanding = Convert.ToDecimal(dr["netland"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new likeitemnetlandings
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