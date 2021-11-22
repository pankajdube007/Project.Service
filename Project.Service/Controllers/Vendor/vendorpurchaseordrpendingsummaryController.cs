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
    public class vendorpurchaseordrpendingsummaryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorpurchasepndingsummary")]
        public HttpResponseMessage GetAllUserdetails(Listofvendorpurchaseordrpendingsummary ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<vendorpurchaseordrpendingsummaryLists> alldcr = new List<vendorpurchaseordrpendingsummaryLists>();
                    List<vendorpurchaseordrpendingsummaryList> alldcr1 = new List<vendorpurchaseordrpendingsummaryList>();
                    var dr = g1.return_dr("vendorpurchaseordrpending '" + ula.PartyId + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorpurchaseordrpendingsummaryList
                            {
                                ItemCode = Convert.ToString(dr["BaseCode"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Color = Convert.ToString(dr["colornm"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                Barnch = Convert.ToString(dr["HomeBranch"].ToString()),
                                TotPendingQty = Convert.ToString(dr["pending"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorpurchaseordrpendingsummaryLists
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