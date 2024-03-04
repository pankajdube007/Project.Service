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
    public class DeaalerValidationController : ApiController
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
                    DataConnectionTrans g2 = new DataConnectionTrans();
                    var dt = g2.return_dt("exec App_validdcr '" + ar.CIN + "','" + ar.Password + "','" + ar.Category + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string data1;

                        string userid1 = string.Empty;
                        //  int uid = Convert.ToInt32(ar.em);
                        string name = Convert.ToString(dt.Rows[0]["dealnm"]);
                        string stat = Convert.ToString(dt.Rows[0]["stat"]);
                        int row = g2.ExecDB("exec App_dcrlogindetladd '" + ar.CIN + "','" + name + "','" + ar.Deviceid + "','" + ar.DeviceType + "','" +
                            ar.AppVerion + "','" + ar.OsVersion + "','" + ar.Pushwooshid + "','" + stat + "','" + true + "','" + ar.IP + "','" + ar.Lat + "','" + ar.Long + "','" + ar.Category + "','"+ar.ModalType+"','"+ar.Address+"'");
                        if (row > 0)
                        {
                            var dt1 = g2.return_dt("exec App_dcrlogindetlshow '" + ar.CIN + "','" + ar.Category + "'");
                            if (dt1.Rows.Count > 0)
                            {
                                //if (ar.Category == "Party")
                                //{
                                //    userid1 = Convert.ToString(dt1.Rows[0]["cinnum"].ToString());
                                //}
                                //else if (ar.Category == "Vendor")
                                //{
                                //    userid1 = Convert.ToString(dt1.Rows[0]["email"].ToString());
                                //}
                                //else if (ar.Category == "Internal User")
                                //{
                                //    userid1 = Convert.ToString(dt1.Rows[0]["email"].ToString());
                                //}
                                //else if (ar.Category == "SalesExecutive")
                                //{
                                //    userid1 = Convert.ToString(dt1.Rows[0]["email"].ToString());
                                //}

                                List<UserValidationParty.UserInfo> user = new List<UserValidationParty.UserInfo>();
                                Authen auth = new Authen();
                                Common com = new Common();
                                string EncryptionKey = auth.EncryptionKey;
                                var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                                user.Add(new UserValidationParty.UserInfo
                                {
                                    result = true,
                                    message = "",
                                    servertime = DateTime.Now.ToString(),
                                    data = new UserValidationParty.Users
                                    {
                                        userlogid = ar.CIN.ToString(),
                                        usernm = Convert.ToString(dt1.Rows[0]["usernm"].ToString().TrimEnd().TrimStart()),
                                        email = Convert.ToString(dt1.Rows[0]["emailid"].ToString()),
                                        mobile = Convert.ToString(dt1.Rows[0]["mobile"].ToString()),
                                        firmname = Convert.ToString(dt1.Rows[0]["firmnm"].ToString()),
                                        exname = Convert.ToString(dt1.Rows[0]["salesexnm"].ToString()),
                                        exmobile = Convert.ToString(dt1.Rows[0]["salesmobile"].ToString()),
                                        exhead = Convert.ToString(dt1.Rows[0]["salesexheadnm"].ToString()),
                                        exheadmobile = Convert.ToString(dt1.Rows[0]["salesexheadmobile"].ToString()),
                                        gstno = Convert.ToString(dt1.Rows[0]["gstno"].ToString()),
                                        slno = Convert.ToInt32(dt1.Rows[0]["SlNo"].ToString()),
                                        branchid = Convert.ToInt32(dt1.Rows[0]["homebranchid"].ToString()),
                                        branchnm = Convert.ToString(dt1.Rows[0]["locnm"].ToString()),
                                        stateid = Convert.ToInt32(dt1.Rows[0]["stateid"].ToString()),
                                        status = Convert.ToString(dt1.Rows[0]["status"].ToString()),
                                        issuccess = Convert.ToBoolean(dt1.Rows[0]["issuccess"].ToString()),
                                        isblock = Convert.ToBoolean(dt1.Rows[0]["isblock"].ToString()),
                                        lastsynclead = Convert.ToString(dt1.Rows[0]["lastsyncdt"].ToString()),
                                        // servertime = Convert.ToString(dt1.Rows[0]["createdt"].ToString()),
                                        uniquekey = Convert.ToBase64String(secret),
                                        Usercat = Convert.ToString(dt1.Rows[0]["dtype"].ToString()),
                                        branchadd = Convert.ToString(dt1.Rows[0]["branchadd"].ToString()),
                                        branchphone = Convert.ToString(dt1.Rows[0]["offphone1"].ToString()),
                                        branchemail = Convert.ToString(dt1.Rows[0]["branchemail"].ToString()),
                                        joiningdt = Convert.ToString(dt1.Rows[0]["joindt"].ToString()),
                                        dob = Convert.ToString(dt1.Rows[0]["dob"].ToString()),
                                        designation = Convert.ToString(dt1.Rows[0]["designm"].ToString()),
                                        lstlogin = Convert.ToString(dt1.Rows[0]["lstlogin"].ToString()),
                                        workingarea = Convert.ToString(dt1.Rows[0]["areanm"].ToString()),
                                        immediatehead = Convert.ToString(dt1.Rows[0]["immediatehd"].ToString()),
                                        immediatehdmobile = Convert.ToString(dt1.Rows[0]["immediatehdmobile"].ToString()),
                                        module = Convert.ToString(dt1.Rows[0]["module"].ToString()),
                                        showfanoption=true
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
                            response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again"), Encoding.UTF8, "application/json");
                            //response.Content.Headers.Add("content-type", "application/json");
                            return response;
                        }
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "User Name Or Password Is Incorrect"), Encoding.UTF8, "application/json");
                        //response.Content.Headers.Add("content-type", "application/json");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Something Wrong Try Again"+ex.Message.ToString()), Encoding.UTF8, "application/json");
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