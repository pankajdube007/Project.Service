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

namespace Project.Service
{
    public class DiscoverWorldController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDiscoverWorld")]
        public HttpResponseMessage GetDetails(DiscoverWorldAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            try
            {
                string data1;

                List<DiscoverWorlds> alldcr = new List<DiscoverWorlds>();
                List<DiscoverWorld> alldcr1 = new List<DiscoverWorld>();
                List<DiscoverWorldDetails> DiscoverWorldDetails = new List<DiscoverWorldDetails>();
                List<DiscoverWorldurls> DiscoverWorldurls = new List<DiscoverWorldurls>();
                var dr = g1.return_dt("App_SaleSummaryDiscover '" + ula.CIN + "','" + ula.FinYear + "'");

                if (dr.Rows.Count > 0)
                {
                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        DiscoverWorldDetails.Add(new DiscoverWorldDetails
                        {
                            currentamt = dr.Rows[i]["curryramt"].ToString(),
                            lstyramt = dr.Rows[i]["lstyramt"].ToString(),
                            target = dr.Rows[i]["curtarget"].ToString(),
                            bellowbase = dr.Rows[i]["bellowbase"].ToString(),
                            bellowamt = dr.Rows[i]["bellowamt"].ToString(),
                            salereturnamt = dr.Rows[i]["curryramt"].ToString(),
                            points = dr.Rows[i]["points"].ToString(),
                            bonus = dr.Rows[i]["bonus"].ToString(),
                            growthbonus = dr.Rows[i]["growthbonus"].ToString(),
                            totalpoint = dr.Rows[i]["totalpoint"].ToString()
                        });
                    }

                    var dr1 = g1.return_dt("App_LoyaltyFileUpload 4");
                    string baseurl = _goldMedia.MapPathToPublicUrl("");

                    if (dr1.Rows.Count > 0)
                    {
                        DiscoverWorldurls.Add(new DiscoverWorldurls
                        {
                            viewinfourl = string.IsNullOrEmpty(dr1.Rows[0]["link"].ToString()) ? "" : (baseurl + "loyaltyfile/" + dr1.Rows[0]["link"].ToString()),
                            detailsworkingurl = ""
                        });
                    }
                    else
                    {
                        DiscoverWorldurls.Add(new DiscoverWorldurls
                        {
                            viewinfourl = "",
                            detailsworkingurl = ""
                        });
                    }

                    alldcr1.Add(new DiscoverWorld
                    {
                        Details = DiscoverWorldDetails,
                        url = DiscoverWorldurls,
                    });

                    g1.close_connection();
                    alldcr.Add(new DiscoverWorlds
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