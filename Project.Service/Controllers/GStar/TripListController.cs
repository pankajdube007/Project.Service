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
    public class TripListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTripList")]
        public HttpResponseMessage GetDetails(ListTripList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetTripLists> alldcr = new List<GetTripLists>();
                    List<GetTripList> alldcr1 = new List<GetTripList>();
                    var dr = g1.return_dr("dbo.TripList '" + ula.ExId + "','" + ula.VehId + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetTripList
                            {

                                exeid = Convert.ToString(dr["exeid"].ToString()),
                                vehid = Convert.ToString(dr["vehid"].ToString()),
                                date = Convert.ToString(dr["date"].ToString()),
                                refno = Convert.ToString(dr["refno"].ToString()),
                                starttripimg = string.IsNullOrEmpty(dr["starttripimg"].ToString().TrimEnd(',')) ? string.Empty : ( Convert.ToString(dr["starttripimg"]).ToString().TrimEnd(',')),
                                fromkm = Convert.ToString(dr["fromkm"].ToString()),
                                endtripimg = string.IsNullOrEmpty(dr["endtripimg"].ToString().TrimEnd(',')) ? string.Empty : ( Convert.ToString(dr["endtripimg"]).ToString().TrimEnd(',')),
                                tokm = Convert.ToString(dr["tokm"].ToString()),
                                VehicleNo = Convert.ToString(dr["VehicleNo"].ToString()),
                                model = Convert.ToString(dr["model"].ToString()),
                                mfgby = Convert.ToString(dr["mfgby"].ToString()),
                                VehicleType = Convert.ToString(dr["VehicleType"].ToString()),
                                OwnedBy = Convert.ToString(dr["OwnedBy"].ToString()),
                                StartImgUploadBy = Convert.ToString(dr["suploadby"].ToString()),
                                EndImgUploadBy = Convert.ToString(dr["euploadby"].ToString()),
                                IsCompleted = Convert.ToString(dr["imgstatus"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                IsEdited = Convert.ToString(dr["editstatus"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetTripLists
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