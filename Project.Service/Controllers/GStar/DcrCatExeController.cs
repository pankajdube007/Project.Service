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
    public class DcrCatExeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetDcrCatandAreaList")]
        public HttpResponseMessage GetDetails(DcrCatExe ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<DcrCatExeLists> alldcr = new List<DcrCatExeLists>();
                    List<DcrCatExeList> alldcr1 = new List<DcrCatExeList>();
                    List<DcrCatExeFinalList> alldcrfinal = new List<DcrCatExeFinalList>();
                    List<AreaListOrganationList> alldcrarea = new List<AreaListOrganationList>();
                    var dr = g1.return_dr("apppartycatselect");
                    var dr1 = g1.return_dr("AppSelectArea '"+ula.ExId+"','"+ula.EmpType+ "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DcrCatExeList
                            {
                                catid = Convert.ToInt32(dr["slno"].ToString()),
                                partycatnm = Convert.ToString(dr["partycatnm"].ToString()),
                            });
                        }
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                alldcrarea.Add(new AreaListOrganationList
                                {
                                    areaid = Convert.ToInt32(dr1["areaid"].ToString()),
                                    areanm = Convert.ToString(dr1["areanm"].ToString()),
                                });
                            }
                        }

                        alldcrfinal.Add(new DcrCatExeFinalList
                        {
                            catdata = alldcr1,
                            areadata = alldcrarea,
                        });

                        g1.close_connection();
                        alldcr.Add(new DcrCatExeLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcrfinal,
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Division available"), Encoding.UTF8, "application/json");

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