using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers

{
    public class AllTypePaymentDetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getallpaymenttypedetails")]
        public HttpResponseMessage GetDetails(ListAllTypePaymentDetail ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<AllTypePaymentDetails> alldcr = new List<AllTypePaymentDetails>();
                    List<AllTypePaymentDetail> alldcr1 = new List<AllTypePaymentDetail>();
                    var dr = (SqlDataReader)null;

                    if (ula.ExecId == 0)
                    {
                        dr = g1.return_dr("apppartyalltypeamountdetails '" + ula.CIN + "','" + ula.sdate + "','" + ula.edate + "'");
                    }

                    else
                    {
                        dr = g1.return_dr("apppartyalltypeamountdetailsgstar '" + ula.CIN + "','" + ula.sdate + "','" + ula.edate + "','"+ula.ExecId+"'");

                    }





                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new AllTypePaymentDetail
                            {
                                sumofinvoice = Convert.ToDecimal(dr["sumofinvoice"].ToString()),
                                noofinvoice = Convert.ToInt32(dr["noofinvoice"].ToString()),
                                sumofdebitnote = Convert.ToDecimal(dr["sumofdebitnote"].ToString()),
                                noofdebitnote = Convert.ToInt32(dr["noofdebitnote"].ToString()),
                                sumofpayment = Convert.ToDecimal(dr["sumofpayment"].ToString()),
                                noofpayment = Convert.ToInt32(dr["noofpayment"].ToString()),
                                sumofcreditnote = Convert.ToDecimal(dr["sumofcreditnote"].ToString()),
                                noofcreditnote = Convert.ToInt32(dr["noofcreditnote"].ToString()),
                                ledgerbalance = Convert.ToDecimal(dr["ladgerbalance"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AllTypePaymentDetails
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