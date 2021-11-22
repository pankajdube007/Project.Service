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
    public class DhanbarseQwikpayPriceListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetDhanbarseQwikpayPriceList")]
        public HttpResponseMessage GetDetails(ListsofDhanbarseQwikpayPriceList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DhanbarseQwikpayPriceLists> alldcr = new List<DhanbarseQwikpayPriceLists>();
                    List<DhanbarseQwikpayPriceList> alldcr1 = new List<DhanbarseQwikpayPriceList>();
                    List<DhanbarseQwikpayPriceListFinal> PriceListFinal = new List<DhanbarseQwikpayPriceListFinal>();
                    var dr = g1.return_dt("AppdhanbarseQwikpayPriceList " + ula.Type + "," + ula.index + "," + ula.Count+",'"+ula.CIN+"'");

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new DhanbarseQwikpayPriceList
                            {
                                PolicyType = Convert.ToString(dr.Rows[i]["policytype"].ToString()),
                                PolicyName = Convert.ToString(dr.Rows[i]["policyname"].ToString()),
                                FromDate = dr.Rows[i]["fromdate"].ToString(),
                                ToDate = dr.Rows[i]["todate"].ToString(),
                                fileURL = string.IsNullOrEmpty(dr.Rows[i]["links"].ToString()) ? "" : (baseurl + "schemefiles/" +  Convert.ToString(dr.Rows[i]["links"].ToString().Replace(".jpeg", ".pdf")).ToLower()),
                                imgurl = string.IsNullOrEmpty(dr.Rows[i]["AppImages"].ToString()) ? "" : (baseurl + "schemefiles/" + Convert.ToString(dr.Rows[i]["AppImages"].ToString()).ToLower()),
                            });
                        }

                        PriceListFinal.Add(new DhanbarseQwikpayPriceListFinal
                        {
                            pricelistdata = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new DhanbarseQwikpayPriceLists
                        {
                            result = true,
                            message = "",
                            servertime = DateTime.Now.ToString(),
                            data = PriceListFinal,
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