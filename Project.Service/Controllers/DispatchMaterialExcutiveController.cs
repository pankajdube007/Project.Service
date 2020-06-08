using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class DispatchMaterialExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDispatchedMaterialExcutive")]
        public HttpResponseMessage GetDetails(ListsofDispatchMaterialEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<DispatchMaterialExs> alldcr = new List<DispatchMaterialExs>();
                    List<DispatchMaterialEx> alldcr1 = new List<DispatchMaterialEx>();
                    List<DispatchMaterialExFinal> alldcr2 = new List<DispatchMaterialExFinal>();
                    var dr = g1.return_dt("AppDispatchMaterialExcutive " + ula.ExId + ",'" + ula.CIN + "','" + ula.FromDate + "','" + ula.ToDate + "'," + ula.Index + "," + ula.Count + ",'" + ula.searchtxt + "'," + Convert.ToBoolean(ula.Hierarchy));

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.Index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new DispatchMaterialEx
                            {
                                PartyName = Convert.ToString(dr.Rows[i]["displaynm"]),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"]),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"]),
                                Division = Convert.ToString(dr.Rows[i]["divisionnm"]),
                                Amount = Convert.ToString(dr.Rows[i]["finalamount"]),
                                LrNo = Convert.ToString(dr.Rows[i]["lrno"]),
                                TransporterName = Convert.ToString(dr.Rows[i]["transpotername"]),
                                url = WebConfigurationManager.AppSettings["ErpUrl"] + "Report-TaxInvoice.aspx?type=transporter&id=" + Convert.ToString(dr.Rows[i]["SlNo"].ToString()) + "&uniquekey=" + Convert.ToString(dr.Rows[i]["uniquekey"].ToString() + "&viewtype=App"),
                            });
                        }

                        alldcr2.Add(new DispatchMaterialExFinal
                        {
                            dispatchdata = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new DispatchMaterialExs
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr2,
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