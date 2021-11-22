using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class ExpensesAllController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ShowAllExpensesByExecutive")]
        public HttpResponseMessage GetDetails(ListofExpensesAll ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
        
            string data1 = string.Empty;
            if (ula.ExId != 0)
            {
                try
                {
            
                    List<ExpensesAlls> alldcr = new List<ExpensesAlls>();
                   List<ExpensesAll> alldcr1 = new List<ExpensesAll>();
                    

                    var dr = g1.return_dr("showExpensedetails " + ula.ExId + ",'" + ula.FromDate + "','" + ula.ToDate + "','" + Convert.ToBoolean(ula.Hierarchy) + "'");
                 //   var dr1 = g1.return_dt("showExpensedetails " + ula.ExId + ",'" + ula.FromDate + "','" + ula.ToDate + "','" + Convert.ToBoolean(ula.Hierarchy) + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExpensesAll
                            {
                                ExpenseId = Convert.ToInt32(dr["expenseid"]),
                                salesexnm = Convert.ToString(dr["salesexnm"]),
                                orgnm = Convert.ToString(dr["orgnm"]),
                                catnm = Convert.ToString(dr["catnm"]),
                                FromDt = Convert.ToString(dr["FromDt"]),
                                ToDt = Convert.ToString(dr["ToDt"]),
                                ExpenseAmt = Convert.ToString(dr["ExpenseAmt"]),
                                isGstInvoice = Convert.ToInt32(dr["isGstInvoice"]),
                                GstInvoiceAmt = Convert.ToString(dr["GstInvoiceAmt"]),
                                isAdvance = Convert.ToInt32(dr["isAdvance"]),
                                InvoiceImage = getimageurl(Convert.ToString(dr["InvoiceImage"])),
                                Invoicepdf = getimagepdf(Convert.ToString(dr["InvoiceImage"])),
                                Status = Convert.ToString(dr["Status"]),
                                createdt = Convert.ToString(dr["createdt"]),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new ExpensesAlls
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Available !!!!!!!!"), Encoding.UTF8, "application/json");

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

        public string getimageurl(string url)
        {
            string imgurl = string.Empty;
            GoldMedia _goldMedia = new GoldMedia();
            string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
            if (!string.IsNullOrEmpty(url))
            {
                string[] split = url.Split(',');

                foreach (var item in split)
                {
                    if(item.Split('.')[1]!="pdf")
                    {
                        imgurl = imgurl + baseurl + "expense/" + item.ToString() + ",";
                    }

                   
                }
            }
            

            return imgurl.TrimEnd(',');
        }


        public string getimagepdf(string url)
        {
            string imgurl = string.Empty;
            GoldMedia _goldMedia = new GoldMedia();
            string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
            if (!string.IsNullOrEmpty(url))
            {
                string[] split = url.Split(',');

                foreach (var item in split)
                {
                    if (item.Split('.')[1] == "pdf")
                    {
                        imgurl = imgurl + baseurl + "expense/" + item.ToString() + ",";
                    }


                }
            }


            return imgurl.TrimEnd(',');
        }
    }
}