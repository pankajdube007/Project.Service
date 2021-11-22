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
    public class ComboTotalQuantityController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getComboTotalQuantity")]
        public HttpResponseMessage GetDetails(ListsofComboTotalQty ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;
                    decimal totalqty = 0;
                    List<ComboTotalQtys> alldcr = new List<ComboTotalQtys>();
                    List<ComboTotalQty> alldcr1 = new List<ComboTotalQty>();
                    List<ComboTotalQtyFinal> QtyFinal = new List<ComboTotalQtyFinal>();
                    var dr = g1.return_dr("AppComboTotalQuantity '" + ula.ComboId + "','" + ula.ComboQty + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ComboTotalQty
                            {
                                itemname = Convert.ToString(dr["ItemName"].ToString()),
                                range = Convert.ToString(dr["rangenm"].ToString()),
                                qty = Convert.ToString(dr["qty"].ToString()),
                            });
                            totalqty = totalqty + Convert.ToDecimal(dr["qty"].ToString());
                        }

                        QtyFinal.Add(new ComboTotalQtyFinal
                        {
                            ComboTotalQtyDetails = alldcr1,
                            totalqty = totalqty.ToString(),
                        });

                        g1.close_connection();
                        alldcr.Add(new ComboTotalQtys
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = QtyFinal,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

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