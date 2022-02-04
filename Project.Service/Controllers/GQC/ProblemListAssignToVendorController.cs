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
    public class ProblemListAssignToVendorController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getProblemListAssignToVendor")]
        public HttpResponseMessage GetDetails(ProblemListAssignToVendor ula)
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
                    List<AssignToVendor> alldcr = new List<AssignToVendor>();
                    List<ItemData> item = new List<ItemData>();
                    List<ProblemData> Problem = new List<ProblemData>();
                    var dr = g1.return_dt("GetProblemListForVendor'" + ula.QrCode + "','" + slno + "'");
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["Out"]) == 1)
                        {
                            alldcr.Add(new AssignToVendor
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

                        else if (Convert.ToInt32(dr.Rows[0]["Out"]) == 2)
                        {
                            alldcr.Add(new AssignToVendor
                            {
                                result = false,
                                message = "Vendor already Assign",
                                servertime = DateTime.Now.ToString(),
                                data = item,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        
                        else
                        {



                            var dr1 = g1.return_dr("VendorItemProbList '" + Convert.ToString(dr.Rows[0]["HeadID"]) + "'");
                            while (dr1.Read())
                            {
                                Problem.Add(new ProblemData
                                {
                                    problemid = Convert.ToString(dr1["slno"].ToString()),
                                    problem = Convert.ToString(dr1["ItemProblem"].ToString()),
                                    remark = Convert.ToString(dr1["Remark"].ToString()),

                                });
                            }
                            item.Add(new ItemData
                            {
                                Issue = Problem,
                                qrcode = Convert.ToString(dr.Rows[0]["QRCODE"]),
                                headid = Convert.ToString(dr.Rows[0]["HeadID"]),
                                itemName = Convert.ToString(dr.Rows[0]["itemName"]),
                                ItemId = Convert.ToString(dr.Rows[0]["ItemID"]),
                                remark = Convert.ToString(dr.Rows[0]["remark"]),
                            });


                            g1.close_connection();
                            alldcr.Add(new AssignToVendor
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