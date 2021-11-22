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
    public class ExecCheckInOutListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecCheckInCheckOutList")]
        public HttpResponseMessage GetDetails(ListExecCheckInOutList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecCheckInOutLists> alldcr = new List<ExecCheckInOutLists>();
                    List<ExecCheckInOutList> alldcr1 = new List<ExecCheckInOutList>();

                    var dr = g1.return_dr("appcheckincheckoutList " + ula.ExId + ",'" + ula.date + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecCheckInOutList
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                OrgId = Convert.ToString(dr["orgid"].ToString()),
                                OrgCatId = Convert.ToString(dr["orgcatid"].ToString()),
                                OrgName = Convert.ToString(dr["orgname"].ToString()),
                                OrgCate = Convert.ToString(dr["orgcat"].ToString()),
                                Distnce = Convert.ToDecimal(dr["distance"].ToString()),
                                CheckInTime = Convert.ToString(dr["checkintime"].ToString()),
                                CheckOutTime = Convert.ToString(dr["checkouttime"].ToString()),
                                Dcr = Convert.ToString(dr["dcrmrk"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecCheckInOutLists
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