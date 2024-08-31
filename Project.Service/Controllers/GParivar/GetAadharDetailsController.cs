using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class GetAadharDetailsController : ApiController
    {
        private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/GetAadharDetailsForLedgerConfirmation")]
        public HttpResponseMessage GetDetails(GetListAadhar ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<GetListAadharDetails> alldcr = new List<GetListAadharDetails>();
                    List<GetListAadharDetail> alldcr1 = new List<GetListAadharDetail>();
                   
                    var dr = g2.return_dr("dbo.GetCinDetailsForLedgerConfirmation '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListAadharDetail
                            {
                                FullName = Convert.ToString(dr["FullName"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                Pancard = Convert.ToString(dr["Pancard"].ToString()),
                                AadharNo = Convert.ToString(dr["AadharNo"].ToString()),
                                IsUpdated = Convert.ToString(dr["IsUpdated"].ToString()),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new GetListAadharDetails
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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