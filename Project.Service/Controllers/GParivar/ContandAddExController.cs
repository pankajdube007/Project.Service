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
    public class ContandAddExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrgAddDetailsExcutive")]
        public HttpResponseMessage GetDetails(ListsofOrgAddDetailsEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<OrgAddDetailsExs> alldcr = new List<OrgAddDetailsExs>();
                    List<OrgAddDetailsFinalEx> final = new List<OrgAddDetailsFinalEx>();
                    List<OrgContactpersonDetailsEx> contactpersonss = new List<OrgContactpersonDetailsEx>();
                    List<OrgAddDetailsEx> addresss = new List<OrgAddDetailsEx>();
                    var dr = g1.return_dr("Apporgaddselectdcr " + ula.OrgId + "," + ula.CatId);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            addresss.Add(new OrgAddDetailsEx
                            {
                                slno = Convert.ToInt32(dr["orgaddid"]),
                                name = Convert.ToString(dr["address"])
                            });
                        }
                    }

                    var dr1 = g1.return_dr("Apporgcontselectdcr " + ula.OrgId + "," + ula.CatId);
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            contactpersonss.Add(new OrgContactpersonDetailsEx
                            {
                                slno = Convert.ToInt32(dr1["orgcontid"]),
                                contactname = Convert.ToString(dr1["contactperson"])
                            });
                        }
                    }

                    final.Add(new OrgAddDetailsFinalEx
                    {
                        address = addresss,
                        contactperson = contactpersonss
                    });

                    g1.close_connection();
                    alldcr.Add(new OrgAddDetailsExs
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = final,
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