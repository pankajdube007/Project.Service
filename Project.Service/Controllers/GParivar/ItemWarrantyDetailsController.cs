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
    public class ItemWarrantyDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getItemWarrantyDetails")]
        public HttpResponseMessage GetAllUserdetails(ListofItemWarrantyDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.barcode != "")
            {
                try
                {
                    string data1;
                    //int slno = 0;
                    //string[] tokens = ula.barcode.Split('#');
                    //string barcode = string.Empty;
                    //if (tokens.Length == 2)
                    //{
                    //    slno = Convert.ToInt32(tokens[1]);
                    //    barcode = tokens[0].ToString();
                    //}
                    //else
                    //{
                    //    slno = 0;
                    //    barcode = ula.barcode;
                    //}
                    List<ItemWarrantyDetailss> alldcr = new List<ItemWarrantyDetailss>();
                    List<ItemWarrantyDetails> alldcr1 = new List<ItemWarrantyDetails>();

                    var dr = g1.return_dr("[crm].[GetProductDetailsByQRcode] " + ula.slno + ",'"+ula.barcode+"','"+ula.IsMaster+"'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemWarrantyDetails
                            {
                                ProductID = Convert.ToInt32(dr["ProductID"].ToString()),
                                ProductName = Convert.ToString(dr["ProductName"].ToString()),
                                Color = Convert.ToString(dr["Color"].ToString()),
                                ModelNo = Convert.ToString(dr["ModelNo"].ToString()),
                                ManufactureDate = Convert.ToString(dr["ManufactureDate"].ToString()),
                                MonthOfManufacture = Convert.ToString(dr["MonthOfManufacture"].ToString()),
                                VendorName = Convert.ToString(dr["VendorName"].ToString()),
                                LabelGenerationDate = Convert.ToString(dr["LabelGenerationDate"].ToString()),
                                Warrantydate = Convert.ToString(dr["Warrantydate"].ToString()),
                                Qty = Convert.ToString(dr["Qty"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ItemWarrantyDetailss
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}