using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Management;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class partywisecomboidController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getpartywisecomboid")]
        public HttpResponseMessage GetDetails(ListOfpartywisecomboid ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<partywisecomboids> alldcr = new List<partywisecomboids>();
                    List<partywisecomboid> alldcr1 = new List<partywisecomboid>();

                    var dr = g1.return_dr("partywisecomboidcount '" + ula.partycin + "','" + ula.CIN + "','" + ula.Category + "',"+ula.ComboId);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new partywisecomboid
                            {
                                ComboName = Convert.ToString(dr["ComboName"].ToString()),
                                NumberOfCombo = Convert.ToString(dr["NumberOfCombo"].ToString()),
                                used = Convert.ToString(dr["used"].ToString()),
                                ComboIds = Convert.ToString(dr["Comboid"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new partywisecomboids
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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
       