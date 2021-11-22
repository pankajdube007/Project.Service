using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Serialization;
using System.Text;
using Project.Service.Filters;
using Project.Service.Models;


namespace Project.Service.Controllers
{
    public class UserValidationPartyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        // [HMACAuthentication]
        [Route("api/ValidateUserDealer")]

        public HttpResponseMessage Validation(ActionResultParty ar)
        {
            Common cm = new Common();
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            if (ModelState.IsValid)
            {

                try
                {
                    DataConection g1 = new DataConection();
                    var dt = g1.return_dt("exec App_validdcr '" + ar.CIN + "','" + ar.Password + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string data1, T;
                        int uid = Convert.ToInt32(dt.Rows[0]["SlNo"]);
                        string lognm = Convert.ToString(dt.Rows[0]["usernm"]);
                        string name = Convert.ToString(dt.Rows[0]["name"]);
                        string stat = Convert.ToString(dt.Rows[0]["stat"]);
                        int row = g1.ExecDB("exec App_dcrlogindetladd " + ar.CIN + ",'" + lognm + "','" + name + "','" + ar.Deviceid + "','" + ar.Pushwooshid + "','" + stat + "','" + true + "'");
                        if (row > 0)
                        {
                            var dt1 = g1.return_dt(string.Format("exec dcrlogindetlshow '{0}'", ar.CIN));
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
                                    message = "",
                                    servertime = DateTime.Now.ToString(),
                                    data = new UserValidation.Users
                                    {
                                        userlogid = Convert.ToInt32(dt1.Rows[0]["userlogid"].ToString()),
                                        userlognm = Convert.ToString(dt1.Rows[0]["userlognm"].ToString()),
                                        usernm = Convert.ToString(dt1.Rows[0]["usernm"].ToString()),
                                        status = Convert.ToString(dt1.Rows[0]["status"].ToString()),
                                        issuccess = Convert.ToBoolean(dt1.Rows[0]["issuccess"].ToString()),
                                        isblock = Convert.ToBoolean(dt1.Rows[0]["isblock"].ToString()),
                                        lastsynclead = Convert.ToString(dt1.Rows[0]["lastsyncdt"].ToString()),
                                        // servertime = Convert.ToString(dt1.Rows[0]["createdt"].ToString()),
                                        uniquekey = Convert.ToBase64String(secret),
                                        Usercat = Convert.ToString(dt1.Rows[0]["usercat"].ToString())
                                    },
                                });
                                data1 = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                //response.Content.Headers.Add("content-type", "application/json");
                                response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                return response;
                            }
                            else
                            {
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again"), Encoding.UTF8, "application/json");
                                //response.Content.Headers.Add("content-type", "application/json");
                                return response;
                            }
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again 2"), Encoding.UTF8, "application/json");
                            //response.Content.Headers.Add("content-type", "application/json");
                            return response;
                        }
                    }
                    else
                    {
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