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
    public class DispatchMaterialController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDispatchedMaterial")]
        public HttpResponseMessage GetDetails(ListsofDispatchMaterialAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DispatchMaterials> alldcr = new List<DispatchMaterials>();
                    List<DispatchMaterial> alldcr1 = new List<DispatchMaterial>();
                    List<DispatchMaterialFinal> alldcr2 = new List<DispatchMaterialFinal>();
                    var dr = g1.return_dt("App_InvoiceDetails '" + ula.CIN + "','" + ula.FromDate + "','" + ula.ToDate + "'," + ula.Index + "," + ula.Count + ",'" + ula.SearchText + "'");

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
                            alldcr1.Add(new DispatchMaterial
                            {
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"].ToString()),
                                Division = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                Amount = Convert.ToString(dr.Rows[i]["finalamount"].ToString()),
                                LrNo = Convert.ToString(dr.Rows[i]["lrno"].ToString()),
                                TransporterName = Convert.ToString(dr.Rows[i]["transpotername"].ToString()),
                                url = WebConfigurationManager.AppSettings["ErpUrl"] + "Report-TaxInvoice.aspx?type=transporter&id=" + Convert.ToString(dr.Rows[i]["SlNo"].ToString()) + "&uniquekey=" + Convert.ToString(dr.Rows[i]["uniquekey"].ToString() + "&viewtype=App"),
                            });
                        }

                        alldcr2.Add(new DispatchMaterialFinal
                        {
                            dispatchdata = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new DispatchMaterials
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