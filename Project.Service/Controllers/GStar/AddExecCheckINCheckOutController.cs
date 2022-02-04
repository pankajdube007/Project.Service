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

                    var dr = g2.return_dt("addexeccheckinout '" + ula.OrgId + "','" + ula.OrgCat + "','" + ula.ExId + "','" + ula.DeviceId + "','" + ula.Lat + "','" + ula.Long + "','"+ula.address+"'," + ula.Distance + "," + ula.Type + "," + ula.IsForceFully + "," + ula.InOuttype + ",'" + ula.InOuttime + "','" + uploadfilename + "'");
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
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                    response.Content = new StringContent(cm.StatusTime(false, "Kindly checkin/checkout using same device, that used for Punch In!!!!!!!!"), Encoding.UTF8, "application/json");

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