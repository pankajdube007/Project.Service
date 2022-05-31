using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System.Text;
using System.Net.Http;
using Project.Service.Models.IT;

namespace Project.Service.Controllers.IT
{
    public class AddChangeWeeklyOffController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddChangeWeeklyOff")]
        public HttpResponseMessage GetDetails(ListofChangeWeeklyOff ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.EmpID != 0)
            {
                try
                {
                    string data1;

                    List<AddChangeWeeklyOffLists> alldcr = new List<AddChangeWeeklyOffLists>();
                    List<AddChangeWeeklyOffList> alldcr1 = new List<AddChangeWeeklyOffList>();

                    var dr = g2.return_dr("InsertChangeWeeklyOff '" + ula.EmpID + "','" + ula.Date + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddChangeWeeklyOffList
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddChangeWeeklyOffLists
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Add Change Weekly Day Off Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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