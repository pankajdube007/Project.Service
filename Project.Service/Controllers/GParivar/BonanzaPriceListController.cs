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
    public class BonanzaPriceListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getBonanzaPriceList")]
        public HttpResponseMessage GetDetails(BonanzaPriceList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    List<ListBonanzaPrices> alldcr = new List<ListBonanzaPrices>();
                    List<ListBonanzaPrice> alldcr1 = new List<ListBonanzaPrice>();

                    var dr = g1.return_dr("bonanzapricerlist '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ListBonanzaPrice
                            {
                                PriceId = Convert.ToInt32(dr["priceid"].ToString()),
                                Price = Convert.ToString(dr["price"].ToString()),
                                Qty = Convert.ToString(dr["qty"].ToString()),
                                DealerPoint = Convert.ToString(dr["dealerpoint"].ToString()),
                                ProductPoint = Convert.ToString(dr["point"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ListBonanzaPrices
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}