using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class GetRaiseTicketDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getRaiseTicketDetails")]
        public HttpResponseMessage GetDetails(ListofRaiseTicketDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetRaiseTicketDetailLists> alldcr = new List<GetRaiseTicketDetailLists>();
                    List<GetRaiseTicketDetailList> alldcr1 = new List<GetRaiseTicketDetailList>();
                    var dr = g1.return_dr("crm.GetRaiseTicketByGParivaarUser  '" + ula.CIN + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.Search + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetRaiseTicketDetailList
                            {
                                TktNo = Convert.ToString(dr["TktNo"].ToString()),
                                TktStatus = Convert.ToString(dr["TktStatus"].ToString()),
                                Tktdt = Convert.ToString(dr["Tktdt"].ToString()),
                                CustContactNo = Convert.ToString(dr["CustContactNo"].ToString()),
                                FullAddress = Convert.ToString(dr["FullAddress"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetRaiseTicketDetailLists
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