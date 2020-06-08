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
    public class PartywisesecureamtController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getManagementpartywisesecuredamt")]
        public HttpResponseMessage GetDetails(ListOfPartyWiseSecureAmt ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<partysecureamts> alldcr = new List<partysecureamts>();
                    List<partysecureamt> alldcr1 = new List<partysecureamt>();
                    var dr = g1.return_dr("branchinsubal '" + ula.Branch + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new partysecureamt
                            {

                                Name = Convert.ToString(dr["displaynmwitharea"]),
                                Outstanding = Convert.ToDecimal(dr["outstng"].ToString()),
                                Secured = Convert.ToDecimal(dr["cld"].ToString()),
                                Securedper = Convert.ToDecimal(dr["cldper"].ToString()),
                                UnSecured = Convert.ToDecimal(dr["balance"].ToString()),
                                UnSecuredper = Convert.ToDecimal(dr["balanceper"].ToString()),
                                Insurance = Convert.ToDecimal(dr["insu"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new partysecureamts
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