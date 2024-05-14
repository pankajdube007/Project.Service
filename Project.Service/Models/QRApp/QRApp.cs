using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;


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



namespace Project.Service.Models.QRApp
{
    public class LoginValidate
    {
        public String type { get; set; }
        public String username { get; set; }
        public String password { get; set; }
    }


    public class MasterDetails
    {
        public String userid { get; set; }
        public String useremail { get; set; }
    }



    public class GetVendorDetails
    {
        public String branchid { get; set; }
    }


    public class GetPurInDetails
    {
        public String vendorid { get; set; }
    }


    public class GetPartyDetails
    {
        public String branchid { get; set; }
    }


    public class GetDCDetails
    {
        public String partyid { get; set; }
        public String catid { get; set; }
    }


    public class GetWarehouseDetails
    {
        public String branchid { get; set; }

    }


    public class GetDivisionDetails
    {
        public String userid { get; set; }
    }


    public class GetProductDetails
    {
        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public String divisionid { get; set; }
    }


    public class GetBinDetails
    {
        public String branchid { get; set; }
        public String warehouseid { get; set; }
    }



    public class SearchDetails
    {
        public String referenceid { get; set; }
        public String type { get; set; }
    }

    public class QRDetails
    {
        public String type { get; set; }
        public String qrcode { get; set; }
        public String vendorid { get; set; }
        public String warehouseid { get; set; }
        public String productid { get; set; }
        public String userid { get; set; }
    }

    public class StockDetails
    {
        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public String productid { get; set; }
    }



    public class PostWarehouseDetailslst
    {
        public String userid { get; set; }
        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public List<PostWarehouseDetails> data { get; set; }
    }

    public class PostWarehouseDetails
    {

        public String productid { get; set; }
        public String binid { get; set; }
        public String qrcode { get; set; }
        public String qrtype { get; set; }
        public String qrqty { get; set; }
    }


    public class PostPurchaseINDetailslst
    {
        public String userid { get; set; }
        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public List<PostPurchaseINDetails> data { get; set; }
    }

    public class PostPurchaseINDetails
    {

        public String poid { get; set; }
        public String podid { get; set; }
        public String productid { get; set; }
        public String binid { get; set; }
        public String qrcode { get; set; }
        public String qrtype { get; set; }
        public String qrqty { get; set; }
    }


    public class PostDCDetailslst
    {
        public String userid { get; set; }
        public String branchid { get; set; }
        public String warehouseid { get; set; }

        public List<PostDCDetails> data { get; set; }
    }

    public class PostDCDetails
    {
        public String dcid { get; set; }
        public String dcdid { get; set; }
        public String productid { get; set; }
        public String qrcode { get; set; }
        public String qrtype { get; set; }
        public String qrqty { get; set; }
    }


    public class UserInputs
    {
        private string name;
        public string usernm
        {
            get
            {
                return this.name ?? "";
            }
            set
            {
                this.name = value;
            }
        }
        private string password;
        public string pwd
        {
            get
            {
                return this.password ?? "";
            }
            set
            {
                this.password = value;
            }
        }
        private string track;
        public string ip
        {
            get
            {
                return this.track ?? "";
            }
            set
            {
                this.track = value;
            }
        }
    }

    public class ValidateLoginData
    {
        public string ValidateAsync(UserInputs userLogin)
        {
            
            String ResponseData = "";

            try
            {
                DataConnectionTrans g2 = new DataConnectionTrans();
                var dt = g2.return_dt("exec validuserotherQR '" + userLogin.usernm + "','" + userLogin.pwd + "',''");
                if (dt.Rows.Count > 0)
                {
                   
                    int uid = Convert.ToInt32(dt.Rows[0]["SlNo"]);
                    string lognm = Convert.ToString(dt.Rows[0]["usernm"]);
                    string name = Convert.ToString(dt.Rows[0]["name"]);
                    string stat = Convert.ToString(dt.Rows[0]["stat"]);
                    int row = g2.ExecDB("exec dcrlogindetladd " + uid + ",'" + lognm + "','" + name + "','" + userLogin.ip + "','" + stat + "','" + true + "'");
                    if (row > 0)
                    {
                        var dt1 = g2.return_dt("exec dcrlogindetlshow '" + userLogin.usernm + "'");
                        if (dt1.Rows.Count > 0)
                        {
                            List<ValidationOther.UserInfoOther> user = new List<ValidationOther.UserInfoOther>();
                            Authen auth = new Authen();
                            
                            string EncryptionKey = auth.EncryptionKey;
                            var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                            user.Add(new ValidationOther.UserInfoOther
                            {
                                result = true,
                                message = "",
                                servertime = DateTime.Now.ToString(),
                                data = new ValidationOther.UsersOther
                                {
                                    userlogid = Convert.ToInt32(dt1.Rows[0]["userlogid"].ToString()),
                                    userlognm = Convert.ToString(dt1.Rows[0]["userlognm"].ToString()),
                                    usernm = Convert.ToString(dt1.Rows[0]["usernm"].ToString()),
                                    stateid = Convert.ToInt32(dt1.Rows[0]["stateid"].ToString()),
                                    status = Convert.ToString(dt1.Rows[0]["status"].ToString()),
                                    issuccess = Convert.ToBoolean(dt1.Rows[0]["issuccess"].ToString()),
                                    isblock = Convert.ToBoolean(dt1.Rows[0]["isblock"].ToString()),
                                    uniquekey = Convert.ToBase64String(secret),
                                    usercat = Convert.ToString(dt1.Rows[0]["usercat"].ToString())
                                },
                            });
                            ResponseData = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                            g2.close_connection();
                            return ResponseData;
                        }
                        else
                        {
                            List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                            CommonReturnData objCommonReturnData = new CommonReturnData();
                            objCommonReturnData.message = "Something Wrong Try Again";
                            objCommonReturnData.result = false;
                            objCommonReturnData.servertime = DateTime.Now.ToString();
                            objCommonReturnDatalst.Add(objCommonReturnData);


                            ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            g2.close_connection();
                            return ResponseData;
                        }
                    }
                    else
                    {
                        List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                        CommonReturnData objCommonReturnData = new CommonReturnData();
                        objCommonReturnData.message = "Something Wrong Try Again";
                        objCommonReturnData.result = false;
                        objCommonReturnData.servertime = DateTime.Now.ToString();
                        objCommonReturnDatalst.Add(objCommonReturnData);



                        ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        g2.close_connection();
                        return ResponseData;
                    }
                }
                else
                {
                    List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                    CommonReturnData objCommonReturnData = new CommonReturnData();
                    objCommonReturnData.message = "User Name Or Password Is Incorrect";
                    objCommonReturnData.result = false;
                    objCommonReturnData.servertime = DateTime.Now.ToString();
                    objCommonReturnDatalst.Add(objCommonReturnData);
                    

                    ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    g2.close_connection();
                    return ResponseData;
                }
            }
            catch (Exception ex)
            {
                List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                CommonReturnData objCommonReturnData = new CommonReturnData();
                objCommonReturnData.message = "Something Wrong Try Again";
                objCommonReturnData.result = false;
                objCommonReturnData.servertime = DateTime.Now.ToString();
                objCommonReturnDatalst.Add(objCommonReturnData);

                ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
             
                return ResponseData;
            }
          
        }

      

        public string ValidateVendorAsync(UserInputs userLogin)
        {
            String ResponseData = "";

            try
            {
                DataConnectionTrans g2 = new DataConnectionTrans();
                var dt = g2.return_dt("exec App_validdcr '" + userLogin.usernm + "','" + userLogin.pwd + "','Vendor' ");
                if (dt.Rows.Count > 0)
                {
                    string userid1 = string.Empty;
                    //  int uid = Convert.ToInt32(ar.em);
                    string name = Convert.ToString(dt.Rows[0]["dealnm"]);
                    string stat = Convert.ToString(dt.Rows[0]["stat"]);
                    int row = g2.ExecDB("exec App_dcrlogindetladd '" + userLogin.usernm + "','" + name + "','','','','','','" + stat + "','" + true + "','0.0.0.0','','','Vendor','',''");
                    if (row > 0)
                    {
                        var dt1 = g2.return_dt("exec App_dcrlogindetlshow '" + userLogin.usernm + "','Vendor' ");
                        if (dt1.Rows.Count > 0)
                        {
                            List<UserValidationParty.UserInfo> user = new List<UserValidationParty.UserInfo>();
                            Authen auth = new Authen();
                            string EncryptionKey = auth.EncryptionKey;
                            var secret = auth.EncryptString(EncryptionKey, Convert.ToString(dt1.Rows[0]["uniquekey"].ToString()));
                            user.Add(new UserValidationParty.UserInfo
                            {
                                result = true,
                                message = "",
                                servertime = DateTime.Now.ToString(),
                                data = new UserValidationParty.Users
                                {
                                    userlogid = userLogin.usernm.ToString(),
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
                                    showfanoption = true
                                },
                            });
                            ResponseData = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                           

                            return ResponseData;
                        }
                        else
                        {
                            List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                            CommonReturnData objCommonReturnData = new CommonReturnData();
                            objCommonReturnData.message = "Something Wrong Try Again";
                            objCommonReturnData.result = false;
                            objCommonReturnData.servertime = DateTime.Now.ToString();
                            objCommonReturnDatalst.Add(objCommonReturnData);

                            ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


                            return ResponseData;
                        }
                    }
                    else
                    {
                        List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                        CommonReturnData objCommonReturnData = new CommonReturnData();
                        objCommonReturnData.message = "Something Wrong Try Again";
                        objCommonReturnData.result = false;
                        objCommonReturnData.servertime = DateTime.Now.ToString();
                        objCommonReturnDatalst.Add(objCommonReturnData);

                        ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });



                        return ResponseData;
                    }
                }
                else
                {

                    List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                    CommonReturnData objCommonReturnData = new CommonReturnData();
                    objCommonReturnData.message = "User Name Or Password Is Incorrect";
                    objCommonReturnData.result = false;
                    objCommonReturnData.servertime = DateTime.Now.ToString();
                    objCommonReturnDatalst.Add(objCommonReturnData);

                    ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });



                    return ResponseData;
                }
            }
            catch (Exception ex)
            {

                List<CommonReturnData> objCommonReturnDatalst = new List<CommonReturnData>();
                CommonReturnData objCommonReturnData = new CommonReturnData();
                objCommonReturnData.message = "Something Wrong Try Again";
                objCommonReturnData.result = false;
                objCommonReturnData.servertime = DateTime.Now.ToString();
                objCommonReturnDatalst.Add(objCommonReturnData);

                ResponseData = JsonConvert.SerializeObject(objCommonReturnDatalst, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


                return ResponseData;
            }
            
        }

    }


   

    public class CommonReturnData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public SuccesData data { get; set; }
    }

    public class SuccesData
    {
        public bool data { get; set; }
    }


    public class LogDetails
    {
        public void TraceService(String Requests, String Response, String APIName, String OrderID)
        {
           
        }
    }



    public class BranchDetailsList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<BranchDetails> data { get; set; }
    }


    public class BranchDetails
    {
        public string slno { get; set; }
        public string branchname { get; set; }
        public bool isdefault { get; set; }
    }



    public class PageDetailsList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<PageDetailData> data { get; set; }
    }


    public class PageDetailData
    {
        public string slno { get; set; }
        public string text { get; set; }
        public string typecat { get; set; }

    }

    public class ProductDetailData
    {
        public string slno { get; set; }
        public string productname { get; set; }
        public string stockqty { get; set; }

    }


    public class ProductDetailDataList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<ProductDetailData> data { get; set; }
    }

    public class SearchDetailDataList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<SearchDetailData> data { get; set; }
    }


    public class SearchDetailData
    {
        public string headid { get; set; }
        public string childid { get; set; }
        public string productid { get; set; }
        public string productqty { get; set; }
        public string qrqty { get; set; }
        public bool partialout { get; set; }
        public string productcode { get; set; }
        public string productname { get; set; }
        public string branchid { get; set; }
        public string warehouseid { get; set; }

    }


    public class QRDetailDataList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<QRDetailData> data { get; set; }
    }


    public class QRDetailData
    {
        public string qrtype { get; set; }
        public string vendorid { get; set; }
        public string qrcode { get; set; }
        public string productid { get; set; }
        public string productqty { get; set; }
        public string pqr { get; set; }
        public string iqr { get; set; }
        public string oqr { get; set; }
        public string cqr { get; set; }
        public string poid { get; set; }
        public string isinnerproductsame { get; set; }
        public string productinnerqty { get; set; }

    }



    public class LoginUserResponse
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<LoginResponse> data { get; set; }
    }


    public class LoginResponse
    {
        public string code { get; set; }
        public string mesg { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string useremail { get; set; }
        public string jwt { get; set; }
    }








    public class StockDataList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<StockDataDetails> data { get; set; }
    }


    public class StockDataDetails
    {
        public string productid { get; set; }
        public string branchid { get; set; }
        public string warehouseid { get; set; }
        public string stockqty { get; set; }
        public string productcode { get; set; }
        public string productname { get; set; }
        public string stockqrqty { get; set; }

    }







    public class QRPostDetailsList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<QRPostDetails> data { get; set; }
    }


    public class QRPostDetails
    {
        public string qrcode { get; set; }
    }
}