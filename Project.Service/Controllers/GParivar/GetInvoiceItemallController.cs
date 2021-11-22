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
    public class GetInvoiceItemallController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getInvoiceItemall")]
        public HttpResponseMessage GetDetails(ListInvoiceItemall ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetInvoiceItemallDetails> alldcr = new List<GetInvoiceItemallDetails>();
                    List<GetInvoiceItemallDetail> alldcr1 = new List<GetInvoiceItemallDetail>();
                    var dr = g1.return_dr("dbo.invoicegetitemallapp '" + ula.branchid + "','" + ula.datefrom + "','" + ula.dateto + "','" + ula.Ishomebrnch + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetInvoiceItemallDetail
                            {
                                ponum = Convert.ToString(dr["ponum"].ToString()),
                                hsn = Convert.ToString(dr["hsn"].ToString()),
                                invoicetype = Convert.ToString(dr["invoicetype"].ToString()),
                                rangenm = Convert.ToString(dr["rangenm"].ToString()),
                                invoiceno = Convert.ToString(dr["invoiceno"].ToString()),
                                invoicedate = Convert.ToString(dr["invoicedate"].ToString()),
                                partyid = Convert.ToInt32(dr["partyid"].ToString()),
                                ItemID = Convert.ToInt32(dr["ItemID"].ToString()),
                                disper = Convert.ToDouble(dr["disper"].ToString()),
                                unitnm = Convert.ToString(dr["unitnm"].ToString()),
                                itemcode = Convert.ToString(dr["itemcode"].ToString()),
                                brnch = Convert.ToString(dr["brnch"].ToString()),
                                offerprice = Convert.ToDouble(dr["offerprice"].ToString()),
                                itemmrp = Convert.ToDouble(dr["itemmrp"].ToString()),
                                undercutting = Convert.ToDouble(dr["undercutting"].ToString()),
                                taxper = Convert.ToDouble(dr["taxper"].ToString()),
                                amount = Convert.ToDouble(dr["amount"].ToString()),
                                discount = Convert.ToDouble(dr["discount"].ToString()),
                                tax = Convert.ToDouble(dr["tax"].ToString()),
                                finalamount = Convert.ToDouble(dr["finalamount"].ToString()),
                                PCategory = Convert.ToString(dr["PCategory"].ToString()),
                                PartyName = Convert.ToString(dr["PartyName"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                taxper1 = Convert.ToDouble(dr["taxper1"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                warehousenm = Convert.ToString(dr["warehousenm"].ToString()),
                                agentnm = Convert.ToString(dr["agentnm"].ToString()),





                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetInvoiceItemallDetails
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