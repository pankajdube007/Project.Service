﻿using Newtonsoft.Json;
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
    public class ManagementTotalSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTotalSaleBranchWise")]
        public HttpResponseMessage GetDetails(ListofManagementTotalSale ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ManagementTotalSales> alldcr = new List<ManagementTotalSales>();
                    List<ManagementTotalSale> alldcr1 = new List<ManagementTotalSale>();

                    var dr = g1.return_dr("InvoiceReportManagementBranch '" + ula.FromDate + "','" + ula.ToDate + "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementTotalSale
                            {
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                branchnm = Convert.ToString(dr["branchnm"].ToString()),
                                wiringdevices = Convert.ToString(dr["wiring"].ToString()),
                                lights = Convert.ToString(dr["lights"].ToString()),
                                wireandcable = Convert.ToString(dr["wire"].ToString()),
                                pipesandfittings = Convert.ToString(dr["pipes"].ToString()),
                                mcbanddbs = Convert.ToString(dr["mcb"].ToString()),
                                fan = Convert.ToString(dr["fan"].ToString()),
                                Automation = Convert.ToString(dr["AU"].ToString()),
                                healthcare = Convert.ToString(dr["HEALTHCARE"].ToString()),
                                totalsale = Convert.ToString(dr["totalsale"].ToString()),
                                branchcontribution = Convert.ToString(dr["branchcontribution"].ToString()),
                                branchcontributionpercentage = Convert.ToString(dr["contribypercent"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementTotalSales
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