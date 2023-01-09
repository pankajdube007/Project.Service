using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class ExecWiseVendorReqListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecWiseVendorReqList")]
        public HttpResponseMessage GetDetails(ExecWiseVendorReqList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecWiseVendorReqLists> alldcr = new List<ExecWiseVendorReqLists>();
                    List<ExecWiseVendorReqLists1> alldcr1 = new List<ExecWiseVendorReqLists1>();
                    var dr = g1.return_dr("ExecWiseVendorReqList '" + ula.ExId + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecWiseVendorReqLists1
                            {

                                ReferenceNo = Convert.ToString(dr["ReferenceNo"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                inspectiondate = Convert.ToString(dr["inspectiondate"].ToString()),
                                status = Convert.ToString(dr["status"].ToString()),
                                vaddress = Convert.ToString(dr["vaddress"].ToString()),
                                vcontact = Convert.ToString(dr["vcontact"].ToString()),
                                

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecWiseVendorReqLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1
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