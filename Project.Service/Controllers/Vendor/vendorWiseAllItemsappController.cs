using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Vendor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Vendor
{
    public class vendorWiseAllItemsappController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorWiseAllItemsapp")]
        public HttpResponseMessage GetAllUserdetails(vendorWiseAllItemsapp ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<vendorWiseAllItemsappLists> alldcr = new List<vendorWiseAllItemsappLists>();
                    List<vendorWiseAllItemsappList> alldcr1 = new List<vendorWiseAllItemsappList>();
                    var dr = g1.return_dr("vendorWiseAllItemsapp '" + ula.PartyId + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorWiseAllItemsappList
                            {
                                ProductCode1 = Convert.ToString(dr["ProductCode1"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorWiseAllItemsappLists
                        {
                            result = true,
                            item = string.Empty,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}