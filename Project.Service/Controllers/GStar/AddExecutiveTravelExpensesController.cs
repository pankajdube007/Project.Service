using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class AddExecutiveTravelExpensesController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addExecutiveTravelExpenses")]
        public HttpResponseMessage GetDetails(ListofAddExecTravelExpDetails ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddExecTravelExpDetailsLists> alldcr = new List<AddExecTravelExpDetailsLists>();
                    List<AddExecTravelExpDetailsList> alldcr1 = new List<AddExecTravelExpDetailsList>();
                    string uploadImage = string.Empty;

                    if (ula.ImgBill != "abc")
                    {
                        uploadImage = GetImage(ula.ImgBill, 1);
                    }
                    else
                    {
                        uploadImage = "abc";
                    }

                    var dr = g2.return_dr("dbo.AddExecutiveTravelExpenses '" +
                                       ula.ExId + "','" +
                                       ula.ExpenseNo + "','" +
                                       ula.TravelDate + "','" +
                                       ula.BillNo + "','" +
                                       ula.TravelReqid + "','" +
                                       ula.MerchantCategoryid + "','" +
                                       ula.MerchantTypeid + "','" +
                                       ula.GSTIN + "','" +
                                       ula.GSTType + "','" +
                                       ula.TaxPer + "','" +
                                       ula.Cost + "','" +
                                       ula.CGSTamt + "','" +
                                       ula.SGSTamt + "','" +
                                       ula.IGSTamt + "','" +
                                       ula.CGSTper + "','" +
                                       ula.SGSTper + "','" +
                                       ula.IGSTper + "','" +
                                       ula.RoundOff + "','" +
                                       ula.TotalAmt + "','" +
                                       ula.Description + "','" +
                                       uploadImage + "','" +
                                       ula.EmpIds + "','" +
                                       ula.PaidBy + "','" +
                                       ula.MonthlyReport + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddExecTravelExpDetailsList
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddExecTravelExpDetailsLists
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Add Executive Travel Expenses Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

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



        protected string GetImage(string img, int folderCreation)
        {
            var _goldMedia = new GoldMedia();
            var result = "";
            string uniquefoldernm = "";
            if (folderCreation == 1)
            {
                uniquefoldernm = "addexectravelexpenses";
            }


            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName = Guid.NewGuid().ToString();

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                    false, true);
                result = _goldMedia.MapPathToPublicUrl(uniquefoldernm + '/' + FileName + ".jpg");
            }
            return result;
        }
    }
}