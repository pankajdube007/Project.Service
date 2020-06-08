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
    public class CustomerdetailsbymobController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getcustomerdetailbymob")]
        public HttpResponseMessage GetDetails(ListCustomerDetailByMob ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CustomerDetailByMobLists> alldcr = new List<CustomerDetailByMobLists>();
                    List<CustomerDetailByMobList> alldcr1 = new List<CustomerDetailByMobList>();

                    var dr = g1.return_dr("GetCustomerDetailsByMobileno'" + ula.CIN + "','" + ula.Cat + "','" + ula.Mobile + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CustomerDetailByMobList
                            {

                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                UserCat = Convert.ToString(dr["UserCat"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                UserName = Convert.ToString(dr["UserName"].ToString()),
                                UserSurname = Convert.ToString(dr["UserSurname"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                DateOfBirth = Convert.ToString(dr["DateOfBirth"].ToString()),
                                Sex = Convert.ToString(dr["Sex"].ToString()),
                                RefCode = Convert.ToString(dr["RefCode"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                Hmaddress = Convert.ToString(dr["Hmaddress"].ToString()),
                                Hmaddress1 = Convert.ToString(dr["Hmaddress1"].ToString()),
                                Hmstate = Convert.ToString(dr["Hmstate"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                Hmdistrict = Convert.ToString(dr["Hmdistrict"].ToString()),
                                Distrctnm = Convert.ToString(dr["Distrctnm"].ToString()),
                                Hmcity = Convert.ToString(dr["Hmcity"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                                Hmpincode = Convert.ToString(dr["Hmpincode"].ToString()),
                                CIN = Convert.ToString(dr["CIN"].ToString()),
                                AddressTypeId = Convert.ToString(dr["AddressTypeId"].ToString()),
                                ShopName = Convert.ToString(dr["ShopName"].ToString()),
                                GstNo = Convert.ToString(dr["GstNo"].ToString()),
                                Deluid = Convert.ToString(dr["Deluid"].ToString()),
                                Gstscan = Convert.ToString(dr["Gstscan"].ToString()),
                                ShopPhoto = Convert.ToString(dr["ShopPhoto"].ToString()),
                                ShopEstCerti = Convert.ToString(dr["ShopEstCerti"].ToString()),
                                Profilephoto = Convert.ToString(dr["Profilephoto"].ToString()),
                                Wrkaddress = Convert.ToString(dr["Wrkaddress"].ToString()),
                                Wrkaddress1 = Convert.ToString(dr["Wrkaddress1"].ToString()),
                                Wrkstate = Convert.ToString(dr["Wrkstate"].ToString()),
                                Wrkdistrict = Convert.ToString(dr["Wrkdistrict"].ToString()),
                                Wrkcity = Convert.ToString(dr["Wrkcity"].ToString()),
                                Wrkpincode = Convert.ToString(dr["Wrkpincode"].ToString()),
                                workAddressTypeId = Convert.ToString(dr["wrkstatenm"].ToString()),
                                wrkstatenm = Convert.ToString(dr["Wrkaddress1"].ToString()),
                                wrkdistrictnm = Convert.ToString(dr["wrkdistrictnm"].ToString()),
                                wrkcitynm = Convert.ToString(dr["wrkcitynm"].ToString()),
                                KycdocumentNo1 = Convert.ToString(dr["KycdocumentNo1"].ToString()),
                                documentimglink1 = Convert.ToString(dr["documentimglink1"].ToString()),
                                KycDocMasterId1 = Convert.ToString(dr["KycDocMasterId1"].ToString()),
                                KycdocumentNo2 = Convert.ToString(dr["KycdocumentNo2"].ToString()),
                                documentimglink2 = Convert.ToString(dr["documentimglink2"].ToString()),
                                KycDocMasterId2 = Convert.ToString(dr["KycDocMasterId2"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),





                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CustomerDetailByMobLists
                        {
                            result = true,
                            message = string.Empty,
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
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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