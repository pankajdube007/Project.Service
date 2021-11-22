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
    public class salereturnrequestshowController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/salereturnrequestshows")]
        public HttpResponseMessage GetDetails(salereturnrequestshowlist ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<salereturnrequestshows> alldcr = new List<salereturnrequestshows>();
                    List<salereturnrequestshow> alldcr1 = new List<salereturnrequestshow>();

                    var dr = g1.return_dr("salereturnrequestshow " + ula.slno);
                    string baseurl = _goldMedia.MapPathToPublicUrl("");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new salereturnrequestshow
                            {
                                rtype = Convert.ToString(dr["rtype"].ToString()),
                                divid = Convert.ToInt32(dr["divid"].ToString()),
                                qty = Convert.ToInt32(dr["qty"].ToString()),
                                qtytype = Convert.ToInt32(dr["qtytype"].ToString()),
                                requestpickupfromdt = Convert.ToString(dr["requestpickupfromdt"].ToString()),
                                requestpickuptodt = Convert.ToString(dr["requestpickuptodt"].ToString()),
                                reason = Convert.ToInt32(dr["reason"].ToString()),
                                Image1 = string.IsNullOrEmpty(dr["Image1"].ToString().Trim(',')) ? "" : (baseurl + "salereturnrequest/" + dr["Image1"].ToString().Trim(',')),
                                Image2 = string.IsNullOrEmpty(dr["Image2"].ToString().Trim(',')) ? "" : (baseurl + "salereturnrequest/" + dr["Image2"].ToString().Trim(',')),
                                Image3 = string.IsNullOrEmpty(dr["Image3"].ToString().Trim(',')) ? "" : (baseurl + "salereturnrequest/" + dr["Image3"].ToString().Trim(',')),
                                remark = Convert.ToString(dr["remark"].ToString()),
                                level = Convert.ToString(dr["level"].ToString()),
                                requestdt = Convert.ToString(dr["requestdt"].ToString()),
                                requestno = Convert.ToString(dr["requestno"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                reasonnm = Convert.ToString(dr["reasonnm"].ToString()),
                                approve2dt = Convert.ToString(dr["approve2dt"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                pickupdt = Convert.ToString(dr["pickupdt"].ToString()),
                                pickuptime = Convert.ToString(dr["pickuptime"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                contactno = Convert.ToString(dr["contactno"].ToString()),
                                amount = Convert.ToString(dr["amount"].ToString()),
                                excutiveapprovedt = Convert.ToString(dr["excutiveapprovedt"].ToString()),
                                itemapprlink = string.IsNullOrEmpty(dr["itemapprlink"].ToString().Trim(',')) ? "" : (dr["itemapprlink"].ToString().Trim(',')),
                                billingappr = Convert.ToString(dr["billingappr"].ToString()),
                                billingapprdt = Convert.ToString(dr["billingapprdt"].ToString()),
                                invoicedebit = string.IsNullOrEmpty(dr["invoicedebit"].ToString().Trim(',')) ? "" : (baseurl + "salereturnrequest/" + dr["invoicedebit"].ToString().Trim(',')),
                                lrno = Convert.ToString(dr["lrno"].ToString()),
                                lrdt = Convert.ToString(dr["lrdt"].ToString()),
                                docno = Convert.ToString(dr["docno"].ToString()),
                                docdt = Convert.ToString(dr["docdt"].ToString()),
                                doctype = Convert.ToInt32(dr["doctype"].ToString()),
                                docamount = Convert.ToString(dr["docamount"].ToString()),
                                transporternm = Convert.ToString(dr["transporternm"].ToString()),
                            });
                        }
                        g1.close_connection();

                        alldcr.Add(new salereturnrequestshows
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}