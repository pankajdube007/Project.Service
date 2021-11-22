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
    public class CatwiseItemMarginController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getmargincatwise")]
        public HttpResponseMessage GetDetails(ListCatWiseItemMargin ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN !="")
            {
                try
                {
                    string data1;

                    List<CatWiseItemMarginLists> alldcr = new List<CatWiseItemMarginLists>();
                    List<CatWiseItemMarginList> alldcr1 = new List<CatWiseItemMarginList>();

                    var dr = g1.return_dr("Managementcostingrpt'" + ula.CatId + "','" + ula.Category + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CatWiseItemMarginList
                            {

                                ItemId = Convert.ToInt32(dr["ItemId"].ToString()),
                                ItemCode = Convert.ToString(dr["productCode1"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                LastPerValue= Convert.ToString(dr["lastper"].ToString()),
                                TotalPurValue = Convert.ToString(dr["totpur"].ToString()),
                                OfferPrice = Convert.ToString(dr["Offerprice"].ToString()),
                                FinalAmount = Convert.ToString(dr["final"].ToString()),
                                FinalDiscount = Convert.ToString(dr["finaldicount"].ToString()),
                                Margin = Convert.ToString(dr["margin"].ToString()),
                                MarginPer = Convert.ToString(dr["marginper"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CatWiseItemMarginLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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