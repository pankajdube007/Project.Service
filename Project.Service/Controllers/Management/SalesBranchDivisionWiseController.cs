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
    public class SalesBranchDivisionWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetSalesBranchDivisionWise")]
        public HttpResponseMessage GetDetails(SalesBranchDivisionWise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<DataBranchnwiseSales> alldcr = new List<DataBranchnwiseSales>();
                List<DataBranchnwiseSale> alldcr1 = new List<DataBranchnwiseSale>();

                var dr = g1.return_dr("TodayInvoiceReportManagementBranchdivisionwsise '" + ula.BranchID + "','" + ula.fromdate + "','" + ula.todate + "','" + ula.CIN + "','" + ula.Category + "'");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new DataBranchnwiseSale
                        {
                            divisionid = Convert.ToInt32(dr["divisionid"].ToString()),
                            divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                            saleamt = Convert.ToString(dr["saleamt"].ToString()),
                        });
                    }
                    g1.close_connection();
                    alldcr.Add(new DataBranchnwiseSales
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
    }
}