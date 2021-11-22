using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class luckydrawwinnerlistController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getluckydrawwinnerlist")]
        public HttpResponseMessage GetDetails(Listluckydrawwinnerlist ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<luckydrawwinnerlistDetails> alldcr = new List<luckydrawwinnerlistDetails>();
                    List<luckydrawwinnerlistDetail> alldcr1 = new List<luckydrawwinnerlistDetail>();
                    var dr = g2.return_dr("luckydrawwinnerlist '" + ula.CIN + "'");
                    if (dr.HasRows) 
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new luckydrawwinnerlistDetail
                            {
                                Name = Convert.ToString(dr["name"]),
                                ContactPerson= Convert.ToString(dr["contactnm"]),
                                State= Convert.ToString(dr["statenm"]),
                                Gift = Convert.ToString(dr["Price"]),
                                Date = Convert.ToString(dr["giftdate"]),
                                Image = string.IsNullOrEmpty(dr["img"].ToString().Trim(',')) ? "" : (baseurl + "luckydrawimage/" + dr["img"].ToString().Trim(','))
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new luckydrawwinnerlistDetails
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
                        g2.close_connection();
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