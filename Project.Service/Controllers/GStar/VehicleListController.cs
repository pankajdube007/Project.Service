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

namespace Project.Service.Controllers.GStar
{
    public class VehicleListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVehicleList")]
        public HttpResponseMessage GetDetails(ListVehicleList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetVehicleLists> alldcr = new List<GetVehicleLists>();
                    List<GetVehicleList> alldcr1 = new List<GetVehicleList>();
                    var dr = g1.return_dr("dbo.VehicleMstList '" + ula.ExId+"'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetVehicleList
                            {

                                VehicleId = Convert.ToString(dr["slno"].ToString()),
                                VehicleNo = Convert.ToString(dr["VehicleNo"].ToString()),
                                VehicleType = Convert.ToString(dr["VehicleType"].ToString()),
                                OwnedBy = Convert.ToString(dr["OwnedBy"].ToString()),
                                model = Convert.ToString(dr["model"].ToString()),
                                mfgby = Convert.ToString(dr["mfgby"].ToString()),
                                img = string.IsNullOrEmpty(dr["img"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "vehicleimage/" + Convert.ToString(dr["img"]).ToString().TrimEnd(',')),
                               

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetVehicleLists
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