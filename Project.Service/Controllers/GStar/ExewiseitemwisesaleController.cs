using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class ExewiseitemwisesaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Exewiseitemwisesale")]
        public HttpResponseMessage GetDetails(ExewiseitemwisesaleList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<Exewiseitemwisesale> alldcr = new List<Exewiseitemwisesale>();
                    List<Exewiseitemwisesales> alldcr1 = new List<Exewiseitemwisesales>();
                    var dr = g1.return_dr($"dbo.Exewiseitemwisesaleapp  {ula.ExId} , {ula.ItemId} , '{ula.FromDate}' , '{ula.Todate}' , {ula.Hierarchy}  ");
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new Exewiseitemwisesales
                            {

                                MonthName = Convert.ToString(dr["Monthname"].ToString()),
                                CurrentQuantity = Convert.ToString(dr["qty_C"].ToString()),
                                CurrentAmount = Convert.ToString(dr["amt_C"].ToString()),
                                lastYearQuantity = Convert.ToString(dr["qty_L"].ToString()),
                                lastYearAmount = Convert.ToString(dr["amt_L"].ToString()),
                                AmountDiffPer = Convert.ToString(dr["amtpercentage"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Exewiseitemwisesale
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available !!!"), Encoding.UTF8, "application/json");

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