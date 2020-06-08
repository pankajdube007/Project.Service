using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using static Project.Service.Models.ValidateManufacturingUser;

namespace Project.Service.Controllers
{
    public class ValidateManufacturingUserController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        // [HMACAuthentication]
        [Route("api/ValidateManufacturingUser")]
        public HttpResponseMessage ValidationOther(ValidationOtherAction ar)
        {
            Models.Common cm = new Models.Common();
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            if (ModelState.IsValid && ar.category== "Manufacturing")
            {
                try
                {
                    // DataConection g1 = new DataConection();
                    Models.DataConnectionTrans g2 = new Models.DataConnectionTrans();
                    var dt = g2.return_dt("exec validuserothermanufacturing '" + ar.usernm + "','" + ar.pwd + "','"+ar.category+"'");
                    if (dt.Rows.Count > 0)
                    {
                        string data1;
                        int uid = Convert.ToInt32(dt.Rows[0]["SlNo"]);
                        string lognm = Convert.ToString(dt.Rows[0]["usernm"]);
                        string name = Convert.ToString(dt.Rows[0]["name"]);
                        string stat = Convert.ToString(dt.Rows[0]["stat"]);
                        int row = g2.ExecDB("exec dcrlogindetladdManufacturing " + uid + ",'" + lognm + "','" + name + "','" + ar.ip + "','" + stat + "','" + true + "'");
                        //int row = g1.ExecDB("exec dcrlogindetladd1 " + uid + ",'" + lognm + "','" + name + "','" + ar.deviceid + "','" + ar.pushwooshid + "','" + stat + "','" + true + "'");
                        if (row > 0)
                        {
                            var dt1 = g2.return_dt("exec dcrlogindetlManufacturingshow '" + ar.usernm + "'");
                            if (dt1.Rows.Count > 0)
                            {
                                List<UserInfoOther> user = new List<UserInfoOther>();
                                Models.Authen auth = new Models.Authen();
                             
                                string EncryptionKey = auth.EncryptionKey;
                                var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                                user.Add(new UserInfoOther
                                {
                                    result = true,
                                    message = "",
                                    servertime = DateTime.Now.ToString(),
                                    data = new UsersOther
                                    {
                                        userlogid = Convert.ToInt32(dt1.Rows[0]["userlogid"].ToString()),
                                        userlognm = Convert.ToString(dt1.Rows[0]["userlognm"].ToString()),
                                        usernm = Convert.ToString(dt1.Rows[0]["usernm"].ToString()),
                                        stateid = Convert.ToInt32(dt1.Rows[0]["stateid"].ToString()),
                                        status = Convert.ToString(dt1.Rows[0]["status"].ToString()),
                                        issuccess = Convert.ToBoolean(dt1.Rows[0]["issuccess"].ToString()),
                                        isblock = Convert.ToBoolean(dt1.Rows[0]["isblock"].ToString()),
                                        uniquekey = Convert.ToBase64String(secret)
                                    },
                                });
                                data1 = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                return response;
                            }
                            else
                            {
                                g2.close_connection();
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again"), Encoding.UTF8, "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            g2.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again 2"), Encoding.UTF8, "application/json");
                            // response.Content.Headers.Add("content-type","application/json");
                            return response;
                        }
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "User Name Or Password Is Incorrect"), Encoding.UTF8, "application/json");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again" + ex.ToString()), Encoding.UTF8, "application/json");
                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Not a Valid User to Login!!!!!!!"), Encoding.UTF8, "application/json");
                return response;
            }
        }
    }
}