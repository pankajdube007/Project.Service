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
    public class RaiseTicketContactDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getRaiseTicketContactDetails")]
        public HttpResponseMessage GetDetails(ListofRaiseTicketContactDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetRaiseTicketContactDetailLists> alldcr = new List<GetRaiseTicketContactDetailLists>();
                    List<GetRaiseTicketContactDetailList> alldcr1 = new List<GetRaiseTicketContactDetailList>();
                    var dr = g1.return_dr("crm.GetCustomerDetailsFromContactNoForCustTicket  '" + ula.ContactNo + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetRaiseTicketContactDetailList
                            {
                                Name = Convert.ToString(dr["Name"].ToString()),
                                Address = Convert.ToString(dr["Address"].ToString()),
                                Address2 = Convert.ToString(dr["Address2"].ToString()),
                                Address3 = Convert.ToString(dr["Address3"].ToString()),
                                ContactNo = Convert.ToString(dr["ContactNo"].ToString()),
                                EmailID = Convert.ToString(dr["EmailID"].ToString()),
                                PincodeID = Convert.ToString(dr["PincodeID"].ToString()),
                                Pincode = Convert.ToString(dr["Pincode"].ToString()),
                                StateID = Convert.ToString(dr["StateID"].ToString()),
                                DistrictID = Convert.ToString(dr["DistrictID"].ToString()),
                                City = Convert.ToString(dr["City"].ToString()),
                                CustomerID = Convert.ToString(dr["CustomerID"].ToString()),
                                CustUniquekey = Convert.ToString(dr["CustUniquekey"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                Distrctnm = Convert.ToString(dr["Distrctnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetRaiseTicketContactDetailLists
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