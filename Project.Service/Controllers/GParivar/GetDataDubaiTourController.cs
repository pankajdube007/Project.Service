using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Project.Service.Models.GParivar;


namespace Project.Service.Controllers.GParivar
{
    public class GetDataDubaiTourController : ApiController
    {
        [HttpPost]
        [Filters.ValidateModel]
        [Route("api/getdubaitourdata")]
        public HttpResponseMessage GetDetails(ListGetDataDubaiTour ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<GetDataDubaiTour> alldcr = new List<GetDataDubaiTour>();
                    List<GetDataDubaiTours> alldcr1 = new List<GetDataDubaiTours>();
                    var dr = g1.return_dr("Dubaitourgetdataapp '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetDataDubaiTours
                            {
                                DisplayName = Convert.ToString(dr["displaynm"]),
                                Homebranch = Convert.ToString(dr["homebrnch"]),
                                HomebranchId = Convert.ToString(dr["homebrnchid"]),
                                PartyId = Convert.ToString(dr["partyid"]),
                                Typecat = Convert.ToString(dr["typecat"]),
                                Sale = Convert.ToString(dr["sale"]),
                                GiftCount = Convert.ToString(dr["giftcnt"]),
                                CnAmount = Convert.ToString(dr["cnamt"]),
                                OutStanding = Convert.ToString(dr["oustanding"]),
                                Slab = Convert.ToString(dr["slab"]),
                                per = Convert.ToString(dr["per"]),
                                isallow = Convert.ToString(dr["isallow"]),
                                isactive = Convert.ToString(dr["isactive"]),
                                pdfurl = string.IsNullOrEmpty(dr["pdflink"].ToString().Trim(',')) ? "" : (baseurl + "dubai/" + dr["pdflink"].ToString().Trim(','))
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetDataDubaiTour
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