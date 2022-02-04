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
    public class AddFanComboController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/fancobmoadd")]
        public HttpResponseMessage GetDetails(ListAddFanCombo ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<AddFanCombos> alldcr = new List<AddFanCombos>();
                    List<AddFanCombo> alldcr1 = new List<AddFanCombo>();

                    string data1;

                    string val = g2.reterive_val("fancomboadd '" + ula.CIN + "','" + ula.noofcombo + "','" + ula.Catids + "','" + ula.Qty + "','"+ula.totalReward + "','"+ ula.isasiatourselect + "'");

                    if (val == "1")
                    {
                        alldcr1.Add(new AddFanCombo
                        {
                            OutPut = "Order placed successfully",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddFanCombos
                        {
                            result = true,
                            message = "Order placed successfully.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                     
                    }
                    if (val == "2")
                    {
                        alldcr1.Add(new AddFanCombo
                        {
                            OutPut = "Order updated successfully.",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddFanCombos
                        {
                            result = true,
                            message = "Order updated successfully.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                    }
                    else if (val == "10")
                    {
                        alldcr1.Add(new AddFanCombo
                        {
                            OutPut = "No Of Combo No valid",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddFanCombos
                        {
                            result = false,
                            message = "No Of Combo No valid",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                    }

                    else if (val == "3")
                    {
                        alldcr1.Add(new AddFanCombo
                        {
                            OutPut = "Qty Criteria Not Matched",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddFanCombos
                        {
                            result = false,
                            message = "Qty Criteria Not Matched",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                    }


                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Something is wrong."), Encoding.UTF8, "application/json");

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