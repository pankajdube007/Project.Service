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
    public class DealerDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDealerDetails")]
        public HttpResponseMessage GetDetails(ListsofDealerDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DealerDetailss> alldcr = new List<DealerDetailss>();
                    List<DealerDetails> alldcr1 = new List<DealerDetails>();
                    var dr = g1.return_dr("App_dcrlogindetlshow '" + ula.CIN + "','Party'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DealerDetails
                            {
                                usernm = Convert.ToString(dr["usernm"].ToString().TrimEnd().TrimStart()),
                                email = Convert.ToString(dr["email"].ToString()),
                                mobile = Convert.ToString(dr["mobile"].ToString()),
                                firmname = Convert.ToString(dr["firmnm"].ToString()),
                                exname = Convert.ToString(dr["salesexnm"].ToString()),
                                exmobile = Convert.ToString(dr["salesmobile"].ToString()),
                                exhead = Convert.ToString(dr["salesexheadnm"].ToString()),
                                exheadmobile = Convert.ToString(dr["salesexheadmobile"].ToString()),
                                gstno = Convert.ToString(dr["gstno"].ToString()),
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                branchid = Convert.ToInt32(dr["homebranchid"].ToString()),
                                branchnm = Convert.ToString(dr["locnm"].ToString()),
                                stateid = Convert.ToInt32(dr["stateid"].ToString()),
                                status = Convert.ToString(dr["status"].ToString()),
                                issuccess = Convert.ToBoolean(dr["issuccess"].ToString()),
                                isblock = Convert.ToBoolean(dr["isblock"].ToString()),
                                lastsynclead = Convert.ToString(dr["lastsyncdt"].ToString()),
                                branchadd = Convert.ToString(dr["branchadd"].ToString()),
                                branchphone = Convert.ToString(dr["offphone1"].ToString()),
                                branchemail = Convert.ToString(dr["branchemail"].ToString())
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DealerDetailss
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