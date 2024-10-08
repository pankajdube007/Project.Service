﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GetMerchantCatController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getGetmerchantCatList")]
        public HttpResponseMessage GetDetails(ListmerchantCatList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetmerchantCatLists> alldcr = new List<GetmerchantCatLists>();
                    List<GetmerchantCatList> alldcr1 = new List<GetmerchantCatList>();
                    var dr = g1.return_dr("dbo.GetmerchantCatListnew '" + ula.ExId + "', " + ula.slno + ",'"+ula.Date+"',"+ula.Trvlid);
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetmerchantCatList
                            {

                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                catimg = string.IsNullOrEmpty(dr["catimg"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr["catimg"]).ToString().TrimEnd(',')),  // Convert.ToString(dr["catimg"].ToString()),
                                usedamt = Convert.ToString(dr["usedamt"].ToString()),
                                Limit = Convert.ToString(dr["Limit"].ToString()),
                                Balance = Convert.ToString(dr["Balance"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetmerchantCatLists
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