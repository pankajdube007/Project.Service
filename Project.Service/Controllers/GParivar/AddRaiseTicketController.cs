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
    public class AddRaiseTicketController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addRaiseTicketDetails")]
        public HttpResponseMessage GetDetails(ListofAddRaiseTicket ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<AddRaiseTicketDetails> alldcr = new List<AddRaiseTicketDetails>();
                    List<AddRaiseTicketDetail> alldcr1 = new List<AddRaiseTicketDetail>();

                    int IsPincodeAvailable = 0;

                    if (ula.Pincode != "")
                    {
                        IsPincodeAvailable = 1;
                    }

                    // var dr = g2.return_dr("crm.AddTicketByGParivaarUser '" + 0 + "','" + 0 + "','" + "" + "','" + DateTime.Now + "','" + 1 + "','" + 8 + "','" + ula.CustName + "','" + ula.CustContactNo + "','" + ula.ContactPersonContactNo + "','" + ula.EmailID + "','" + ula.CustAddress + "','" + ula.Address2 + "','" + ula.Address3 + "','" + ula.Pincode + "','" + ula.PincodeID + "','" + ula.StateID + "','" + ula.DistrictID + "','" + ula.City + "','" + ula.PersonCallingName + "','" + ula.ContactPersonName + "','" + 2 + "','" + "" + "','" + "" + "','" + 2 + "','" + ula.ItemQRCode + "','" + 0 + "','" + 0 + "','" + 0 + "','" + ula.ProductDescription + "','" + ula.PurchaseDt + "','" + "01/01/2021" + "','" + IsPincodeAvailable + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + ula.ProductIssueDesc + "','" + "" + "','" + 1 + "','" + 3 + "','" + 6 + "','" + ula.CustomerID + "','" + 0 + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 1 + "','" + 1 + "','" + ula.Custuniquekey + "','" + new Guid().ToString() + "','" + new Guid().ToString() + "','" + 1 + "','" + 1 + "'");

                    var result = g2.reterive_val("crm.AddTicketByGParivaarUser '" + ula.CIN + "','" + 0 + "','" + 0 + "','" + "" + "','" + DateTime.Now + "','" + 1 + "','" + 8 + "','" + ula.CustName + "','" + ula.CustContactNo + "','" + ula.ContactPersonContactNo + "','" + ula.EmailID + "','" + ula.CustAddress + "','" + ula.Address2 + "','" + ula.Address3 + "','" + ula.Pincode + "','" + ula.StateID + "','" + ula.DistrictID + "','" + ula.City + "','" + ula.PersonCallingName + "','" + ula.ContactPersonName + "','" + 2 + "','" + "" + "','" + "" + "','" + 2 + "','" + ula.ItemQRCode + "','" + 0 + "','" + 0 + "','" + 0 + "','" + ula.ProductDescription + "','" + ula.PurchaseDt + "','" + "01/01/2021" + "','" + IsPincodeAvailable + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 1 + "','" + 1 + "','" + "" + "','" + ula.ProductIssueDesc + "','" + "" + "','" + 1 + "','" + 3 + "','" + 6 + "','" + ula.CustomerID + "','" + 0 + "','" + 1 + "','" + 0 + "','" + 0 + "','" + 1 + "','" + 1 + "','" + ula.Custuniquekey + "','" + new Guid().ToString() + "','" + new Guid().ToString() + "','" + 1 + "','" + 1 + "'");

                    string[] resultstring = result.Split('#');

                    g2.close_connection();

                    switch (resultstring[0])
                    {
                        case "1":
                            alldcr1.Add(new AddRaiseTicketDetail
                            {
                                output = ($"Ticket {resultstring[1]} added successfully.Save this ticket number for future reference!")
                            });


                            break;

                        case "2":
                            alldcr1.Add(new AddRaiseTicketDetail
                            {
                                output = "Contact number already exists !"
                            });
                            break;

                        case "3":
                            alldcr1.Add(new AddRaiseTicketDetail
                            {
                                output = "Email ID already exists !"
                            });
                            break;

                        case "4":
                            alldcr1.Add(new AddRaiseTicketDetail
                            {
                                output = "Open ticket on this Item QRCode already exists !"
                            });
                            break;

                        case "8":
                            alldcr1.Add(new AddRaiseTicketDetail
                            {
                                output = "Customer not found !"
                            });
                            break;

                        default:

                            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
                            response1.Content = new StringContent(cm.StatusTime(false, "Enquiry Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response1;

                    }


                    //alldcr1.Add(new AddRaiseTicketDetail
                    //{
                    //    output = "Data Sucessfully inserted"
                    //});

                    g2.close_connection();
                    alldcr.Add(new AddRaiseTicketDetails
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
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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