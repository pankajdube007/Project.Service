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
    public class CashBackCountDataDivisionwiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getcashbackcountdatadivisionwise")]
        public HttpResponseMessage GetDetails(ListCashBackCountDataDivisionwisedetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CashBackCountDataDivisionwiseLists> alldcr = new List<CashBackCountDataDivisionwiseLists>();
                    List<CashBackCountDataDivisionwiseList> alldcr1 = new List<CashBackCountDataDivisionwiseList>();

                    var dr = g1.return_dr("GetCashBackCountDataDivisionwiseForTask '" + ula.CIN + "','" + ula.Category + "','" + ula.Stateid + "','" + ula.StatusType + "'");

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new CashBackCountDataDivisionwiseList
                            {
                               
                                SateId = Convert.ToString(dr["stateid"].ToString()),
                                SateName = Convert.ToString(dr["statenm"].ToString()),
                                WIRECABLE = Convert.ToString(dr["WIRECABLE"].ToString()),
                                WIRINGDEVICES = Convert.ToString(dr["WIRINGDEVICES"].ToString()),
                                MCBDBS = Convert.ToString(dr["MCBDBS"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CashBackCountDataDivisionwiseLists
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