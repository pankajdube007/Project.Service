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
    public class ExecGCSSalePastController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getExecGCSSchemeWisesalePast")]
        public HttpResponseMessage GetDetails(ListexecutiveGscSalePast ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<executiveGscSalePast> alldcr = new List<executiveGscSalePast>();
                    List<executiveGscSalesPast> alldcr1 = new List<executiveGscSalesPast>();
                    var dr = g1.return_dr("GSCPartywisesalePast '" + ula.ExId + "','" + ula.Schemeid + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new executiveGscSalesPast
                            {

                                partyname = Convert.ToString(dr["partyname"].ToString()),
                                itemname = Convert.ToString(dr["itemname"].ToString()),
                                sale = Convert.ToString(dr["sale"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new executiveGscSalePast
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