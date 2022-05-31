using Newtonsoft.Json;
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
                    var dr = g1.return_dr("dbo.GetExpensesDetails '" + ula.ExId + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetExecutiveTravelExpensesList
                            {

                                Execid = Convert.ToString(dr["Execid"].ToString()),
                                ExpenseNo = Convert.ToString(dr["ExpenseNo"].ToString()),
                                TravelDate = Convert.ToString(dr["TravelDate"].ToString()),
                                ImgBill = string.IsNullOrEmpty(dr["ImgBill"].ToString().TrimEnd(',')) ? string.Empty : (Convert.ToString(dr["ImgBill"]).ToString().TrimEnd(',')),
                                SupplierName = Convert.ToString(dr["SupplierName"].ToString()),
                                GSTIN = Convert.ToString(dr["GSTIN"].ToString()),
                                TotalAmt = Convert.ToString(dr["TotalAmt"].ToString()),
                                createdt = Convert.ToString(dr["createdt"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetExecutiveTravelExpensesLists
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