using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class ExecWiseVendorReqitemListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExecWiseVendorReqitemList")]
        public HttpResponseMessage GetDetails(ExecWiseVendorReqitemList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecWiseVendorReqitemLists> alldcr = new List<ExecWiseVendorReqitemLists>();
                    List<ExecWiseVendorReqitemList1> alldcr1 = new List<ExecWiseVendorReqitemList1>();
                    var dr = g1.return_dr("ExecWiseVendorReqitemList '" + ula.slno + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecWiseVendorReqitemList1
                            {

                                ProductCode = dr["ProductCode"].ToString(),
                                slno = dr["slno"].ToString(),
                                RefID = dr["RefID"].ToString(),
                                status = dr["status"].ToString(),
                                inspectiondate = dr["inspectiondate"].ToString(),
                                files = dr["files"].ToString(),
                                remark = dr["remark"].ToString(),
                                isedit = dr["isedit"].ToString()



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecWiseVendorReqitemLists
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