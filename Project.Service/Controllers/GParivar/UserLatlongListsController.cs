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
    //[EnableCors(origins: "*", headers: "*", methods: "POST")]
    public class UserLatlongListsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ListUserLatLong")]
        public HttpResponseMessage GetAllUserLatLong(UserLatlongListAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;
                    List<listlatlongs> alldcr = new List<listlatlongs>();
                    List<listlatlong> alldcr1 = new List<listlatlong>();
                    var dr = g1.return_dr("listofuser " + ula.stateid + ",'" + ula.username + "','" + ula.asondate + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new listlatlong
                            {
                                username = Convert.ToString(dr["name"].ToString()),
                                userid = Convert.ToInt32(dr["userid"].ToString()),
                                stateid = Convert.ToInt32(dr["stateid"].ToString()),
                                position = GetDetailslatlongbyuser(Convert.ToInt32(dr["userid"].ToString()), ula.asondate),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new listlatlongs
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.Unicode);

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No single Lat/Long in this State"), Encoding.Unicode);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.Unicode);

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.Unicode);

                return response;
            }
        }

        private List<positionlatlong> GetDetailslatlongbyuser(int slno, DateTime asondt)
        {
            List<positionlatlong> alldcr11 = new List<positionlatlong>();
            try
            {
                DataConection g1 = new DataConection();
                var dr = g1.return_dr("listofuserbyid " + slno + ",'" + asondt + "'");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr11.Add(new positionlatlong
                        {
                            orderno = Convert.ToInt32(dr["slno"].ToString()),
                            lat = Convert.ToString(dr["lat"].ToString()),
                            lng = Convert.ToString(dr["long"].ToString()),
                            latlngtmdt = Convert.ToDateTime(dr["latlongtmdt"].ToString()),
                            timestamp = Convert.ToString(dr["timestamp"].ToString()),
                            duration = Convert.ToString(dr["duration"].ToString()),
                            place = Convert.ToString(dr["place"].ToString()),
                            address = Convert.ToString(dr["address"].ToString()),
                        });
                    }
                    g1.close_connection();
                }
                else
                {
                    alldcr11.Add(new positionlatlong
                    {
                        orderno = 0,
                        lat = string.Empty,
                        lng = string.Empty,
                        latlngtmdt = DateTime.Now,
                        duration = string.Empty,
                        place = string.Empty,
                        address = string.Empty,
                    });
                }
            }
            catch
            {
                alldcr11.Add(new positionlatlong
                {
                    orderno = 0,
                    lat = string.Empty,
                    lng = string.Empty,
                    latlngtmdt = DateTime.Now,
                    duration = string.Empty,
                    place = string.Empty,
                    address = string.Empty,
                });
            }

            return alldcr11;
        }
    }
}