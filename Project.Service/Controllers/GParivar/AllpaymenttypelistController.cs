using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class AllpaymenttypelistController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getallpaymenttypedetailslist")]
        public HttpResponseMessage GetDetails(ListGetallpaymenttypelist ula)
        {
            string url1 = string.Empty;
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<Getallpaymenttypelists> alldcr = new List<Getallpaymenttypelists>();
                    List<Getallpaymenttypelist> alldcr1 = new List<Getallpaymenttypelist>();
                    List<GetallpaymenttypelistFinal> alldcr2 = new List<GetallpaymenttypelistFinal>();
                    //    var dr = g1.return_dr("apppartyalltypeamountdetailslist '" + ula.CIN + "','" + ula.sdate + "','" + ula.edate + "'");
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            alldcr1.Add(new Getallpaymenttypelist
                    //            {
                    //                invoiceno = Convert.ToString(dr["invoiceno"].ToString()),
                    //                date = Convert.ToString(dr["date"].ToString()),
                    //                amount = Convert.ToDecimal(dr["amount"].ToString()),
                    //                doctype = Convert.ToString(dr["doctype"].ToString()),
                    //                url = WebConfigurationManager.AppSettings["ErpUrl"] + "Report-TaxInvoice.aspx?type=transporter&id=" + Convert.ToString(dr.Rows[i]["SlNo"].ToString()) + "&uniquekey=" + Convert.ToString(dr.Rows[i]["uniquekey"].ToString() + "&viewtype=App"),
                    //            });
                    //        }
                    //        g1.close_connection();
                    //        alldcr.Add(new Getallpaymenttypelists
                    //        {
                    //            result = true,
                    //            message = string.Empty,
                    //            servertime = DateTime.Now.ToString(),
                    //            data = alldcr1,
                    //        });
                    //        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    //        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    //        return response;
                    //    }
                    //    else
                    //    {
                    //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

                    //        return response;
                    //    }
                    //}
                    DataTable dr = new DataTable();
                    if(ula.ExecId==0)
                    {

                         dr = g1.return_dt("apppartyalltypeamountdetailslist '" + ula.CIN + "','" + ula.sdate + "','" + ula.edate + "'," + ula.Index + "," + ula.Count + ",'"+ula.Type+"'");
                    }
                    else
                    {
                         dr = g1.return_dt("apppartyalltypeamountdetailslistgstar '" + ula.CIN + "','" + ula.sdate + "','" + ula.edate + "'," + ula.Index + "," + ula.Count + ",'"+ula.ExecId+"','"+ula.Type+"'");

                    }

                    

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
                            alldcr1.Add(new Getallpaymenttypelist
                            {
                                invoiceno = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                date = Convert.ToString(dr.Rows[i]["date"].ToString()),
                                amount = Convert.ToDecimal(dr.Rows[i]["amount"].ToString()),
                                doctype = Convert.ToString(dr.Rows[i]["doctype"].ToString()),
                                url = WebConfigurationManager.AppSettings["ErpUrl"] + Convert.ToString(dr.Rows[i]["url"].ToString()) + "&viewtype=App",
                            });
                        }

                        alldcr2.Add(new GetallpaymenttypelistFinal
                        {
                            dispatchdata = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new Getallpaymenttypelists
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