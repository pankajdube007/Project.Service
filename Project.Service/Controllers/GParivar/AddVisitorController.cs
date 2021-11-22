using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class AddVisitorController : ApiController
    {
        private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/AddVisitorDetails")]
        public HttpResponseMessage GetDetails(ListAddVisitorDetails ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddVisitorDetails> alldcr = new List<AddVisitorDetails>();
                    List<AddVisitorDetail> alldcr1 = new List<AddVisitorDetail>();
                    string uploadVisitorImage = string.Empty;
                    string uploadVisitorCardImage = string.Empty;

                    if (ula.visitorimg != "") uploadVisitorImage = GetImage(ula.visitorimg, 1);
                    if (ula.visitingcardimg != "") uploadVisitorCardImage = GetImage(ula.visitingcardimg, 2);
                    

                    var dr = g2.return_dr("dbo.InsertvisitorDetils '" + ula.ExId + "','" + ula.tyepeofvisitor + "','" + uploadVisitorImage + "','" + ula.fullaname + "','" + ula.Mobile + "','" + ula.emailid + "','" + ula.pincode + "','" + ula.cityid + "','" + ula.Address + "','" + ula.companyname + "','" + ula.concernperson + "','" + ula.designation + "','" + ula.contactno + "','" + ula.email + "','" + ula.pin + "','" + ula.city1 + "','" + ula.Address1 + "','" + ula.Address2 + "','" + uploadVisitorCardImage + "','" + ula.typeofvisitor1 + "','" + ula.FullNamevisitor1 + "','" + ula.mobilevisitor1 + "','" + ula.typeofvisitor2 + "','" + ula.FullNamevisitor2 + "','" + ula.mobilevisitor2 + "','" + ula.leadtype + "','" + ula.followupdatetime + "','" + ula.followupremark + "','" + ula.itemid + "','" + ula.VisitorId + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddVisitorDetail
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddVisitorDetails
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
                        response.Content = new StringContent(cm.StatusTime(false, "Visitor Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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
                uniquefoldernm = "visitorimage";
            }
            else
            {
                uniquefoldernm = "visitorcardimage";
            }

            if (!string.IsNullOrEmpty(img))
            {
                //string s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                //if (s.Length % 4 > 0)
                //{
                //    s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                //}
                //byte[] binPdf = Convert.FromBase64String(s);

                //Stream stream = new MemoryStream(binPdf);
                //string FileName = Guid.NewGuid().ToString();
                ////string uniquefoldernm = "visitorimage";
                //var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "application/pdf", false, false, true);
                //result = FileName + ".jpg";

                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);

                Stream stream = new MemoryStream(binPdf);
                var FileName = "erp" + '-' + Guid.NewGuid();
                //var uniquefoldernm = "crm/crmappdocuments";

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                    false, true);

                result = FileName + ".jpg";
            }

            return result;
        }

    }
}