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
    public class VendorRemarkUpdateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/UpdateVendorRemark")]
        public HttpResponseMessage GetDetails(VendorRemarkUpdate ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                //Vendor Action 1 Means Replace Product
                //Vendor Action 2 Means Product Mark as Scraped

                List<ItemProblemVendorsUpdate> alldcr = new List<ItemProblemVendorsUpdate>();
                List<ItemProblemVendorUpdateData> item = new List<ItemProblemVendorUpdateData>();
                List<ItemProblemVendorUpdate> Problem = new List<ItemProblemVendorUpdate>();
                var dr = g1.return_dt("updateVendorRemark'" + ula.HeadID + "','" + ula.QrSlno + "','" + ula.QrCode + "','" + ula.VendorID + "','" + ula.VendorRemark + "','" + ula.VendorAction + "'");

                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["Out"]) == 1)
                    {
                        alldcr.Add(new ItemProblemVendorsUpdate
                        {
                            result = false,
                            message = "VendorID Not Found or Vendor Allready Remarked ",
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
                        //var objVendorData = new List<VendorData>();
                        //var objProblemlist = new List<ProblemList>();

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {

                            var dr1 = g1.return_dt("VendorProbListdata '" + Convert.ToString(dr.Rows[i]["itemid"]) + "','" + Convert.ToString(dr.Rows[i]["HeadID"]) + "'");

                            // TO empty Problem Array after increment
                            Problem = new List<ItemProblemVendorUpdate>();

                            for (int j = 0; j < dr1.Rows.Count; j++)
                            {

                                Problem.Add(new ItemProblemVendorUpdate
                                {
                                    problemid = Convert.ToString(dr1.Rows[j]["slno"]),
                                    problem = Convert.ToString(dr1.Rows[j]["ItemProblem"]),
                                    remark = Convert.ToString(dr1.Rows[j]["remark"]),

                                });

                            }


                            item.Add(new ItemProblemVendorUpdateData
                            {
                                Issue = Problem,
                                itemName = Convert.ToString(dr.Rows[i]["itemname"]),
                                ItemId = Convert.ToString(dr.Rows[i]["ItemID"]),
                                qrcode = Convert.ToString(dr.Rows[i]["qrcode"]),
                                qrslno = Convert.ToString(dr.Rows[i]["qrslno"]),
                                headid = Convert.ToString(dr.Rows[i]["HeadID"]),
                                //change in API Parameter added for final screen
                                updateon = Convert.ToString(dr.Rows[i]["updateon"]),
                                vendorRemark = Convert.ToString(dr.Rows[i]["vendorremark"]),
                                action = Convert.ToString(dr.Rows[i]["action"]),



                            });

                        }

                        g1.close_connection();
                        alldcr.Add(new ItemProblemVendorsUpdate
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
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}