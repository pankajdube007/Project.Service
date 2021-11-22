using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class CashBackCountDataCategorywiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getcashbackcountdatacategorywise")]
        public HttpResponseMessage GetDetails(ListCashBackCountDataCategorywiseFordetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CashBackCountDataCategorywiseForLists> alldcr = new List<CashBackCountDataCategorywiseForLists>();
                    List<CashBackCountDataCategorywiseForList> alldcr1 = new List<CashBackCountDataCategorywiseForList>();

                    var dr = g1.return_dr("GetCashBackCountDataCategorywiseForTask '" + ula.CIN + "','" + ula.Category + "','" + ula.Stateid + "','" + ula.StatusType + "'");

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new CashBackCountDataCategorywiseForList
                            {
                               
                                SateId = Convert.ToString(dr["stateid"].ToString()),
                                SateName = Convert.ToString(dr["statenm"].ToString()),
                                Retailer = Convert.ToString(dr["Retailer"].ToString()),
                                CounterBoy = Convert.ToString(dr["CounterBoy"].ToString()),
                                Electrician = Convert.ToString(dr["Electrician"].ToString()),
        

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CashBackCountDataCategorywiseForLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data"), Encoding.UTF8, "application/json");

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