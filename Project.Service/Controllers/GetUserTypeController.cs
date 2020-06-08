using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using static Project.Service.Models.GetUserType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Project.Service.Models;

namespace Project.Service.Controllers
{
    public class GetUserTypeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetUserType")]
        public HttpResponseMessage GetDetails(InputList ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.userid != "")
            {
                try
                {
                    string data1;

                    List<GetResults> alldcr = new List<GetResults>();
                    List<UserType> alldcr1 = new List<UserType>();

                    var dr = g2.return_dr("Appusertype '" + ula.userid + "'"); 

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new UserType
                            {
                               
                                name = Convert.ToString(dr["name"].ToString()),
                                userid = Convert.ToString(dr["usernm"].ToString()),
                                usertype = Convert.ToString(dr["usertype"].ToString()),
                              
                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new GetResults
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}