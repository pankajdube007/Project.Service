using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class AgingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getAging")]
        public HttpResponseMessage GetDetails(ListsofAging ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Agings> alldcr = new List<Agings>();
                    List<Aging> alldcr1 = new List<Aging>();
                    List<AgingDeatail> AgingDeatail = new List<AgingDeatail>();
                    List<Agingurl> Agingurl = new List<Agingurl>();
                    var dr = g1.return_dt("App_outsatndinghistrypartywiseduedays2allbranch '" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            AgingDeatail.Add(new AgingDeatail
                            {
                                days = Convert.ToString(dr.Rows[i]["duedays"]),
                                Amount = Convert.ToString(dr.Rows[i]["outstandingamt"])
                            });
                        }

                        Agingurl.Add(new Agingurl
                        {
                            url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "Outstanding-Report-View.aspx?Receiptwith=Default&partyid=" + Convert.ToString(dr.Rows[0]["partyid"])
                            + "&fromdate=01/04/2015&todate=" + DateTime.Now.ToString("dd/MM/yyyy") + "&branchid=" + Convert.ToString(dr.Rows[0]["typecat"])
                        });

                        alldcr1.Add(new Aging
                        {
                            AgingDetails = AgingDeatail,
                            Agingurls = Agingurl,
                        });

                        g1.close_connection();
                        alldcr.Add(new Agings
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