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
    public class QrScanController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getqrscan")]
        public HttpResponseMessage GetDetails(ListofQrScan ula)
        {
            
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                string ordermsg = string.Empty;

                string QRSplit = ula.QrCode;
                string barcode = QRSplit.ToString().Split('#')[0];
                string type = QRSplit.ToString().Split('#')[1];
                string slno = QRSplit.ToString().Split('#')[2];

                if (barcode.Length == 5 && (type == "1" || type == "2") && slno.Length == 8)
                {
                    List<QrScans> alldcr = new List<QrScans>();
                    List<data1> item = new List<data1>();
                    List<Item> prolemlist = new List<Item>();
                    List<Problem> Problem = new List<Problem>();
                    var dr = g1.return_dt("QRSCANE'" + ula.QrCode + "','" + barcode + "','" + Convert.ToInt32(type) + "','" + slno + "'");
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["RES"]) == 0)
                        {
                            alldcr.Add(new QrScans
                            {
                                result = false,
                                message = "Qr Not Found",
                                servertime = DateTime.Now.ToString(),
                                data = item,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        else if (Convert.ToInt32(dr.Rows[0]["RES"]) == 2)
                        {
                            item.Add(new data1
                            {
                                Item = prolemlist
                            });

                            alldcr.Add(new QrScans
                            {
                                result = true,
                                message = "Qr Found But Item Not Found",
                                servertime = DateTime.Now.ToString(),
                                data = item,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        // qr and  item found
                        else
                        {

                            var dr1 = g1.return_dr("ItemProbList '" + Convert.ToString(dr.Rows[0]["itemid"]) + "'");
                            while (dr1.Read())
                            {
                                Problem.Add(new Problem
                                {
                                    problemid = Convert.ToString(dr1["slno"].ToString()),
                                    problem = Convert.ToString(dr1["ItemProblem"].ToString()),

                                });
                            }
                            prolemlist.Add(new Item
                            {
                                problem = Problem,
                                itemName = Convert.ToString(dr.Rows[0]["itemname"]),
                                ItemId = Convert.ToString(dr.Rows[0]["itemid"]),
                            });

                            item.Add(new data1
                            {
                                Item = prolemlist
                            });
                            g1.close_connection();
                            alldcr.Add(new QrScans
                            {
                                result = true,
                                message = string.Empty,
                                servertime = DateTime.Now.ToString(),
                                data = item,
                            });

                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        g1.close_connection();
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