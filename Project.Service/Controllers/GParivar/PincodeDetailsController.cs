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
    public class PincodeDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPincodeDetails")]
        public HttpResponseMessage GetAllUserdetails(ListofPincodeDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<PincodeDetailsLists> alldcr = new List<PincodeDetailsLists>();
                    List<PincodeDetailsList> alldcr1 = new List<PincodeDetailsList>();
                    var dr = g1.return_dr("GetPincodeDetailsList '" + ula.Pincode + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PincodeDetailsList
                            {
                                StateID = Convert.ToString(dr["StateID"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                DistrictID = Convert.ToString(dr["DistrictID"].ToString()),
                                Distrctnm = Convert.ToString(dr["Distrctnm"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PincodeDetailsLists
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