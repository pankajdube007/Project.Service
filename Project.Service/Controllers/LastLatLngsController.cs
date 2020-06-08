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
    public class LastLatLngsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/LastLatLong")]
        public HttpResponseMessage GetAllUserLatLong(LastLatLngAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;
                    List<lastlatlongs> alldcr = new List<lastlatlongs>();
                    List<lastlatlong> alldcr1 = new List<lastlatlong>();
                    var dr = g1.return_dr("lastlatlong " + ula.stateid + ",'" + ula.asondate + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new lastlatlong
                            {
                                //slno = Convert.ToInt32(dr["slno"].ToString()),
                                lat = Convert.ToString(dr["lat"].ToString()),
                                lng = Convert.ToString(dr["long"].ToString()),
                                latlngtmdt = Convert.ToDateTime(dr["latlongtmdt"].ToString()),
                                username = Convert.ToString(dr["name"].ToString()),
                                userid = Convert.ToInt32(dr["userid"].ToString()),
                                times = Convert.ToString(dr["times"].ToString()),
                                address = Convert.ToString(dr["address"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new lastlatlongs
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
    }
}