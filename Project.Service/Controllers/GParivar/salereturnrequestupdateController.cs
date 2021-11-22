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
    public class salereturnrequestupdateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/salereturnrequestupdates")]
        public HttpResponseMessage GetDetails(salereturnrequestupdatelist ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<salereturnrequestupdates> alldcr = new List<salereturnrequestupdates>();

                    string Image1 = getimage(ula.Image1, ula.imgchange1);
                    string Image2 = getimage(ula.Image2, ula.imgchange2);
                    string Image3 = getimage(ula.Image3, ula.imgchange3);

                    string requestfromdate = null;

                    if (ula.requestpickupfromdt != "")
                    {
                        string[] st1 = ula.requestpickupfromdt.Split('/');

                        if (st1.Count() > 2)
                        {
                            requestfromdate = st1[0] + "/" + st1[1] + "/" + st1[2];
                        }
                    }

                    string requesttodate = null;

                    if (ula.requestpickupfromdt != "")
                    {
                        string[] st1 = ula.requestpickuptodt.Split('/');

                        if (st1.Count() > 2)
                        {
                            requesttodate = st1[0] + "/" + st1[1] + "/" + st1[2];
                        }
                    }

                    var dr = g2.return_dr("salereturnrequestupdate " + ula.rtype + "," + ula.divid + "," + ula.qty + "," + ula.qtytype + ",'','" + ula.reason
                                                                         + "','" + Image1 + "','" + Image2 + "','" + Image3 + "','" + ula.remark + "','','" + ula.CIN
                                                                         + "','" + DateTime.Now + "','" + requestfromdate + "','" + requesttodate + "',0,0," + 1 + "," + ula.slno);

                    if (dr.HasRows)
                    {
                        g2.close_connection();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Updated successfully."), Encoding.UTF8, "application/json");

                        return response;

                        //alldcr.Add(new salereturnrequestupdates
                        //{
                        //    result = "True",
                        //    message = "Updated successfully",
                        //    servertime = DateTime.Now.ToString(),
                        //    data = null,
                        //});
                        //data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        //response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        //return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Update only allowed for 'Pending For Approval.'"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }

        protected string getimage(string img, bool imgchange)
        {
            GoldMedia _goldMedia = new GoldMedia();
            string result = "";

            if (!string.IsNullOrEmpty(img) && imgchange == true)
            {
                string s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0)
                {
                    s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                }
                byte[] binPdf = Convert.FromBase64String(s);

                Stream stream = new MemoryStream(binPdf);
                string FileName = Guid.NewGuid().ToString();
                string uniquefoldernm = "salereturnrequest";

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false, false, true);

                result = FileName + ".jpg";
            }

            return result;
        }
    }
}