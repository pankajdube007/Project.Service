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
using NLog;

namespace Project.Service.Controllers.GStar
{
    public class MenInBlueSumExecController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getMenInBlueSumExecList")]
        public HttpResponseMessage GetDetails(ListofMenInBlueSumExec ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetMenInBlueSumExecLists> alldcr = new List<GetMenInBlueSumExecLists>();
                    List<GetMenInBlueSumExecList> alldcr1 = new List<GetMenInBlueSumExecList>();
                    var dr = g1.return_dr("dbo.gstarmeninbluesum '" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetMenInBlueSumExecList
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
                        g1.close_connection();
                        alldcr.Add(new GetMenInBlueSumExecLists
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