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

namespace Project.Service.Controllers
{
    public class DummySignUpController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getdummysignup")]
        public HttpResponseMessage GetDetails(ActionResultPartyDummy ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Name != "")
            {
                try
                {
                    string data1;

                
                    List<UserValidationPartyDummy.UserInfoDummy> user = new List<UserValidationPartyDummy.UserInfoDummy>();
                 
                    var dt1 = g1.return_dt("dummysignupnew '" + ula.Name + "','" + ula.LastName + "','" + ula.Email + "','" + ula.Password + "'");



                    //if (dr.HasRows)
                    //{
                    //    while (dr.Read())
                    //    {
                    //        alldcr1.Add(new DummySignUpList
                    //        {



                    //             userlogid = Convert.ToString(dr["usernm"].ToString()),
                    //            usernm = Convert.ToString(dr["usernm"].ToString().TrimEnd().TrimStart()),
                    //            email = Convert.ToString(dr["emailid"].ToString()),
                    //            mobile = Convert.ToString(dr["mobile"].ToString()),
                    //            firmname = Convert.ToString(dr["firmnm"].ToString()),
                    //            exname = Convert.ToString(dr["salesexnm"].ToString()),
                    //            exmobile = Convert.ToString(dr["salesmobile"].ToString()),
                    //            exhead = Convert.ToString(dr["salesexheadnm"].ToString()),
                    //            exheadmobile = Convert.ToString(dr["salesexheadmobile"].ToString()),
                    //            gstno = Convert.ToString(dr["gstno"].ToString()),
                    //            slno = Convert.ToInt32(dr["SlNo"].ToString()),
                    //            branchid = Convert.ToInt32(dr["homebranchid"].ToString()),
                    //            branchnm = Convert.ToString(dr["locnm"].ToString()),
                    //            stateid = Convert.ToInt32(dr["stateid"].ToString()),
                    //            status = Convert.ToString(dr["status"].ToString()),
                    //            issuccess = Convert.ToBoolean(dr["issuccess"].ToString()),
                    //            isblock = Convert.ToBoolean(dr["isblock"].ToString()),
                    //            lastsynclead = Convert.ToString(dr["lastsyncdt"].ToString()),
                    //            // servertime = Convert.ToString(dt1.Rows[0]["createdt"].ToString()),
                    //            uniquekey = Convert.ToString(dr["uniquekey"].ToString()),
                    //            Usercat = Convert.ToString(dr["dtype"].ToString()),
                    //            branchadd = Convert.ToString(dr["branchadd"].ToString()),
                    //            branchphone = Convert.ToString(dr["offphone1"].ToString()),
                    //            branchemail = Convert.ToString(dr["branchemail"].ToString()),
                    //            joiningdt = Convert.ToString(dr["joindt"].ToString()),
                    //            dob = Convert.ToString(dr["dob"].ToString()),
                    //            designation = Convert.ToString(dr["designm"].ToString()),
                    //            lstlogin = Convert.ToString(dr["lstlogin"].ToString()),
                    //            workingarea = Convert.ToString(dr["areanm"].ToString()),
                    //            immediatehead = Convert.ToString(dr["immediatehd"].ToString()),
                    //            immediatehdmobile = Convert.ToString(dr["immediatehdmobile"].ToString()),
                    //            module = Convert.ToString(dr["module"].ToString()),
                    //        });
                    //    }
                    //    g1.close_connection();
                    //    alldcr.Add(new DummySignUpLists
                    //    {
                    //        result = true,
                    //        message = string.Empty,
                    //        servertime = DateTime.Now.ToString(),
                    //        data = alldcr1,
                    //    });
                    //    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                    //    return response;
                    //}
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

                        List<UserValidationPartyDummy.UserInfoDummy> userDummy = new List<UserValidationPartyDummy.UserInfoDummy>();
                        Authen auth = new Authen();
                        Common com = new Common();
                        string EncryptionKey = auth.EncryptionKey;
                        var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                        user.Add(new UserValidationPartyDummy.UserInfoDummy
                        {
                            result = true,
                            message = "",
                            servertime = DateTime.Now.ToString(),
                            data = new UserValidationPartyDummy.UsersDummy
                            {
                             
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
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //change by pushkar
                       response.Content = new StringContent(cm.StatusTime(false, "Authentication Failed"), Encoding.UTF8, "application/json");
                        //response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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
    }
}