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
    //[EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*", PreflightMaxAge = 1728000, SupportsCredentials =false)]
    public class AllItemDatasController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ItemGetByText")]
        public HttpResponseMessage GetsItemGetByText(AllItemDataActions llu)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(llu.uniquekey))
            {
                try
                {
                    string data1;
                    List<itemDesc> alldcr = new List<itemDesc>();
                    List<itemDescs> alldcr1 = new List<itemDescs>();
                    var dr = g1.return_dr("itemselectsearchwithscheme2Edited " + llu.categoryid + "," + llu.branchid + "," + llu.lbranchid + "," + llu.partycat + "," + llu.partyid + ",'" + llu.pocat + "'," + llu.subtaxtype + ",'" + llu.searchtwxt + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new itemDescs
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                ProductCode = Convert.ToString(dr["ProductCode"].ToString()),
                                detail = Convert.ToString(dr["detail"].ToString()),
                                divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new itemDesc
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //response.Content = new StringContent(cm.StatusTime(false, "No Item In This String"), Encoding.Unicode);
                        response.Content = new StringContent(cm.StatusTime(false, "No Item In This String"), Encoding.UTF8, "application/json");

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