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
    public class ManagementOutstandingbyDaysController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetOutstandingbyDays")]
        public HttpResponseMessage GetDetails(ListofManagementOutstandingbyDays ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ManagementOutstandingbyDayss> alldcr = new List<ManagementOutstandingbyDayss>();
                    List<ManagementOutstandingbyDays> alldcr1 = new List<ManagementOutstandingbyDays>();

                    var dr = g1.return_dr("allbarnchoutstandingdatabydays " + ula.Days+",'"+ula.CIN+ "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementOutstandingbyDays
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                category = Convert.ToString(dr["PCategory"].ToString()),
                                partynm = Convert.ToString(dr["PartyName"].ToString()),
                                partystatus = Convert.ToString(dr["workstatus"].ToString()),
                                locnm = Convert.ToString(dr["locnm"].ToString()),
                                city = Convert.ToString(dr["city"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                commercial = Convert.ToString(dr["commercial"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                totaloutstanding = Convert.ToString(dr["totaloutstanding"].ToString()),
                                countinvoice = Convert.ToString(dr["countinvoice"].ToString()),
                                historydays = Convert.ToString(dr["hiestdays"].ToString()),
                                totalbalance = Convert.ToString(dr["totlbalance"].ToString()),
                                lastinvoicedate = Convert.ToString(dr["lastinvoicedate1"].ToString()),
                                lastpaymentdate = Convert.ToString(dr["lastpaymentdate1"].ToString()),
                                lastpaymentamt = Convert.ToString(dr["lastpaymentamt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementOutstandingbyDayss
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