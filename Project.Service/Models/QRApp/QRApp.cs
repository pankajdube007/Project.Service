using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using RestSharp;

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








    /// <summary>
    /// START CREATE DC DATA
    /// </summary>


    public class GetDCDetail
    {
        public String partyid { get; set; }
    }



    public class DCItemRequest
    {

        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public String partycat { get; set; }
        public String divisionid { get; set; }
        public String socategory { get; set; }
        public String customerid { get; set; }
        public String gsr { get; set; }
        public String quotation { get; set; }

    }




    public class DCItemList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public DCItemDetailData data { get; set; }
    }


    public class DCItemDetailData
    {
        public string responsemesg { get; set; }
        public List<ItemDetailData> data { get; set; }
    }

    public class ItemDetailData
    {
        public string slno { get; set; }
        public string itemname { get; set; }
        public string icode { get; set; }
        public string poqty { get; set; }
        public string stockqty { get; set; }
    }



    public class PageDetailsDCList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<PageDetailDCData> data { get; set; }
    }


    public class PageDetailDCData
    {
        public string slno { get; set; }
        public string name { get; set; }
        public string cinnum { get; set; }
        public string typecat { get; set; }
        public string cdname { get; set; }
        public string dealertype { get; set; }
    }

    public class DivisionDCList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<DivisionDCData> data { get; set; }
    }


    public class DivisionDCData
    {
        public string slno { get; set; }
        public string divisioncode { get; set; }
        public string divisionnm { get; set; }
        public string printnm { get; set; }

    }




    public class WarehouseData
    {
        public string slno { get; set; }
        public string text { get; set; }

    }


    public class WarehouseDataList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<WarehouseData> data { get; set; }
    }



    public class DCCreateList
    {
        public bool result { get; set; }
        public String message { get; set; }
        public DateTime servertime { get; set; }
        public List<DCResponse> data { get; set; }
    }



    public class DCResponse
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }

    public class TaxTypeResponse
    {
        public String Type { get; set; }
        public String SourceState { get; set; }
        public String SourceCountry { get; set; }
        public String DispatchState { get; set; }
        public String DispatchCountry { get; set; }
        public String CST { get; set; }
    }



    public class PostDCCreateDetailslst
    {
        public String userid { get; set; }
        public String branchid { get; set; }
        public String warehouseid { get; set; }
        public String partycatid { get; set; }
        public String divisionid { get; set; }
        public String socategory { get; set; }
        public String partyid { get; set; }
        public String gsr { get; set; }
        public String quotation { get; set; }
        public String remarks { get; set; }
        public String checkedby { get; set; }
        public List<PostDCCreateDetails> data { get; set; }
    }

    public class PostDCCreateDetails
    {
        public String productid { get; set; }
        public String qrcode { get; set; }
        public String qrtype { get; set; }
        public String qrqty { get; set; }
    }





    /// <summary>
    /// END CREATE DC DATA
    /// </summary>




    public class Sapapi
    {
        DataConnectionTrans g1 = new DataConnectionTrans();
        int rows = 0;


        #region create-goods-receipt-stock-transfer
        public string GoodsReceiptStockTransfer(int ReceiptId, string UniqueKey, bool IsReversal, string FormType, string TableName, int UserId, String BranchType, String ColumnName)
        {
            var StatusCode = "";
            var message = "";
            var rowcount = "";
            String Validate = ConfigurationManager.AppSettings["SAPBLOCKSTOCKALLOW"].ToString().Trim().ToUpper();
            if (Validate == "YES")
            {
                if (BranchType.Trim() == "1" || BranchType.Trim() == "0")
                {

                    try
                    {
                        var result = AccessTokan();
                        RestClient client = new RestClient(new Uri(ConfigurationManager.AppSettings["SAP-Url"]) + "erp-to-sap/create-goods-receipt-stock-transfer");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("authorization", "Bearer " + result);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddJsonBody(new { ReceiptId = ReceiptId, UniqueKey = UniqueKey, IsReversal = IsReversal, TableName = FormType, UserId = UserId });
                        IRestResponse response = client.Execute(request);
                        Root myClass = new RestSharp.Deserializers.JsonDeserializer().Deserialize<Root>(response);

                        if (myClass.StatusCode == 200 && myClass.Data.Count > 0)
                        {
                            g1 = new DataConnectionTrans();
                            rows = g1.ExecDB("updatepurchaseinapihit " + ReceiptId + ",'" + FormType + "' ");
                            StatusCode = "200"; //myClass.Data[0].StatusCode.ToString();
                            message = myClass.Data[0].StatusMessage.ToString();
                            rowcount = myClass.Data.Count.ToString();
                        }
                        else
                        {
                            message = myClass.Errors[0].ErrorMsg.ToString();
                            StatusCode = myClass.Errors[0].ErrorCode.ToString();
                            rowcount = myClass.Data.Count.ToString();
                        }
                    }

                    catch (Exception ex)
                    {
                        StatusCode = "-1";
                        message = ex.Message.ToString();
                    }
                }
                else
                {
                    String RequestURL = ConfigurationManager.AppSettings["SAP-Url"].ToString().Trim() + "erp-to-sap/create-goods-receipt-stock-transfer";
                    String RequestData = "{\"ReceiptId\" = \"" + ReceiptId + "\", \"UniqueKey\" = \"" + UniqueKey + "\",\"IsReversal\" = \"" + IsReversal + "\",\"TableName\" = \"" + FormType + "\", \"UserId\" = \"" + UserId + "\"}";

                    InsertAutoAPIHitDelayMode(RequestURL, RequestData, UserId.ToString().Trim(), TableName, ReceiptId.ToString().Trim(), ColumnName);

                    rows = g1.ExecDB("updatepurchaseinapihit " + ReceiptId + ",'" + FormType + "' ");

                    StatusCode = "200";
                    message = "Success";
                    rowcount = "1";
                }

            }
            else
            {
                StatusCode = "200";
                message = "Success";
                rowcount = "1";
            }

            return StatusCode + "~" + message + "~" + rowcount;

        }
        #endregion

        #region AccessTokan
        public string AccessTokan()
        {


            RestClient client = new RestClient(new Uri(ConfigurationManager.AppSettings["SAP-Url"]) + "api/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("username", ConfigurationManager.AppSettings["SAP-username"]);
            request.AddParameter("password", ConfigurationManager.AppSettings["SAP-password"]);
            request.AddParameter("client_id", ConfigurationManager.AppSettings["SAP-client_id"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["SAP-client_secret"]);
            request.AddParameter("grant_type", "password");
            IRestResponse response = client.Execute(request);


            var status = response.ResponseStatus;
            var statusCode = response.StatusCode;
            var content = response.Content;
            if (status.ToString() == "Completed" && statusCode.ToString() == "OK")
            {
                accesstotanoutput _uData = JsonConvert.DeserializeObject<accesstotanoutput>(response.Content);

                return _uData.access_token.ToString();
            }
            else
            {
                return "";

            }



        }
        #endregion AccessTokan

        public bool InsertAutoAPIHitDelayMode(String RequestURL, String RequestData, String UserID, String TableName, String TableSlno, String ColumnName)
        {
            bool data = false;
            DataConnectionTrans g1 = new DataConnectionTrans();
           
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@RequestURI", RequestURL.Trim());
            param[1] = new SqlParameter("@RequestData", RequestData.Trim());
            param[2] = new SqlParameter("@SysUser", UserID.Trim());
            param[3] = new SqlParameter("@TableName", TableName.Trim());
            param[4] = new SqlParameter("@TableSlNo", TableSlno.Trim());
            param[5] = new SqlParameter("@ColumnName", ColumnName.Trim());
            ds = g1.FillDataSet("Usp_InsertGenerateRequestForSAP", param);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                data = true;
            }
            return data;
        }


        public String ValidateLockDateResult(String Slno, String Module, String Date, String DateFormat)
        {
            String result = "";

            DataTable dtValidate = new DataTable();

            if (Date != "")
            {
                if (DateFormat == "1")
                {
                    Date = DateTime.ParseExact(Date, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "2")
                {
                    Date = DateTime.ParseExact(Date, "MM/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "3")
                {
                    Date = DateTime.ParseExact(Date, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "4")
                {
                    Date = DateTime.ParseExact(Date, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "5")
                {
                    Date = DateTime.ParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "6")
                {
                    Date = DateTime.ParseExact(Date, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "7")
                {
                    Date = DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
                else if (DateFormat == "8")
                {
                    Date = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy");
                }
            }


            if (Slno.Trim() == "0")
            {
                dtValidate = g1.return_dt("exec usp_DateLockCheck '" + Module + "','" + Date + "' ");
            }
            else
            {
                dtValidate = g1.return_dt("exec usp_DateLockCheckEdit '" + Module + "','" + Slno + "' ");
            }

            if (dtValidate.Rows.Count > 0)
            {
                result = dtValidate.Rows[0]["Result"].ToString().Trim();

            }
            else
            {
                result = "1";
            }
            return result;
        }


    }

    public class AutoNoGen
    {
        private readonly string finYear = string.Empty;
        DataConnectionTrans g1 = new DataConnectionTrans();
        public AutoNoGen()
        {
            finYear = ConfigurationManager.AppSettings["finYear"];
        }
        public string[] GetAutoNo(int moduleID, int branchID)
        {
            var Str = "123456789";
            string[] rtnStr = null;
            var result = GetAutoNoTable(moduleID, branchID);
            if (result.Rows.Count > 0)
            {
                rtnStr = new string[5];
                var val = Convert.ToString(result.Rows[0]["code"]);
                var digit = Convert.ToInt32(result.Rows[0]["digit"]);
                rtnStr[0] = val + "/" + finYear + "/";
                rtnStr[1] = Convert.ToString(rtnStr[0].Length);
                rtnStr[2] = digit.ToString();
                rtnStr[3] = val + "/" + finYear + "/" + Str.Substring(0, digit);
                rtnStr[4] = Convert.ToString(rtnStr[3].Length);
            }
            return rtnStr;
        }

        public void GetIlligalUser()
        {
            var datetime1 = DateTime.Now.ToString();
            g1.ExecDB("exec IlligalUserAdd '" + HttpContext.Current.Request.Cookies["uid"].Value + "','" + HttpContext.Current.Request.Cookies["brnchname"].Value + "','" + datetime1 + "','" + HttpContext.Current.Request.Url.ToString() + "'," + HttpContext.Current.Request.Cookies["logno"].Value + "");
            g1.ExecDB("exec IlligalUserStatus " + HttpContext.Current.Request.Cookies["uid"].Value);

        }
        private DataTable GetAutoNoTable(int moduleID, int branchID)
        {
            var dt = g1.return_dt(string.Format("exec GetAutoNoByIDs {0},{1}", moduleID, branchID));
            return dt;
        }
    }




    public class ValidateLockDate
    {
        public String Value { get; set; }
    }
    class accesstotanoutput
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string Parameter { get; set; }
        public string HelpUrl { get; set; }
    }

    public class Success
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string TokenKey { get; set; }
    }

    public class Root
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeMessage { get; set; }
        public string Timestamp { get; set; }
        public int Size { get; set; }
        public List<Success> Data { get; set; }
        public List<Error> Errors { get; set; }
    }






}