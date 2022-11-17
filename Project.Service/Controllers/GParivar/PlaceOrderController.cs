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
    public class PlaceOrderController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addPlaceOrder")]
        public HttpResponseMessage GetDetails(ListofAddPlaceOrder ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;

                string ordermsg = string.Empty;

                List<AddPlaceOrders> alldcr = new List<AddPlaceOrders>();
                List<AddPlaceOrder> alldcr1 = new List<AddPlaceOrder>();
                var dr = g2.return_dt("AddPlaceOrder '"
                    + ula.CIN + "',"
                    + ula.DivisionId + ",'"
                    + ula.OrderDetails.ToString().Replace("WithTaxAmt", "WithTaxAmount") + "','"
                    + ula.Amount + "','"
                    + ula.Category + "',"
                    + ula.ExId + ",'"
                    + ula.DeviceType + "','"
                    + ula.remark + "'");

                g2.close_connection();

                if (dr.Rows.Count > 0)
                {
                    alldcr1.Add(new AddPlaceOrder
                    {
                        type = dr.Rows[0]["type1"].ToString(),
                        message = dr.Rows[0]["errormsg"].ToString().TrimEnd(','),
                    });

                    if ((dr.Rows[0]["type1"].ToString() == "Ponum"))
                    {
                        alldcr.Add(new AddPlaceOrders
                        {
                            result = true,
                            message = "",
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
                        alldcr.Add(new AddPlaceOrders
                        {
                            result = false,
                            message = "",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                else
                {
                    g2.close_connection();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

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