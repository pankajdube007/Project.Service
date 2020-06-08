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
    public class ValidateCinController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ValidateCIN")]
        public HttpResponseMessage GetAllUserLatLong(ValidateCinAction ula)
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

                    List<ValidateCin> alldcr = new List<ValidateCin>();
                    List<ValidateCins> alldcr1 = new List<ValidateCins>();

                    var dr = g2.return_dr("App_Validatecin '" + ula.CIN + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string RequestNo = GetRandomKey(6);
                            string otp = GetRandomKey(4);
                            int row1 = g2.ExecDB("exec App_AddOTPUservalidation '" + ula.CIN + "','" + RequestNo + "','" + otp + "','"+ula.Category+"'");
                            if (row1 > 0)
                            {
                                //** testing no//

                                //Emailid = "pankajdube007@gmail.com";
                                //Mobileno = "9518957760";
                                //ErrorEmailid = "pankajdube007@gmail.com";

                                //**Live//
                                Emailid = dr["emailid"].ToString();
                                Mobileno = dr["mobile"].ToString();
                                ErrorEmailid = "pankajdube007@gmail.com";


                                if (cm.ValidateEmail(Emailid) == true)
                                {
                                    string Subject = "Goldmedal Mobile Application OTP";
                                    string body = "Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only.";
                                    g2.sendmail(Emailid, body, Subject, "GoldMedal", "File");
                                }

                                if (cm.ValidateMobile(Mobileno) == true)
                                {
                                    // bufData = web.DownloadData("http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=goldmedal&password=sd56jjaa&type=0&dlr=0&destination=" + Mobileno + "&source=GLDMDL&message=Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only." + "&url=KKKK%3E");

                                    // string time= string.Format("{0:hh:mm:ss tt}", DateTime.Now.AddMinutes(5));
                                    string smsbody = string.Empty;
                                    //  smsbody = "%3C%23%3E Your Request No is:" + RequestNo + " and OTP is:" + otp + "  OTP Validate for 5 Minutes Only. XzCfDcMIsxX";

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
                            }
                        }
                        // g1.close_connection();
                        g2.close_connection();
                        alldcr1.Add(new ValidateCins
                        {
                            Email = Emailid,
                            Mobile = Mobileno,
                        });

                        alldcr.Add(new ValidateCin
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
                        response.Content = new StringContent(cm.StatusTime(false, "Data Not available"), Encoding.UTF8, "application/json");

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