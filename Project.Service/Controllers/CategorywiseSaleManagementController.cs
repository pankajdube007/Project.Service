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
    public class CategorywiseSaleManagementController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetCategoryWiseSalesCompare")]
        public HttpResponseMessage GetDetails(ListofCategorywiseSaleManagement ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CategorywiseSaleManagements> alldcr = new List<CategorywiseSaleManagements>();
                    List<CategorywiseSaleManagement> alldcr1 = new List<CategorywiseSaleManagement>();

                    var dr = g1.return_dr("InvoiceReportManagementCategorywsisecompare '" + ula.CurFromDate.ToString("MM/dd/yyyy") + "','" + ula.CurToDate.ToString("MM/dd/yyyy") + "','" + ula.LastFromDate.ToString("MM/dd/yyyy") + "','" + ula.LastToDate.ToString("MM/dd/yyyy") + "','" + ula.CIN + "','" + ula.Category + "',"+ula.divisionid);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CategorywiseSaleManagement
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                currentyearsaleamt = Convert.ToString(dr["saleamt"].ToString()),
                                lastyearssaleamt = Convert.ToString(dr["lastsaleamt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CategorywiseSaleManagements
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