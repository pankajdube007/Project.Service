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
    public class ExecutiveTravelExpensesListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExecutiveTravelExpenses")]
        public HttpResponseMessage GetDetails(ListofExecutiveTravelExpenses ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetExecutiveTravelExpensesLists> alldcr = new List<GetExecutiveTravelExpensesLists>();
                    List<GetExecutiveTravelExpensesList> alldcr1 = new List<GetExecutiveTravelExpensesList>();

                    List<GetTotalList> alldcr2 = new List<GetTotalList>();

                    List<GetExecutiveTravelLists> Final = new List<GetExecutiveTravelLists>();


                    var dr = g1.return_dt("dbo.GetExpensesDetails '" + ula.ExId + "','" + ula.search + "','" + ula.fdate + "','" + ula.tdate + "'");
                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new GetExecutiveTravelExpensesList
                            {

                                Execid = Convert.ToString(dr.Rows[i]["Execid"].ToString()),
                                ExpenseNo = Convert.ToString(dr.Rows[i]["ExpenseNo"].ToString()),
                                TravelDate = Convert.ToString(dr.Rows[i]["TravelDate"].ToString()),
                                ImgBill = string.IsNullOrEmpty(dr.Rows[i]["ImgBill"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr.Rows[i]["ImgBill"]).ToString().TrimEnd(',')),
                                SupplierName = Convert.ToString(dr.Rows[i]["SupplierName"].ToString()),
                                GSTIN = Convert.ToString(dr.Rows[i]["GSTIN"].ToString()),
                                TotalAmt = Convert.ToString(dr.Rows[i]["TotalAmt"].ToString()),
                                createdt = Convert.ToString(dr.Rows[i]["createdt"].ToString()),
                                ApprovalStatus = Convert.ToString(dr.Rows[i]["ApprovalStatus"].ToString()),
                                reimbursementamt = Convert.ToString(dr.Rows[i]["reimbursementamt"].ToString()),
                                catimg = string.IsNullOrEmpty(dr.Rows[i]["catimg"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr.Rows[i]["catimg"]).ToString().TrimEnd(',')),
                                TravelRefNo = Convert.ToString(dr.Rows[i]["TravelRefNo"].ToString()),
                                Travel = Convert.ToString(dr.Rows[i]["Travel"].ToString()),
                                TravelFromDate = Convert.ToString(dr.Rows[i]["TravelFromDate"].ToString()),
                                TravelToDate = Convert.ToString(dr.Rows[i]["TravelToDate"].ToString()),

                                Remark = Convert.ToString(dr.Rows[i]["Remark"].ToString()),

                                ShowEdit          = Convert.ToBoolean(dr.Rows[i]["ShowEdit"].ToString()),
                                MonthlyReport     = Convert.ToString(dr.Rows[i]["MonthlyReport"].ToString()),
                                PaidBY            = Convert.ToString(dr.Rows[i]["PaidBY"].ToString()),
                                EmployeeGeplCodes = Convert.ToString(dr.Rows[i]["EmployeeGeplCodes"].ToString()),
                                EmployeeID        = Convert.ToString(dr.Rows[i]["EmployeeID"].ToString()),
                                Description       = Convert.ToString(dr.Rows[i]["Description"].ToString()),
                                IGSTper           = Convert.ToString(dr.Rows[i]["IGSTper"].ToString()),
                                CGSTamt           = Convert.ToString(dr.Rows[i]["CGSTamt"].ToString()),
                                SGSTamt           = Convert.ToString(dr.Rows[i]["SGSTamt"].ToString()),
                                IGSTamt           = Convert.ToString(dr.Rows[i]["IGSTamt"].ToString()),
                                CGSTper           = Convert.ToString(dr.Rows[i]["CGSTper"].ToString()),
                                SGSTper           = Convert.ToString(dr.Rows[i]["SGSTper"].ToString()),
                                CostBeforeTax     = Convert.ToString(dr.Rows[i]["CostBeforeTax"].ToString()),
                                CategoryName      = Convert.ToString(dr.Rows[i]["CategoryName"].ToString()),
                                CategoryId        = Convert.ToString(dr.Rows[i]["CategoryId"].ToString()),
                                MerchantId        = Convert.ToString(dr.Rows[i]["MerchantId"].ToString()),
                                TravelID          = Convert.ToString(dr.Rows[i]["TravelID"].ToString()),
                                BillNo            = Convert.ToString(dr.Rows[i]["BillNo"].ToString()),

                                SlNoAttendees = Convert.ToString(dr.Rows[i]["SlNoAttendees"].ToString()),
                                TaxPer = Convert.ToString(dr.Rows[i]["TaxPer"].ToString()),
                                GSTType = Convert.ToString(dr.Rows[i]["GSTType"].ToString()),
                                SlNoExecutiveTravelExpenses = Convert.ToString(dr.Rows[i]["SlNoExecutiveTravelExpenses"].ToString()),

                            });
                        }

                        alldcr2.Add(new GetTotalList
                        {
                            TotalAmount = Math.Round(Convert.ToDecimal(dr.Compute("SUM(TotalAmount)", string.Empty))).ToString(),
                            TotalReimbursableAmount = Math.Round(Convert.ToDecimal(dr.Compute("SUM(TotalReimbursableAmount)", string.Empty))).ToString(),
                        });
                    }
                    //if (dr.HasRows)
                    //{
                    //    string baseurl = _goldMedia.MapPathToPublicUrl("");
                    //    while (dr.Read())
                    //    {
                    //        alldcr2.Add(new GetTotalList
                    //        {

                               

                    //            TotalAmount = Math.Round(Convert.ToDecimal(dr.Compute("SUM(TotalAmount)", string.Empty))).ToString(),
                    //            TotalReimbursableAmount = Math.Round(Convert.ToDecimal(dr.Compute("SUM(TotalReimbursableAmount)", string.Empty))).ToString(),
                    //        });
                    //    }

                    //}

                    Final.Add(new GetExecutiveTravelLists
                    {

                        ExecutiveTravelExpensesList = alldcr1,
                        TotalList = alldcr2

                        //listdtwisesumList = alldcr2,
                        //LocalConveyanceList= alldcr1
                    });


                    g1.close_connection();
                    alldcr.Add(new GetExecutiveTravelExpensesLists
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = Final,
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;

                    //g1.close_connection();
                    //alldcr.Add(new GetExecutiveTravelExpensesLists
                    //{
                    //    result = true,
                    //    message = string.Empty,
                    //    servertime = DateTime.Now.ToString(),
                    //    data = alldcr1,
                    //});
                    //data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    //response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    //return response;
                    //}
                    //else
                    //{
                    //    g1.close_connection();
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                    //    return response;
                    //}
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