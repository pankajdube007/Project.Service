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
    public class ExecFanComboListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getexecfancombolist")]
        public HttpResponseMessage GetAllUserdetails(ListofExecFanCombo ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExecId !=0)
            {
                try
                {
                    string data1;
                    List<ExecFanComboLists> alldcr = new List<ExecFanComboLists>();
                    List<ExecFanComboList> alldcr1 = new List<ExecFanComboList>();
                    var dr = g1.return_dr("execfancombolist '" + ula.ExecId + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecFanComboList
                            {
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                noofcombo = Convert.ToString(dr["noofcombo"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                displaynmwitharea = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                salesexname = Convert.ToString(dr["salesexname"].ToString()),
                                partycontactno = Convert.ToString(dr["partycontactno"].ToString()),
                                execcontactno = Convert.ToString(dr["execcontactno"].ToString()),
                                bookingdate = Convert.ToString(dr["bookingdate"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecFanComboLists
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