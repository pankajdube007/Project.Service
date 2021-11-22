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
    public class SalesSummaryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesSummary")]
        public HttpResponseMessage GetDetails(SalesSummaryAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            try
            {
                string data1;

                List<SalesSummarys> alldcr = new List<SalesSummarys>();
                List<SalesSummary> alldcr1 = new List<SalesSummary>();
                List<SalesSummaryDetails> SalesSummaryDetails = new List<SalesSummaryDetails>();
                List<StarRewardurls> StarRewardurls = new List<StarRewardurls>();
                var dr = g1.return_dt("App_SaleSummary '" + ula.CIN + "','" + ula.FinYear + "'");

                if (dr.Rows.Count > 0)
                {
                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        SalesSummaryDetails.Add(new SalesSummaryDetails
                        {
                            division = dr.Rows[i]["division"].ToString(),
                            lstyrsales = dr.Rows[i]["lstyramt"].ToString(),
                            lstyrsalestrade = dr.Rows[i]["lstyramttrade"].ToString(),
                            curryrsales = dr.Rows[i]["curryramt"].ToString(),
                            curryrsalestrade = dr.Rows[i]["curryramttrade"].ToString(),
                        });
                    }

                    var dr1 = g1.return_dt("App_LoyaltyFileUpload 3");
                    string baseurl = _goldMedia.MapPathToPublicUrl("");

                    if (dr1.Rows.Count > 0)
                    {
                        StarRewardurls.Add(new StarRewardurls
                        {
                            url = string.IsNullOrEmpty(dr1.Rows[0]["link"].ToString()) ? "" : (baseurl + "loyaltyfile/" + dr1.Rows[0]["link"].ToString()),
                        });
                    }
                    else
                    {
                        StarRewardurls.Add(new StarRewardurls
                        {
                            url = "",
                        });
                    }

                    alldcr1.Add(new SalesSummary
                    {
                        SummaryDetails = SalesSummaryDetails,
                        StarRewardurl = StarRewardurls,
                    });

                    g1.close_connection();
                    alldcr.Add(new SalesSummarys
                    {
                        result = true,
                        message = "",
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
    }
}