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
    public class GetpartyidpeechedekhoappController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpartyidpeechedekhoapp")]
        public HttpResponseMessage GetAllUserdetails(partyidpeechedekhoapp ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<Getpartyidpeechedekhoapp> alldcr = new List<Getpartyidpeechedekhoapp>();
                    List<Getpartyidpeechedekhoapp1> alldcr1 = new List<Getpartyidpeechedekhoapp1>();
                    var dr = g1.return_dr("partyidpeechedekhoapp '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Getpartyidpeechedekhoapp1
                            {
                                displaynm = Convert.ToString(dr["displaynm"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                target1 = Convert.ToString(dr["MainTarget60"].ToString()),
                                target2 = Convert.ToString(dr["MainTarget70"].ToString()),
                                target3 = Convert.ToString(dr["MainTarget80"].ToString()),
                                runningtarget = Convert.ToString(dr["runningtarget"].ToString()),
                                nexttarget = Convert.ToString(dr["nexttarget"].ToString()),
                                octto15novNormal = Convert.ToString(dr["octto15novNormal"].ToString()),
                                octto15novBonus = Convert.ToString(dr["octto15novBonus"].ToString()),
                                oct16todec31Normal = Convert.ToString(dr["oct16todec31Normal"].ToString()),
                                oct16todec31Bonus = Convert.ToString(dr["oct16todec31Bonus"].ToString()),
                                q2Bonus = Convert.ToString(dr["q2Bonus"].ToString()),

                                Total = Convert.ToString(dr["TotalBonus"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Getpartyidpeechedekhoapp
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
