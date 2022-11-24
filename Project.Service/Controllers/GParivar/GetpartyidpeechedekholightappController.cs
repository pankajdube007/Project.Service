using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;



namespace Project.Service.Controllers.GParivar
{
    public class GetpartyidpeechedekholightappController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpartyidpeechedekholightapp")]
        public HttpResponseMessage GetAllUserdetails(partyidpeechedekholightapp ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<Getpartyidpeechedekholightapp> alldcr = new List<Getpartyidpeechedekholightapp>();
                    List<Getpartyidpeechedekholightapp1> alldcr1 = new List<Getpartyidpeechedekholightapp1>();
                    var dr = g1.return_dr("partyidpeechedekholightapp '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Getpartyidpeechedekholightapp1
                            {
                                displaynm = Convert.ToString(dr["displaynm"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
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
                        alldcr.Add(new Getpartyidpeechedekholightapp
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}




