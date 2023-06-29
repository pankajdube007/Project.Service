using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class SearchbyMobilenoController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetUserbyMobile")]
        public HttpResponseMessage GetDetails(SearchbyMobilenotList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExecId != 0)
            {
                try
                {
                    string data1;


                    List<SearchbyMobilenoS> alldcr = new List<SearchbyMobilenoS>();
                    List<SearchbyMobileno> alldcr1 = new List<SearchbyMobileno>();
                    var dr = g1.return_dr($"exec NukkadmeetmobileSearch {ula.Mobile} ");
                    if (dr.HasRows)
                    {


                        while (dr.Read())
                        {
                            alldcr1.Add(new SearchbyMobileno
                            {

                                Slno = Convert.ToString(dr["SlNo"].ToString()),
                                Name = Convert.ToString(dr["UserName"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                Mobile = Convert.ToString(dr["MobileNo"].ToString()),
                                Category = Convert.ToString(dr["categorynm"].ToString()),
                                Status = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                Address = Convert.ToString(dr["add1"].ToString()),
                                Pincode = Convert.ToString(dr["Pincode"].ToString()),
                                Photo = Convert.ToString(dr["ProfileImageURL"].ToString())
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new SearchbyMobilenoS
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