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
    public class DcrInputDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDcrInputExcutive")]
        public HttpResponseMessage GetDetails(ListsofDcrInputDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<DcrInputDetailss> alldcr = new List<DcrInputDetailss>();
                    List<contactmode> contactmodess = new List<contactmode>();
                    List<purposelist> purposelistss = new List<purposelist>();
                    List<productlist> productlistss = new List<productlist>();
                    List<transportlist> transportlistss = new List<transportlist>();
                    List<DcrInputDetailsFinal> Final = new List<DcrInputDetailsFinal>();
                    var dr = g1.return_dr("AppContactModeExecutive");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            contactmodess.Add(new contactmode
                            {
                                slno = Convert.ToInt32(dr["SlNo"]),
                                name = Convert.ToString(dr["contactmodename"])
                            });
                        }
                    }

                    var dr1 = g1.return_dr("AppPurposeExecutive");
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            purposelistss.Add(new purposelist
                            {
                                slno = Convert.ToInt32(dr1["SlNo"]),
                                name = Convert.ToString(dr1["name"])
                            });
                        }
                    }

                    var dr2 = g1.return_dr("AppProductListExecutive");
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            productlistss.Add(new productlist
                            {
                                slno = Convert.ToInt32(dr2["SlNo"]),
                                name = Convert.ToString(dr2["categorynm"])
                            });
                        }
                    }

                    var dr3 = g1.return_dr("AppTransportListExecutive");
                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            transportlistss.Add(new transportlist
                            {
                                slno = Convert.ToInt32(dr3["slno"]),
                                name = Convert.ToString(dr3["transport"])
                            });
                        }
                    }


                    Final.Add(new DcrInputDetailsFinal
                    {
                        contactmodedata = contactmodess,
                        purposelistdata = purposelistss,
                        productlistdata = productlistss,
                        transportlistdata=transportlistss
                    });

                    g1.close_connection();
                    alldcr.Add(new DcrInputDetailss
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = Final,
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
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