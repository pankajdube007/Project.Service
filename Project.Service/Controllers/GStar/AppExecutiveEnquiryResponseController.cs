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
    public class AppExecutiveEnquiryResponseController : ApiController
    {


        [HttpPost]
        [ValidateModel]
        [Route("api/getappExcutiveEnquiryResponse")]
        public HttpResponseMessage GetDetails(AppExecutiveEnquiryResponse ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data;
                    List<ExecutiveEnquiryResponse> alldcr = new List<ExecutiveEnquiryResponse>();
                                      var dr = g2.return_dr("execenquiryresponse " + ula.ExId + ",'" + ula.SlNo + "','" + ula.Status + "','" + ula.Response + "'");
                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        alldcr.Add(new ExecutiveEnquiryResponse
                        {
                            result = "True",
                            message = "Updated Successfully",
                            servertime = DateTime.Now.ToString(),
                            data = string.Empty,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

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