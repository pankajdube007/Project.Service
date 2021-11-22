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
    public class AllAllOpenTicketsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getallopenticketslist")]
        public HttpResponseMessage GetAllUserdetails(ListofAllOpenTickets ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<AllOpenTicketsLists> alldcr = new List<AllOpenTicketsLists>();
                    List<AllOpenTicketsList> alldcr1 = new List<AllOpenTicketsList>();
                    var dr = g1.return_dr("[crm].[GetAllOpenTicketsAPI] '" + ula.CIN + "','" + ula.Category + "','" + ula.Type + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new AllOpenTicketsList
                            {
                                Number = Convert.ToString(dr["Number"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                Tktno = Convert.ToString(dr["Tktno"].ToString()),
                                Tktdt = Convert.ToString(dr["Tktdt"].ToString()),
                                CustName = Convert.ToString(dr["CustName"].ToString()),
                                ProductDivision = Convert.ToString(dr["ProductDivision"].ToString()),
                                ProductName = Convert.ToString(dr["ProductName"].ToString()),
                                uniquekey = Convert.ToString(dr["uniquekey"].ToString()),
                                TktPriority = Convert.ToString(dr["TktPriority"].ToString()),
                                CustContactNo = Convert.ToString(dr["CustContactNo"].ToString()),
                                ProductIssues = Convert.ToString(dr["ProductIssues"].ToString()),
                                AppointmentDate = Convert.ToString(dr["AppointmentDate"].ToString()),
                                TimeSlot = Convert.ToString(dr["TimeSlot"].ToString()),
                                TktStatus = Convert.ToString(dr["TktStatus"].ToString()),
                                ScName = Convert.ToString(dr["ScName"].ToString()),
                                AssignedToName = Convert.ToString(dr["AssignedToName"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AllOpenTicketsLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}