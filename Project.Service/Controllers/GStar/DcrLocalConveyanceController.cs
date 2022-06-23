using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class DcrLocalConveyanceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDcrLocalConveyance")]
        public HttpResponseMessage GetDetails(ListofDcrLocalConveyance ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetDcrLocalConveyanceLists> alldcr = new List<GetDcrLocalConveyanceLists>();
                    List<GetDcrLocalConveyanceList> alldcr1 = new List<GetDcrLocalConveyanceList>();

                    List<GetexeccheckinoutlistdtwisesumList> alldcr2 = new List<GetexeccheckinoutlistdtwisesumList>();

                    List<GetFinalLists> Final = new List<GetFinalLists>();

                    var dr = g1.return_dr("dbo.execcheckinoutlistdtwise '" + ula.ExId + "','" + ula.date + "'");
                    var dr1 = g1.return_dr("dbo.execcheckinoutlistdtwisesum '" + ula.ExId + "','" + ula.date + "','" + ula.transportid + "'");


                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetDcrLocalConveyanceList
                            {

                                orgid = Convert.ToString(dr["orgid"].ToString()),
                                orgcat = Convert.ToString(dr["orgcat"].ToString()),
                                traveldistance = Convert.ToString(dr["traveldistance"].ToString()),
                                orgnm = Convert.ToString(dr["orgnm"].ToString()),

                            });
                        }

                    }

                    if (dr1.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr1.Read())
                        {
                            alldcr2.Add(new GetexeccheckinoutlistdtwisesumList
                            {

                                traveldistanceinout = Convert.ToString(dr1["traveldistanceinout"].ToString()),
                                claimablediastance = Convert.ToString(dr1["claimablediastance"].ToString()),
                                odomtrkm = Convert.ToString(dr1["odomtrkm"].ToString()),
                                BikeRate = Convert.ToString(dr1["BikeRate"].ToString()),
                                CarRate = Convert.ToString(dr1["CarRate"].ToString()),
                                balancekm = Convert.ToString(dr1["balancekm"].ToString()),
                                balanceamt = Convert.ToString(dr1["balanceamt"].ToString()),
                            });
                        }

                    }

                    Final.Add(new GetFinalLists {

                        LocalConveyanceList = alldcr1,
                        listdtwisesumList = alldcr2

                        //listdtwisesumList = alldcr2,
                        //LocalConveyanceList= alldcr1
                    });


                    g1.close_connection();
                        alldcr.Add(new GetDcrLocalConveyanceLists
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
                    //}
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