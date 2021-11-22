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
    public class CoupenCodeCheckController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CheckBarcode")]
        public HttpResponseMessage GetAllUserdetails(ListofCoupenCodeCheck ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.barcode != "")
            {
                try
                {
                    string data1;
                    int slno = 0;
                    string[] tokens = ula.barcode.Split('#');
                    string barcode = string.Empty;
                    if(tokens.Length==2)
                    {
                        slno = Convert.ToInt32(tokens[1]);
                        barcode = tokens[0].ToString();
                    }
                    else
                    {
                        slno = 0;
                        barcode = ula.barcode;
                    }
                    List<CoupenCodeChecks> alldcr = new List<CoupenCodeChecks>();
                    List<CoupenCodeCheck> alldcr1 = new List<CoupenCodeCheck>();
                    var dr = g1.return_dr(" AppQrCouponCodeCheck " + slno + ",'" + barcode + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CoupenCodeCheck
                            {
                              
                                CoupenCode = Convert.ToString(dr["CoupenCode"].ToString()),
                                CoupenValue = Convert.ToString(dr["CoupenValue"].ToString()),
                                CoupenExpirydt = Convert.ToString(dr["CoupenExpirydt"].ToString()),
                                IsUsed = Convert.ToString(dr["IsUsed"].ToString()),
                                CustomerCategory = Convert.ToString(dr["CustomerCategory"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CoupenCodeChecks
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