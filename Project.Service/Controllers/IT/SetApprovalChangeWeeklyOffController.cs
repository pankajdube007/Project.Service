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
    public class SetApprovalChangeWeeklyOffController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/setApprovalChangeWeeklyOff")]
        public HttpResponseMessage GetDetails(ListofSetApprovalChangeWeeklyOff ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.EmpID != 0)
            {
                try
                {
                    string data1;

                    List<AddSetApprovalChangeWeeklyOffLists> alldcr = new List<AddSetApprovalChangeWeeklyOffLists>();
                    List<AddSetApprovalChangeWeeklyOffList> alldcr1 = new List<AddSetApprovalChangeWeeklyOffList>();

                    var dr = g2.return_dr("SetApprovalChangeWeeklyOff '" + ula.EmpID + "','" + ula.Date + "','" + ula.ApprovedBy + "','" + ula.status + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddSetApprovalChangeWeeklyOffList
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddSetApprovalChangeWeeklyOffLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "Change Weekly Day Off Not Updated!!!!!!!!"), Encoding.UTF8, "application/json");

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