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
    public class ListsofSubjectController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetSubjectList")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<Subjects> alldcr = new List<Subjects>();
                List<Subject> alldcr1 = new List<Subject>();
                var dr = g1.return_dr("App_Subject");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new Subject
                        {
                            slno = Convert.ToInt32(dr["SlNo"].ToString()),
                            Subjectnm = Convert.ToString(dr["SubjectName"].ToString()),
                        });
                    }
                    g1.close_connection();
                    alldcr.Add(new Subjects
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
                    response.Content = new StringContent(cm.StatusTime(true, "No Subject available"), Encoding.UTF8, "application/json");

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
    }
}