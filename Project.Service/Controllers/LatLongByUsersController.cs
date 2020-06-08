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
    public class LatLongByUsersController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AllLatLongByUser")]
        public HttpResponseMessage LatLongByUser(LatLongByUserAction llu)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(llu.uniquekey))
            {
                try
                {
                    string data1;
                    List<latlongbyus> alldcr = new List<latlongbyus>();
                    List<latlongbyu> alldcr1 = new List<latlongbyu>();
                    var dr = g1.return_dr("latlongselect " + llu.userid + ",'" + llu.fromdt + "','" + llu.todt + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new latlongbyu
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                lat = Convert.ToString(dr["lat"].ToString()),
                                longi = Convert.ToString(dr["long"].ToString()),
                                latlongtmdt = Convert.ToString(dr["latlongtmdt"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new latlongbyus
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
                        response.Content = new StringContent(cm.StatusTime(false, "No single Lat/Long in this User"), Encoding.Unicode);

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
    }
}