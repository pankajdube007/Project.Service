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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RetriveItemsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ItemByText")]
        public HttpResponseMessage AllGetsItemGetByText(RetriveItemAction llu)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            string data1, data2, data3, data4;
            if (cm.Validateo(llu.uniquekey))
            {
                try
                {
                    //string data1, data2, data3, data4;
                    // List<itemDesc> alldcr = new List<itemDesc>();
                    List<itemsDesc> alldcr1 = new List<itemsDesc>();
                    var dr = g1.return_dr("itemselectsearchwithscheme2Edited '" + llu.categoryid + "'," + llu.branchid + "," + llu.lbranchid + "," + llu.partycat + "," + llu.partyid + ",'" + llu.pocat + "'," + llu.subtaxtype + ",'" + llu.searchtwxt + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new itemsDesc
                            {
                                //slno = Convert.ToInt32(dr["slno"].ToString()),
                                ProductCode = Convert.ToString(dr["ProductCode"].ToString()),
                                detail = Convert.ToString(dr["detail"].ToString()),
                                // divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                            });
                        }
                        g1.close_connection();
                        data1 = JsonConvert.SerializeObject(alldcr1, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        List<itemsDesc> alldcr11 = new List<itemsDesc>();
                        alldcr11.Add(new itemsDesc
                        {
                            //slno = Convert.ToInt32(dr["slno"].ToString()),
                            ProductCode = "No Item In This String",
                            detail = "No Item In This String",
                            // divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                        });
                        g1.close_connection();
                        data2 = JsonConvert.SerializeObject(alldcr11, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data2, Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    List<itemsDesc> alldcr1111 = new List<itemsDesc>();
                    alldcr1111.Add(new itemsDesc
                    {
                        //slno = Convert.ToInt32(dr["slno"].ToString()),
                        ProductCode = "Oops! Something is wrong, try again later!!!!!!!!",
                        detail = "Oops! Something is wrong, try again later!!!!!!!!",
                        // divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                    });
                    data3 = JsonConvert.SerializeObject(alldcr1111, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data3, Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                List<itemsDesc> alldcr11111 = new List<itemsDesc>();
                alldcr11111.Add(new itemsDesc
                {
                    //slno = Convert.ToInt32(dr["slno"].ToString()),
                    ProductCode = "Please Log In",
                    detail = "Please Log In",
                    // divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                });
                data4 = JsonConvert.SerializeObject(alldcr11111, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent(data4, Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}