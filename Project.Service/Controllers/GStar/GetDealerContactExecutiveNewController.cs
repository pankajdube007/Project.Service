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
    public class GetDealerContactExecutiveNewController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDealerContactExcutiveNew")]
        public HttpResponseMessage GetAllUserLatLong(ListofDisAndDealerContactExNew ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<DisAndDealerContactExsNew> alldcr = new List<DisAndDealerContactExsNew>();
                    List<DisAndDealerContactExNew> alldcr1 = new List<DisAndDealerContactExNew>();
                    List<DisAndDealerContactExFinalNew> DisAndDealerContactExFinalNews = new List<DisAndDealerContactExFinalNew>();
                  
                    var dr = g1.return_dt("AppcustomercontactExnew '" + ula.ExId + "'," + Convert.ToBoolean(ula.Hierarchy) + ",'" + ula.index + "','" + ula.Count + "','" + ula.Search + "'");
                    bool more = false;
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new DisAndDealerContactExNew
                            {
                                partynm = dr.Rows[i]["displaynm"].ToString(),
                                exnm = dr.Rows[i]["salesexname"].ToString(),
                                mobile = dr.Rows[i]["mobile"].ToString(),
                                emailid = dr.Rows[i]["emailid"].ToString(),
                                partytype = dr.Rows[i]["partycat"].ToString(),
                                address = dr.Rows[i]["Daddress"].ToString().Replace('\n', ' '),
                                gstno = dr.Rows[i]["GSTNo"].ToString(),
                                contactperson = dr.Rows[i]["contactperson"].ToString(),
                            });
                        }

                        DisAndDealerContactExFinalNews.Add(new DisAndDealerContactExFinalNew
                        {
                            DisAndDealerContact = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new DisAndDealerContactExsNew
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = DisAndDealerContactExFinalNews,
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