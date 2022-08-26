using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using NLog;
using Project.Service.Models.GParivar;

namespace Project.Service.Controllers.GParivar
{
    public class MenInBlueSumPartyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getMenInBlueSumPartyList")]
        public HttpResponseMessage GetDetails(ListofMenInBlueSumParty ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetMenInBlueSumPartyLists> alldcr = new List<GetMenInBlueSumPartyLists>();
                    List<GetMenInBlueSumPartyList> alldcr1 = new List<GetMenInBlueSumPartyList>();

                    List<GetMenInBlueSumDetailList> alldcr2 = new List<GetMenInBlueSumDetailList>();

                    List<GetFinalLists> Final = new List<GetFinalLists>();

                    var dr = g1.return_dr("dbo.gpariwarmeninbluesum '" + ula.CIN + "'");
                    var dr1 = g1.return_dr("dbo.meninbluesumdetail '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetMenInBlueSumPartyList
                            {

                                Name = Convert.ToString(dr["name"].ToString()),
                                Partyid = Convert.ToString(dr["partyid"].ToString()),
                                TypeCat = Convert.ToString(dr["typecat"].ToString()),
                                TotalPoints = Convert.ToString(dr["totalpoints"].ToString()),
                                DisplayName = Convert.ToString(dr["displaynm"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                CurrentPrice = Convert.ToString(dr["CurPrice"].ToString()),
                                CurrentPriceImg = string.IsNullOrEmpty(dr["CurPriceimg"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr["CurPriceimg"]).ToString().TrimEnd(',')),
                                NextPrice = Convert.ToString(dr["NextPrice"].ToString()),
                                NextPriceImg = string.IsNullOrEmpty(dr["NextPriceimg"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr["NextPriceimg"]).ToString().TrimEnd(',')),
                                cin = Convert.ToString(dr["cin"].ToString()),
                            });
                        }
                    }

                    if (dr1.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr1.Read())
                        {
                            alldcr2.Add(new GetMenInBlueSumDetailList
                            {
                                Partyid = Convert.ToString(dr["partyid"].ToString()),
                                Division = Convert.ToString(dr["Division"].ToString()),
                                Slab = Convert.ToString(dr["slb"].ToString()),
                                Sale = Convert.ToString(dr["Sale"].ToString()),
                                Point = Convert.ToString(dr["point"].ToString()),
                                PartyName = Convert.ToString(dr["PartyName"].ToString()),

                            });
                        }

                    }

                    Final.Add(new GetFinalLists
                    {

                        partyList = alldcr1,
                        detailList = alldcr2

                    });

                    g1.close_connection();
                    alldcr.Add(new GetMenInBlueSumPartyLists
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = Final,
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;

                    //else
                    //{
                    //    g1.close_connection();
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                    //    return response;
                    //}
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