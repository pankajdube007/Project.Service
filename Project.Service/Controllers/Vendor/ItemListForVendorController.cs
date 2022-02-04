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
    public class ItemListForVendorController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getItemListForVendor")]
        public HttpResponseMessage GetDetails(ItemListVendor ula)
        {

            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<ListVendor> alldcr = new List<ListVendor>();
                List<VendorData> item = new List<VendorData>();
                List<ProblemListData> Problem = new List<ProblemListData>();
                var dr = g1.return_dt("getItemListForVendordata'" + ula.QrSlno + "','" + ula.QrCode + "','" + ula.VendorID + "'");
                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["Out"]) == 1)
                    {
                        alldcr.Add(new ListVendor
                        {
                            result = false,
                            message = "VendorID Not Found",
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
                            Problem = new List<ProblemListData>();

                            for (int j = 0; j < dr1.Rows.Count; j++)
                            {

                                Problem.Add(new ProblemListData
                                {
                                    problemid = Convert.ToString(dr1.Rows[j]["slno"]),
                                    problem = Convert.ToString(dr1.Rows[j]["ItemProblem"]),
                                    remark = Convert.ToString(dr1.Rows[j]["remark"]),

                                });

                            }


                            item.Add(new VendorData
                            {
                                Issue = Problem,
                                itemName = Convert.ToString(dr.Rows[i]["itemname"]),
                                ItemId = Convert.ToString(dr.Rows[i]["ItemID"]),
                                qrcode = Convert.ToString(dr.Rows[i]["qrcode"]),
                                qrslno = Convert.ToString(dr.Rows[i]["qrslno"]),
                                headid = Convert.ToString(dr.Rows[i]["HeadID"]),
                            });
                         
                        }

                        g1.close_connection();
                        alldcr.Add(new ListVendor
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