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
    public class GetpeechedekholightappController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getpeechedekholightapp")]
        public HttpResponseMessage GetDetails(peechedekholightapp ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<Getpeechedekholightapp> alldcr = new List<Getpeechedekholightapp>();
                    List<Getpeechedekholightapp1> alldcr1 = new List<Getpeechedekholightapp1>();
                    var dr = g1.return_dr("dbo.execpeechedekholightapp '" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new Getpeechedekholightapp1
                            {

                                displaynm = Convert.ToString(dr["displaynm"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                salesexname = Convert.ToString(dr["salesexname"].ToString()),
                                runningtarget = Convert.ToString(dr["runningtarget"].ToString()),
                                nexttarget = Convert.ToString(dr["nexttarget"].ToString()),
                                OctNormal = Convert.ToString(dr["OctNormal"].ToString()),
                                OctBonus = Convert.ToString(dr["OctBonus"].ToString()),
                                NovNormal = Convert.ToString(dr["NovNormal"].ToString()),
                                NovBonus = Convert.ToString(dr["NovBonus"].ToString()),
                                DecNormal = Convert.ToString(dr["DecNormal"].ToString()),
                                DecBonus = Convert.ToString(dr["DecBonus"].ToString()),
                                Bonusq2 = Convert.ToString(dr["Bonusq2"].ToString()),
                                Total = Convert.ToString(dr["TotalBonus"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Getpeechedekholightapp
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
        