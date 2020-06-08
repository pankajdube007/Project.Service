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
    public class ExpenseUnplannedController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/UnplannedExpensesExecutive")]
        public HttpResponseMessage GetDetails(ListofExpenseUnplanned ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.ExId != 0)
            {
                try
                {
                    string img = string.Empty;
                    List<ExpenseUnplanneds> alldcr = new List<ExpenseUnplanneds>();
                    //List<ExpenseDCRImage> alldcr1 = new List<ExpenseDCRImage>();
                    string uploadfilename = string.Empty;

                    if(ula.InvoiceImage !=null)
                    {
                        var result = JsonConvert.DeserializeObject<List<ExpenseUnplannedImage>>(ula.InvoiceImage.ToString());

                        if (result.Count > 0)
                        {
                            foreach (var item in result)
                            {
                                if (!string.IsNullOrEmpty(item.img))
                                {
                                    img = img + cm.SaveBlogImage(item.img, item.extension, "Expense", Guid.NewGuid().ToString()) + ",";
                                }
                            }
                        }
                    }
                   


                    var dr = g1.return_dr("spInsertUnplannedExpenseMaster " + ula.ExId  + "," + ula.ExpenseType +","+ula.CatId+","+ula.OrgId+ ",'" + ula.FromDate+"','"+ ula.ToDate + "','" + ula.ExpenseAmt + "','" + ula.isGstInvoice + "','" + ula.GstInvoiceAmt + "','" + img.TrimEnd(',') + "',"+ula.IsAdvance+","+ula.ExpenceId + ",'"+ula.Statement+"'");

                    if (dr.HasRows)
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Expense Data "+ula.Statement+"ed"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" ), Encoding.UTF8, "application/json");

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