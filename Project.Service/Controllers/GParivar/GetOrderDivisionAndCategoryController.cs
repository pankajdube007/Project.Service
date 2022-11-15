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
    public class OrderDivisionCat
    {
        public int divisionid { get; set; } = 0;
        public string divisionnm { get; set; } = "";
        public int catid { get; set; } = 0;
        public string catnm { get; set; } = "";
        public string catimage { get; set; } = "";
    }

    public class GetOrderDivisionAndCategoryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrderDivisionAndCategoryDetailList")]
        public HttpResponseMessage GetAllUserdetails(OrderDivisionAndCategory ula)
        {
            //DataConection g1 = new DataConection();
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<OrderDivisionAndCategoryLists> alldcr = new List<OrderDivisionAndCategoryLists>();
                    List<OrderDivisionAndCategoryList> alldcr1 = new List<OrderDivisionAndCategoryList>();
                    var dr = g1.return_dr("GetOrderDivisionAndCategoryList '" + ula.CIN + "','trade'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string baseurl = _goldMedia.MapPathToPublicUrl("");
                            alldcr1.Add(new OrderDivisionAndCategoryList
                            {
                                divisionid = Convert.ToInt32(dr["divisionid"]),
                                divisionnm = dr["divisionnm"].ToString(),
                                catid = Convert.ToInt32(dr["catid"].ToString()),
                                catnm = dr["catnm"].ToString(),
                                catimage = string.IsNullOrEmpty(dr["uploadfile"].ToString().TrimEnd(',')) ? "" : (baseurl + "categorymaster/" + dr["uploadfile"].ToString().TrimEnd(',')),
                                ShowPlaceOrderButton = "true",
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OrderDivisionAndCategoryLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}