using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class StateWisePointChildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/statewisepointschild")]
        public HttpResponseMessage GetDetails(ListofStateWisePointchild ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<StateWisePointchilds> alldcr = new List<StateWisePointchilds>();
                    List<StateWisePointchild> alldcr1 = new List<StateWisePointchild>();
                    var dr = g1.return_dr("statewisepointschild '" + ula.CIN + "','" + ula.Category + "','"+ula.State+"','"+ula.Type+"'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new StateWisePointchild
                            {

                                profileid = Convert.ToString(dr["profileid"]),
                                FullName = Convert.ToString(dr["FullName"].ToString()),
                                Categorynm = Convert.ToString(dr["Categorynm"].ToString()),
                                PointType = Convert.ToString(dr["PointType"].ToString()),


                                MobileNo = Convert.ToString(dr["MobileNo"]),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                BalancePoints = Convert.ToString(dr["BalancePoints"].ToString()),
                                ShopName = Convert.ToString(dr["ShopName"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new StateWisePointchilds
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