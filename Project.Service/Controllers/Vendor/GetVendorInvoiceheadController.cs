using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class GetVendorInvoiceheadController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorinvoiceHead")]
        public HttpResponseMessage GetDetails(ListofVendorInvoiceHead ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetVendorInvoiceHeadDetails> alldcr = new List<GetVendorInvoiceHeadDetails>();
                    List<GetVendorInvoiceHeadDetail> alldcr1 = new List<GetVendorInvoiceHeadDetail>();
                    var dr = g1.return_dr("dbo.x '" + ula.datefrom + "','" + ula.dateto + "','" + ula.PartyID + "','" + ula.TypeCat + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetVendorInvoiceHeadDetail
                            {
                                SLNo = Convert.ToString(dr["SLNo"].ToString()),
                                invoiceno = Convert.ToString(dr["invoiceno"].ToString()),
                                invoicedate = Convert.ToString(dr["invoicedate"].ToString()),
                                itemamount = Convert.ToDouble(dr["itemamount"].ToString()),
                                taxamount = Convert.ToDouble(dr["taxamount"].ToString()),
                                totalamount = Convert.ToDouble(dr["totalamount"].ToString()),
                                finalamount = Convert.ToDouble(dr["finalamount"].ToString()),
                                roundoff = Convert.ToDouble(dr["roundoff"].ToString()),
                                EWayBillno = Convert.ToString(dr["EWayBillno"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                taxamount2 = Convert.ToDouble(dr["taxamount2"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetVendorInvoiceHeadDetails
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