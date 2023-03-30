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
using System.Xml.Linq;
using System.Data;

namespace Project.Service.Controllers
{
    public class AddExecCheckINCheckOutController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddExecCheckINCheckOut")]
        public HttpResponseMessage GetDetails(ListsofExecCheckINCheckOut ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecCheckINCheckOuts> alldcr = new List<ExecCheckINCheckOuts>();
                    List<ExecCheckINCheckOut> alldcr1 = new List<ExecCheckINCheckOut>();
                    string uploadfilename = string.Empty;

                    if (!string.IsNullOrEmpty(ula.Image))
                    {
                        string s = ula.Image.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                        if (s.Length % 4 > 0)
                        {
                            s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                        }
                        byte[] binPdf = Convert.FromBase64String(s);

                        Stream stream = new MemoryStream(binPdf);
                        string FileName = Guid.NewGuid().ToString();
                        string uniquefoldernm = "checkoutimage";
                        var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "application/pdf", false, false, true);
                        uploadfilename = FileName + ".jpg";
                    }

                    string dis = "0";
                    string pastlat="";
                    string pastlan="";
                    if (ula.Type == 1)
                    {
                        var dr1 = g2.return_dt("latlancurpas '" + ula.ExId + "','" + ula.EmpType + "'");
                        if (dr1.Rows.Count > 0)
                        {
                             pastlat = dr1.Rows[0]["pastlat"].ToString();
                             pastlan = dr1.Rows[0]["pastlan"].ToString();
                             dis = GetAddress2(pastlat, pastlan, ula.Lat, ula.Long);
                        }
                    }



                  

                    var dr = g2.return_dt("addexeccheckinout '" + ula.OrgId + "','" + ula.OrgCat + "','" + ula.ExId + "','" + ula.DeviceId + "','" + ula.Lat + "','" + ula.Long + "','"+ula.address+"','" + dis + "'," + ula.Type + "," + ula.IsForceFully + "," + ula.InOuttype + ",'" + ula.InOuttime + "','" + uploadfilename + "','"+ula.MDistance + "','" + ula.EmpType + "','"+pastlat+"','"+ pastlan + "'");
                    string errormsg = string.Empty;

                    if (dr.Rows.Count > 0)
                    {
                        errormsg = dr.Rows[0]["errormsg"].ToString();
                        if (errormsg == "1")
                        {
                            alldcr1.Add(new ExecCheckINCheckOut
                            {
                                CheckInLat = dr.Rows[0]["checkinlat"].ToString(),
                                CheckInLong = dr.Rows[0]["checkinlan"].ToString(),
                                CheckOutLat = dr.Rows[0]["checkoutlat"].ToString(),
                                CheckOutLong = dr.Rows[0]["checkoutlan"].ToString(),                                        
                                orgid ="0",
                                orgcat = "0",
                                orgCate = dr.Rows[0]["orgCate"].ToString(),
                                orgName = dr.Rows[0]["orgName"].ToString(),
                                orgId = dr.Rows[0]["orgId"].ToString(),
                                orgCatId = dr.Rows[0]["orgCatId"].ToString(),
                                checkInTime = dr.Rows[0]["checkintime"].ToString(),
                                checkOutTime = dr.Rows[0]["checkouttime"].ToString(),
                                slno = dr.Rows[0]["slno"].ToString(),
                                distnce = dr.Rows[0]["distnce"].ToString(),

                            });

                            g2.close_connection();
                            alldcr.Add(new ExecCheckINCheckOuts
                            {
                                result = true,
                                message = "Data Sucessfully inserted",
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


                            alldcr1.Add(new ExecCheckINCheckOut
                            {
                                CheckInLat = "0",
                                CheckInLong = "0",
                                CheckOutLat ="0",
                                CheckOutLong = "0",
                                orgid = dr.Rows[0]["orgid"].ToString(),
                                orgcat = dr.Rows[0]["orgcat"].ToString(),
                            });

                            g2.close_connection();
                            alldcr.Add(new ExecCheckINCheckOuts
                            {
                                result = false,
                                message = errormsg,
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Login/Logout failed!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                     response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");
                   // response.Content = new StringContent(cm.StatusTime(false, "Kindly checkin/checkout using same device, that used for Punch In!!!!!!!!"), Encoding.UTF8, "application/json");

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

        private string GetAddress2(string latitude, string longitude, string latitude1, string longitude1)
        {
            DataSet dsResult = new DataSet();
            string locationName = "";
            string url = string.Format("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + latitude + "," + longitude + "&destinations=" + latitude1 + "," + longitude1 + "&sensor=false&key=AIzaSyCuYEQogqF3cTj_f8oj-eM3YabPaF57js4");
            XElement xml = XElement.Load(url);
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {

                    dsResult.ReadXml(reader);
                    locationName = dsResult.Tables["distance"].Rows[0]["value"].ToString();
                }
            }
            return locationName;
        }
    }
}