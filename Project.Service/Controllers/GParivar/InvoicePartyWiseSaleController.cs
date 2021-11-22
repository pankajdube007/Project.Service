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
    public class InvoicePartyWiseSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getinvoicepartywisesale")]
        public HttpResponseMessage GetDetails(ListInvoicePartyWiseSale ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<InvoicePartyWiseSaleLists> alldcr = new List<InvoicePartyWiseSaleLists>();
                    List<InvoicePartyWiseSaleList> alldcr1 = new List<InvoicePartyWiseSaleList>();

                    var dr = g1.return_dr("getInvoicePartyWise_Sale '" + ula.FromDate + "','" + ula.Todate + "','" + ula.PartyId + "','" + ula.TypeCat + "','" + ula.DivId + "','" + ula.BranchId + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new InvoicePartyWiseSaleList
                            {

                                PartyName = Convert.ToString(dr["PartyName"].ToString()),
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                TypeCate = Convert.ToString(dr["typeofparty"].ToString()),
                                BranchId = Convert.ToString(dr["homebranchid"].ToString()),
                                MonthName = Convert.ToString(dr["MonthName"].ToString()),
                                InvoiceNo = Convert.ToString(dr["invoiceno"].ToString()),
                                outstandingamt = Convert.ToString(dr["outstandingamt"].ToString()),
                                finalamount = Convert.ToString(dr["finalamount"].ToString()),






                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new InvoicePartyWiseSaleLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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