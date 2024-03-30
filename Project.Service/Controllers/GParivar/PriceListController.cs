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
    public class PriceListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetPriceList")]
        public HttpResponseMessage GetDetails(ListsofPriceLists ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PriceListss> alldcr = new List<PriceListss>();
                    List<PriceLists> alldcr1 = new List<PriceLists>();
                    List<PriceListFinal> PriceListFinal = new List<PriceListFinal>();
                    var dr = g1.return_dt("App_PriceList " + ula.index + "," + ula.Count + ",'"+ula.CIN+"'");

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
                            alldcr1.Add(new PriceLists
                            {
                                BrandName = Convert.ToString(dr.Rows[i]["Comptype"].ToString()),
                                RangeName = Convert.ToString(dr.Rows[i]["RangName"].ToString()),
                                FromDate = dr.Rows[i]["FromDate"].ToString(),
                                ToDate = dr.Rows[i]["ToDate"].ToString(),
                                fileURL = string.IsNullOrEmpty(dr.Rows[i]["UploadFiles"].ToString()) ? "" : (baseurl + "othercompanypricelist/dist/" + Convert.ToString(dr.Rows[i]["UploadFiles"].ToString().Replace(".jpeg", ".pdf"))),
                                imgurl = string.IsNullOrEmpty(dr.Rows[i]["ImagesNm"].ToString()) ? "" : (baseurl + "othercompanypricelist/dist/" + Convert.ToString(dr.Rows[i]["ImagesNm"].ToString())),
                            });
                        }

                        PriceListFinal.Add(new PriceListFinal
                        {
                            pricelistdata = alldcr1,
                            ismore = more,
                        });
                        g1.close_connection();
                        alldcr.Add(new PriceListss
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