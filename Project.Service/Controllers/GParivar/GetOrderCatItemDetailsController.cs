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
    public class GetOrderCatItemDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrderCatItemDetailList")]
        public HttpResponseMessage GetDetails(ListOfGetOrderCatItemDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetOrderCatItemDetailss> alldcr = new List<GetOrderCatItemDetailss>();
                    List<GetOrderCatItemDetails> alldcr1 = new List<GetOrderCatItemDetails>();

                    var dr = g1.return_dr("GetAppCatlagueItemDetails " + ula.CategoryId + "," + ula.DivisionId + ",'" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetOrderCatItemDetails
                            {
                                CategoryId = dr["categoryid"].ToString(),
                                categorynm = dr["categorynm"].ToString(),
                                SubCategoryId = dr["rangeid"].ToString(),
                                Subcategorynm = dr["rangenm"].ToString(),
                                DivisionId = dr["divisionid"].ToString(),
                                divisionnm = dr["divisionnm"].ToString(),
                                itemcode = dr["itemcode"].ToString(),
                                itemnm = dr["itemnm"].ToString(),
                                Subcategoryurl = string.IsNullOrEmpty(dr["uploadfile"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "subcategorymaster/" + Convert.ToString(dr["uploadfile"]).ToString().TrimEnd(',')),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetOrderCatItemDetailss
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