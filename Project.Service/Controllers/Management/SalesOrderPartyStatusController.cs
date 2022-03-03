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
    public class SalesOrderPartyStatusController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesOrderPartyStatus")]
        public HttpResponseMessage GetDetails(SalesOrderPartyStatus ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ListSalesOrderPartyStatuss> alldcr = new List<ListSalesOrderPartyStatuss>();
                    List<ListSalesOrderPartyStatus> alldcr1 = new List<ListSalesOrderPartyStatus>();
                   
                    var dr = g1.return_dr("saleOrderSelectAllBranchApp '" + ula.CIN + "','" + ula.DivID + "','" + ula.Category + "','" + ula.Fromdate + "','" + ula.Todate + "'");
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ListSalesOrderPartyStatus
                            {
                                Slno = Convert.ToInt32(dr["slno"].ToString()),
                                PartyID= Convert.ToInt32(dr["partyid"].ToString()),
                                PONO = Convert.ToString(dr["ponum"].ToString()),
                                PoDate = Convert.ToString(dr["podttime"].ToString()),
                                PoStatus = Convert.ToString(dr["postatus"].ToString()),
                                Party = Convert.ToString(dr["PartyName"].ToString()),
                                Branch = Convert.ToString(dr["Branch"].ToString()),
                                Total = Convert.ToString(dr["finalamt"].ToString()),
                                Uniquekey = Convert.ToString(dr["uniquekey"].ToString()),
                                Url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "salesorder-print.aspx?id=" + Convert.ToString(dr["slno"].ToString() + "&uniquekey=" + Convert.ToString(dr["uniquekey"].ToString()))
                   


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ListSalesOrderPartyStatuss
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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