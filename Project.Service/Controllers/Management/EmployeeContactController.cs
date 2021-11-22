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
    public class EmployeeContactController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getEmployeeContact")]
        public HttpResponseMessage GetDetails(ListsofEmployeeContact ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ClientSecret != string.Empty)
            {
                try
                {
                    string data1;

                    List<EmployeeContacts> alldcr = new List<EmployeeContacts>();
                    List<EmployeeContact> alldcr1 = new List<EmployeeContact>();
                    var dr = g1.return_dt("AppHelpDeskEmployeeContact");

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new EmployeeContact
                            {
                                workdescription = dr.Rows[i]["WorkDesc"].ToString(),
                                contactperson = dr.Rows[i]["ContactPerson"].ToString(),
                                mobile = dr.Rows[i]["contactno"].ToString(),
                                landline = dr.Rows[i]["contactnocur"].ToString(),
                                email = dr.Rows[i]["email"].ToString(),
                                department = dr.Rows[i]["Department"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new EmployeeContacts
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