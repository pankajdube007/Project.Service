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
    public class YoutubeVideoController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getYoutubeVideo")]
        public HttpResponseMessage GetDetails(ListsofYoutubeVideoAction ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<YoutubeVideos> alldcr = new List<YoutubeVideos>();
                    List<YoutubeVideoProduct> product = new List<YoutubeVideoProduct>();
                    List<YoutubeVideoAdvertisement> advertisement = new List<YoutubeVideoAdvertisement>();
                    List<YoutubeVideoEvents> events = new List<YoutubeVideoEvents>();
                    List<YoutubeVideoTechnical> technical = new List<YoutubeVideoTechnical>();
                    List<YoutubeVideoFinal> youtubefinal = new List<YoutubeVideoFinal>();

                    var dr = g1.return_dr("App_YouTubeMasterEvent");
                    var dr1 = g1.return_dr("App_YouTubeMasterAdvertisement");
                    var dr2 = g1.return_dr("App_YouTubeMasterProduct");
                    var dr3 = g1.return_dr("App_YouTubeMastertechnical");
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            events.Add(new YoutubeVideoEvents
                            {
                                videolink = Convert.ToString(dr["VideoLink"].ToString()),
                                images = baseurl + "youtube/" + Convert.ToString(dr["Images"].ToString().TrimStart(',').TrimEnd(',').Replace(",", "")),
                                subject = Convert.ToString(dr["Subject"].ToString()),
                                details = Convert.ToString(dr["Details"].ToString()),
                                hour = Convert.ToString(dr["TimeHH"].ToString()),
                                minute = Convert.ToString(dr["TimeMM"].ToString()),
                                second = Convert.ToString(dr["TimeTT"].ToString()),
                            });
                        }
                    }

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            advertisement.Add(new YoutubeVideoAdvertisement
                            {
                                videolink = Convert.ToString(dr1["VideoLink"].ToString()),
                                images = baseurl + "youtube/" + Convert.ToString(dr1["Images"].ToString()),
                                subject = Convert.ToString(dr1["Subject"].ToString()),
                                details = Convert.ToString(dr1["Details"].ToString()),
                                hour = Convert.ToString(dr1["TimeHH"].ToString()),
                                minute = Convert.ToString(dr1["TimeMM"].ToString()),
                                second = Convert.ToString(dr1["TimeTT"].ToString()),
                            });
                        }
                    }

                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            product.Add(new YoutubeVideoProduct
                            {
                                videolink = Convert.ToString(dr2["VideoLink"].ToString()),
                                images = baseurl + "youtube/" + Convert.ToString(dr2["Images"].ToString()),
                                subject = Convert.ToString(dr2["Subject"].ToString()),
                                details = Convert.ToString(dr2["Details"].ToString()),
                                hour = Convert.ToString(dr2["TimeHH"].ToString()),
                                minute = Convert.ToString(dr2["TimeMM"].ToString()),
                                second = Convert.ToString(dr2["TimeTT"].ToString()),
                            });
                        }
                    }

                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            technical.Add(new YoutubeVideoTechnical
                            {
                                videolink = Convert.ToString(dr3["VideoLink"].ToString()),
                                images = baseurl + "youtube/" + Convert.ToString(dr3["Images"].ToString()),
                                subject = Convert.ToString(dr3["Subject"].ToString()),
                                details = Convert.ToString(dr3["Details"].ToString()),
                                hour = Convert.ToString(dr3["TimeHH"].ToString()),
                                minute = Convert.ToString(dr3["TimeMM"].ToString()),
                                second = Convert.ToString(dr3["TimeTT"].ToString()),
                            });
                        }
                    }

                    youtubefinal.Add(new YoutubeVideoFinal
                    {
                        events = events,
                        advertisement = advertisement,
                        product = product,
                        technical=technical,
                    });

                    g1.close_connection();
                    alldcr.Add(new YoutubeVideos
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = youtubefinal,
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