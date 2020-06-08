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
    public class UserlatlongsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AllUserLatLong")]
        public HttpResponseMessage GetAllUserLatLong(UserlatlongAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;
                    List<ulatlongs> alldcr = new List<ulatlongs>();
                    List<ulatlong> alldcr1 = new List<ulatlong>();
                    var dr = g1.return_dr("latlongselectall " + ula.stateid + string.Empty);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ulatlong
                            {
                                //slno = Convert.ToInt32(dr["slno"].ToString()),
                                lat = Convert.ToString(dr["lat"].ToString()),
                                longi = Convert.ToString(dr["long"].ToString()),
                                latlongtmdt = Convert.ToDateTime(dr["latlongtmdt"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ulatlongs
                        {
                            result = "True",
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
                        response.Content = new StringContent(cm.StatusTime(false, "No single Lat/Long in this State"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}