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
using System.Web.Script.Serialization;

namespace Project.Service.Controllers
{
    public class UserValidationsController : ApiController
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="usernm"></param>
        /// <param name="pwd"></param>
        /// <param name="deviceid"></param>
        /// <param name="pushwooshid"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        // [HMACAuthentication]
        [Route("api/ValidateUser")]
        public HttpResponseMessage Validation(ActionResult ar)
        {
            Common cm = new Common();
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            if (ModelState.IsValid)
            {
                try
                {
                    DataConection g1 = new DataConection();
                    DataConnectionTrans g2 = new DataConnectionTrans();
                    var dt = g1.return_dt("exec validdcr '" + ar.usernm + "','" + ar.pwd + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string data1, T;
                        int uid = Convert.ToInt32(dt.Rows[0]["SlNo"]);
                        string lognm = Convert.ToString(dt.Rows[0]["usernm"]);
                        string name = Convert.ToString(dt.Rows[0]["name"]);
                        string stat = Convert.ToString(dt.Rows[0]["stat"]);
                        int row = g2.ExecDB("exec dcrlogindetladd1 " + uid + ",'" + lognm + "','" + name + "','" + ar.deviceid + "','" + ar.pushwooshid + "','" + stat + "','" + true + "'");
                        if (row > 0)
                        {
                            var dt1 = g1.return_dt(string.Format("exec dcrlogindetlshow '{0}'", ar.usernm));
                            if (dt1.Rows.Count > 0)
                            {
                                List<UserValidation.UserInfo> user = new List<UserValidation.UserInfo>();
                                Authen auth = new Authen();
                                Common com = new Common();
                                string EncryptionKey = auth.EncryptionKey;
                                var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                                user.Add(new UserValidation.UserInfo
                                {
                                    result = true,
                                    message = string.Empty,
                                    servertime = DateTime.Now.ToString(),
                                    data = new UserValidation.Users
                                    {
                                        userlogid = Convert.ToInt32(dt1.Rows[0]["userlogid"].ToString()),
                                        userlognm = Convert.ToString(dt1.Rows[0]["userlognm"].ToString()),
                                        usernm = Convert.ToString(dt1.Rows[0]["usernm"].ToString()),
                                        status = Convert.ToString(dt1.Rows[0]["status"].ToString()),
                                        issuccess = Convert.ToBoolean(dt1.Rows[0]["issuccess"].ToString()),
                                        isblock = Convert.ToBoolean(dt1.Rows[0]["isblock"].ToString()),
                                        workingtime = new UserValidation.Work
                                        {
                                            workingtimeto = "24.00",
                                            workingtimefrom = "00.01"
                                        },
                                        holidaylist = com.Getholiday(),
                                        lastsynclead = Convert.ToString(dt1.Rows[0]["lastsyncdt"].ToString()),
                                        // servertime = Convert.ToString(dt1.Rows[0]["createdt"].ToString()),
                                        uniquekey = Convert.ToBase64String(secret),
                                        Usercat = Convert.ToString(dt1.Rows[0]["usercat"].ToString())
                                    },
                                });

                                g1.close_connection();
                                data1 = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                //response.Content.Headers.Add("content-type", "application/json");
                                response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                return response;
                            }
                            else
                            {
                                g1.close_connection();
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again"), Encoding.UTF8, "application/json");
                                //response.Content.Headers.Add("content-type", "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            g1.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again 2"), Encoding.UTF8, "application/json");
                            //response.Content.Headers.Add("content-type", "application/json");
                            return response;
                        }
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "User Name Or Password Is False"), Encoding.UTF8, "application/json");
                        //response.Content.Headers.Add("content-type", "application/json");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again" + ex.ToString()), Encoding.UTF8, "application/json");
                    //response.Content.Headers.Add("content-type", "application/json");
                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Input Details"), Encoding.UTF8, "application/json");
                //response.Content.Headers.Add("content-type", "application/json");
                return response;
            }
        }
    }
}