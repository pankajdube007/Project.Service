using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class ForgetMpinController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ForgetMpin")]
        public HttpResponseMessage GetAllUserLatLong(ForgetMpinAction ula)
        {
            WebClient web = new WebClient();
            byte[] bufData = null;
            string errormsg = string.Empty;
            string Emailid = string.Empty;
            string Mobileno = string.Empty;
            string ErrorEmailid = string.Empty;
            //  DataConection g1 = new DataConection();
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ForgetMpins> alldcr = new List<ForgetMpins>();
                    List<ForgetMpin> alldcr1 = new List<ForgetMpin>();

                    var dr = g2.return_dr("App_Validatecin '" + ula.CIN + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string RequestNo = GetRandomKey(6);
                            string otp = GetRandomKey(4);
                            var dr2 = g2.return_dr("exec ForgetOTPUserAdd '" + ula.CIN + "','"+ula.Category+"','" + RequestNo + "','" + otp + "','" + ula.appid + "','" + ula.deviceId + "'");
                            if (dr2.HasRows)
                            {
                                Emailid = dr["emailid"].ToString();
                                Mobileno = dr["mobile"].ToString();
                                ErrorEmailid = "pankajdube007@gmail.com";
                                // Emailid = "sanchit.goldmedal@gmail.com";
                              //  Emailid = "pankajdube007@gmail.com";
                                //  Mobileno = "8369557767";
                               // Mobileno = "9518957760";

                                if (cm.ValidateEmail(Emailid) == true)
                                {
                                    string Subject = "Goldmedal Mobile Application OTP";
                                    string body = "Your Request No is: " + RequestNo + " and OTP is: " + otp + "  OTP Validate for 5 Minutes Only.";
                                    g2.sendmail(Emailid, body, Subject, "GoldMedal", "File");
                                }

                                if (cm.ValidateMobile(Mobileno) == true)
                                {
                                    string smsbody = string.Empty;
                                    smsbody = "%3C%23%3E Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only. XzCfDcMIsxX";
                                    if (ula.Category == "SalesExecutive")
                                    {
                                        smsbody = "%3C%23%3E Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only. " + ConfigurationManager.AppSettings["Gold.AutoRead.Executive"].ToString().Replace("+", "%2b");
                                    }
                                    else
                                    {
                                        smsbody = "Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only. ";
                                    }


                                    string msgurl = "http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=goldmedal&password=sd56jjaa&type=0&dlr=0&destination=" + Mobileno + "&source=GLDMDL&message=" + smsbody + "&url=KKKK%3E";
                                    bufData = web.DownloadData(msgurl);
                                }

                              

                                if (cm.ValidateMobile(Mobileno) == false && cm.ValidateEmail(Emailid) == false)
                                {
                                    errormsg = "Mobile and Email Id Not Valid,Please Update";
                                }

                                while (dr2.Read())
                                {
                                    alldcr1.Add(new ForgetMpin
                                    {
                                        Email = Emailid,
                                        Mobile = Mobileno,
                                        RequestNo = dr2["RequestNo"].ToString(),
                                        OTP = dr2["OTP"].ToString(),
                                        //  LastPin = dr2["lastpin"].ToString(),
                                    });
                                }
                            }
                        }
                        // g1.close_connection();
                        g2.close_connection();

                        alldcr.Add(new ForgetMpins
                        {
                            result = true,
                            servertime = DateTime.Now.ToString(),
                            message = errormsg,
                            data = alldcr1
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
                        response.Content = new StringContent(cm.StatusTime(false, "Please Provide Valid CIN..!!!!"), Encoding.UTF8, "application/json");

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

        private string GetRandomKey(int len)
        {
            int maxSize = len;
            char[] chars = new char[30];
            string a;
            a = "123456789";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data) { result.Append(chars[b % (chars.Length)]); }
            return result.ToString();
        }
    }
}