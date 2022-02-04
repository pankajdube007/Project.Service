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
    public class ItemProblemMapController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/MapItemProblem")]
        public HttpResponseMessage GetDetails(ListofItemProblemMap ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;

                string ordermsg = string.Empty;

                string QRSplit = ula.QrCode;
                string barcode = QRSplit.ToString().Split('#')[0];
                string qrtype = QRSplit.ToString().Split('#')[1];
                string qrslno = QRSplit.ToString().Split('#')[2];
                if (barcode.Length == 5 && (qrtype == "1" || qrtype == "2") && qrslno.Length == 8)
                {

                    List<ItemProblemMaps> alldcr = new List<ItemProblemMaps>();
                    List<ItemProblemMap> alldcr1 = new List<ItemProblemMap>();
                    var dr = g2.return_dt("MapItemWithProblem '" + ula.QrCode + "',"+ula.itemid+",'" + ula.remark + "','" + ula.ProblemDetails.ToString() + "','" + barcode + "'," + Convert.ToInt32(qrtype) + ",'" + qrslno + "'");
                    g2.close_connection();

                    if (dr.Rows.Count > 0)
                    {
                        alldcr1.Add(new ItemProblemMap
                        {
                            type = Convert.ToInt32(dr.Rows[0]["typee"]),

                        });

                        if (Convert.ToInt32(dr.Rows[0]["typee"]) == 1)
                        {
                            alldcr.Add(new ItemProblemMaps
                            {
                                result = true,
                                message = "Item Not Matched",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        if (Convert.ToInt32(dr.Rows[0]["typee"]) == 2)
                        {
                            alldcr.Add(new ItemProblemMaps
                            {
                                result = true,
                                message = "Qr Allready Mapped With Problem",
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
                            alldcr.Add(new ItemProblemMaps
                            {
                                result = true,
                                message = "Item Mapped With Problem",
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
                else
                {

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, please check QR code again!!!!!!!!"), Encoding.UTF8, "application/json");
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