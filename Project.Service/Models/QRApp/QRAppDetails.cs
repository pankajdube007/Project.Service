using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;


namespace Project.Service.Models.QRApp
{
    public class QRAppDetails
    {
        public HttpResponseMessage LoginValidate(LoginValidate objLoginValidate)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objLoginValidate);

            String code = "";
            String mesg = "";
            String userid = "";
            String username = "";
            String useremail = "";



            try
            {
                String UserName = objLoginValidate.username.Trim();
                String Password = objLoginValidate.password.Trim();
                String Type = objLoginValidate.type.Trim();

                UserInputs userLogin = new UserInputs();
                userLogin.usernm = UserName;
                userLogin.pwd = Password;
                userLogin.ip = "0.0.0.0";

                if (Type.Trim().ToUpper() == "ERP")
                {
                    ValidateLoginData objValidateLoginData = new ValidateLoginData();
                    String ResponseData = objValidateLoginData.ValidateAsync(userLogin);
                    dynamic _uData = JsonConvert.DeserializeObject(ResponseData);


                    string resultstatus = _uData[0]["result"];
                    string message = _uData[0]["message"];
                    if (resultstatus.Trim() == "False")
                    {
                        code = "400";
                        mesg = Convert.ToString(_uData[0]["message"]);
                    }
                    else
                    {
                        bool isblock = _uData[0].data["isblock"];
                        bool issuccess = _uData[0].data["issuccess"];
                        if (isblock)
                        {
                            code = "400";
                            mesg = "Oops!! Your A/C has been blocked";
                        }
                        else
                        {
                            code = "200";
                            mesg = "success";
                            userid = Convert.ToString(_uData[0].data["userlogid"]);
                            username = Convert.ToString(_uData[0].data["usernm"]);
                            useremail = Convert.ToString(_uData[0].data["userlognm"]);

                        }
                    }
                }
                else if (Type.Trim().ToUpper() == "VENDOR")
                {
                    ValidateLoginData objValidateLoginData = new ValidateLoginData();
                    String ResponseData = objValidateLoginData.ValidateVendorAsync(userLogin);
                    dynamic _uData = JsonConvert.DeserializeObject(ResponseData);
                    string resultstatus = _uData[0]["result"];
                    string message = _uData[0]["message"];
                    if (resultstatus.Trim() == "False")
                    {
                        code = "400";
                        mesg = Convert.ToString(_uData[0]["message"]);
                    }
                    else
                    {
                        bool isblock = _uData[0].data["isblock"];
                        bool issuccess = _uData[0].data["issuccess"];
                        if (isblock)
                        {
                            code = "400";
                            mesg = "Oops!! Your A/C has been blocked";
                        }
                        else
                        {
                            code = "200";
                            mesg = "success";
                            userid = Convert.ToString(_uData[0].data["slno"]);
                            username = Convert.ToString(_uData[0].data["usernm"]);
                            useremail = Convert.ToString(_uData[0].data["userlognm"]);
                        }
                    }
                }
                else
                {
                    code = "400";
                    mesg = "No Details Found";
                }


            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            String jwt = "";


            LoginUserResponse objLoginUserResponse = new LoginUserResponse();

            if (code == "200")
            {
                objLoginUserResponse.result = true;
                objLoginUserResponse.message = "Success";
            }
            else
            {
                objLoginUserResponse.result = false;
                objLoginUserResponse.message = mesg;
            }
            objLoginUserResponse.servertime = DateTime.Now;

            List<LoginResponse> objLoginResponselst = new List<LoginResponse>();
            LoginResponse objLoginResponse = new LoginResponse();
            objLoginResponse.code = code;
            objLoginResponse.mesg = mesg;
            objLoginResponse.userid = userid;
            objLoginResponse.username = username;
            objLoginResponse.useremail = useremail;
            objLoginResponse.jwt = jwt;
            objLoginResponselst.Add(objLoginResponse);

            objLoginUserResponse.data = objLoginResponselst;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objLoginUserResponse);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "Login", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetBranch(MasterDetails objMasterDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objMasterDetails);

            String code = "";
            String mesg = "";

            BranchDetailsList objBranchDetailsList = new BranchDetailsList();

            try
            {
                String UserID = objMasterDetails.userid.Trim();
                String UserEmail = objMasterDetails.useremail.Trim();


                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@userid", UserEmail);
                ds = objDataAccess.FillDataSet("checklogin", param);
                DataTable dt = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    String OtherBranchID = "0";
                    String hbranchid = dt.Rows[0]["homebranchid"].ToString().Trim();
                    if (Convert.ToString(dt.Rows[0]["usercat"]) == "InternalUser")
                    {
                        OtherBranchID = dt.Rows[0]["OtherReportBranchIds"].ToString().Trim();
                    }
                    else
                    {
                        OtherBranchID = dt.Rows[0]["homebranchid"].ToString().Trim();
                    }

                    objDataAccess = new DataConnectionTrans();
                    ds = new DataSet();
                    param = new SqlParameter[1];
                    param[0] = new SqlParameter("@branchid1", OtherBranchID);
                    ds = objDataAccess.FillDataSet("branchselectlogin", param);
                    DataTable dtBranch = new DataTable();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dtBranch = ds.Tables[0];
                    }


                    if (dtBranch.Rows.Count > 0)
                    {
                        List<BranchDetails> objBranchDetailslstdata = new List<BranchDetails>();
                        foreach (DataRow dr in dtBranch.Rows)
                        {
                            BranchDetails objBranchDetails = new BranchDetails();

                            objBranchDetails.slno = dr["Slno"].ToString().Trim();
                            objBranchDetails.branchname = dr["printnm"].ToString().Trim();
                            if (hbranchid == dr["Slno"].ToString().Trim())
                            {
                                objBranchDetails.isdefault = true;
                            }
                            else
                            {
                                objBranchDetails.isdefault = false;
                            }
                            objBranchDetailslstdata.Add(objBranchDetails);
                        }

                        objBranchDetailsList.data = objBranchDetailslstdata;

                        code = "200";
                        mesg = "Success";

                        if (code == "200")
                        {
                            objBranchDetailsList.result = true;
                            objBranchDetailsList.message = "Success";
                        }
                        else
                        {
                            objBranchDetailsList.result = false;
                            objBranchDetailsList.message = mesg;
                        }
                        objBranchDetailsList.servertime = DateTime.Now;
                    }

                }
                else
                {
                    code = "400";
                    mesg = "Branch not found";
                }
            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objBranchDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "BranchList", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            return resp;
        }

        public HttpResponseMessage GetVendorDetails(GetVendorDetails objGetVendorDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetVendorDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {

                String BranchID = objGetVendorDetails.branchid.Trim();

                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@branchid2", BranchID);
                ds = objDataAccess.FillDataSet("VendorselectActive", param);
                DataTable dtVendor = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtVendor = ds.Tables[0];
                }

                if (dtVendor.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtVendor.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["SlNo"].ToString().Trim();
                        objPageDetailData.text = dr["dealnm"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Vendor Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "VendorDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetPurInDetails(GetPurInDetails objGetPurInDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetPurInDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {
                String VendorID = objGetPurInDetails.vendorid.Trim();


                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@VendorID", VendorID);
                ds = objDataAccess.FillDataSet("GetPurchaseInByVendorIDForQRData", param);
                DataTable dtpurchaseIN = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtpurchaseIN = ds.Tables[0];
                }

                if (dtpurchaseIN.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtpurchaseIN.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["SlNo"].ToString().Trim();
                        objPageDetailData.text = dr["ponum"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Refrence Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PUrchaseInDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetPartyDetails(GetPartyDetails objGetPartyDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetPartyDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {

                String BranchID = objGetPartyDetails.branchid.Trim();


                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@branchid", BranchID);
                ds = objDataAccess.FillDataSet("getpartyfordcForQRScan", param);
                DataTable dtDCDetails = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtDCDetails = ds.Tables[0];
                }



                if (dtDCDetails.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtDCDetails.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.text = dr["cdname"].ToString().Trim();
                        objPageDetailData.typecat = dr["typecat"].ToString().Trim();
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Party Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PartyDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetDCDetails(GetDCDetails objGetDCDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetDCDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {
                String PartyID = objGetDCDetails.partyid.Trim();
                String CatID = objGetDCDetails.catid.Trim();


                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@typeofparty", CatID);
                param[1] = new SqlParameter("@partyid", PartyID);
                ds = objDataAccess.FillDataSet("GetDCByPartyForQRData", param);
                DataTable dtDCDetails = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtDCDetails = ds.Tables[0];
                }



                if (dtDCDetails.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtDCDetails.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["SlNo"].ToString().Trim();
                        objPageDetailData.text = dr["dcno"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Refrence Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DCDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetWarehouseDetails(GetWarehouseDetails objGetWarehouseDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetWarehouseDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {

                String BranchID = objGetWarehouseDetails.branchid.Trim();

                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BranchID", BranchID);
                ds = objDataAccess.FillDataSet("GetWarehouseBranchWiseForApp", param);
                DataTable dtWarehouse = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtWarehouse = ds.Tables[0];
                }


                if (dtWarehouse.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtWarehouse.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.text = dr["printnm"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Warehouse Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "WarehouseDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetDivisionDetails(GetDivisionDetails objGetDivisionDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetDivisionDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {

                String UserID = objGetDivisionDetails.userid.Trim();

                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@userid", UserID);
                ds = objDataAccess.FillDataSet("GetVendorDetailsForQRAddInStock", param);
                DataTable dtDivision = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtDivision = ds.Tables[2];
                }

                if (dtDivision.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtDivision.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.text = dr["printnm"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Division Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DivisionDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetProductDetails(GetProductDetails objGetProductDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetProductDetails);

            String code = "";
            String mesg = "";

            ProductDetailDataList objPageDetailsList = new ProductDetailDataList();

            try
            {

                String BranchID = objGetProductDetails.branchid.Trim();
                String WarehouseID = objGetProductDetails.warehouseid.Trim();
                String DivisionID = objGetProductDetails.divisionid.Trim();

                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@WarehouseID", WarehouseID);
                param[1] = new SqlParameter("@BranchID", BranchID);
                param[2] = new SqlParameter("@DivisionID", DivisionID);
                ds = objDataAccess.FillDataSet("GetWarehouseDataWithProduct", param);
                DataTable dtProduct = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtProduct = ds.Tables[0];
                }

                if (dtProduct.Rows.Count > 0)
                {
                    List<ProductDetailData> objProductDetailDatalst = new List<ProductDetailData>();
                    foreach (DataRow dr in dtProduct.Rows)
                    {
                        ProductDetailData objProductDetailData = new ProductDetailData();
                        objProductDetailData.slno = dr["slno"].ToString().Trim();
                        objProductDetailData.productname = dr["ProductName"].ToString().Trim();
                        objProductDetailData.stockqty = dr["StockQty"].ToString().Trim();
                        objProductDetailDatalst.Add(objProductDetailData);
                    }

                    objPageDetailsList.data = objProductDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Product Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "ProductDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetBinDetails(GetBinDetails objGetBinDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetBinDetails);

            String code = "";
            String mesg = "";

            PageDetailsList objPageDetailsList = new PageDetailsList();

            try
            {

                String BranchID = objGetBinDetails.branchid.Trim();
                String WarehouseID = objGetBinDetails.warehouseid.Trim();




                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@WarehouseID", WarehouseID);
                param[1] = new SqlParameter("@BranchID", BranchID);
                ds = objDataAccess.FillDataSet("GetWarehouseData", param);
                DataTable dtwarehouseBin = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtwarehouseBin = ds.Tables[0];
                }

                if (dtwarehouseBin.Rows.Count > 0)
                {
                    List<PageDetailData> objPageDetailDatalst = new List<PageDetailData>();
                    foreach (DataRow dr in dtwarehouseBin.Rows)
                    {
                        PageDetailData objPageDetailData = new PageDetailData();
                        objPageDetailData.slno = dr["ID"].ToString().Trim();
                        objPageDetailData.text = dr["TextData"].ToString().Trim();
                        objPageDetailData.typecat = "0";
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Warehouse Bin Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "BINDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage SearchDetails(SearchDetails objSearchDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objSearchDetails);

            String code = "";
            String mesg = "";

            SearchDetailDataList objPageDetailsList = new SearchDetailDataList();

            try
            {
                String ReferenceID = objSearchDetails.referenceid.Trim();
                String Type = objSearchDetails.type.Trim();

                if (Type.ToUpper() == "PURIN")
                {

                    DataConnectionTrans objDataAccess = new DataConnectionTrans();
                    DataSet ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@poid", ReferenceID);
                    ds = objDataAccess.FillDataSet("GetPurchaseInChildQRDatascan", param);
                    DataTable dtVendor = new DataTable();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dtVendor = ds.Tables[0];
                    }


                    if (dtVendor.Rows.Count > 0)
                    {
                        List<SearchDetailData> objSearchDetailDatalst = new List<SearchDetailData>();
                        foreach (DataRow dr in dtVendor.Rows)
                        {
                            SearchDetailData objSearchDetailData = new SearchDetailData();
                            objSearchDetailData.headid = dr["poid"].ToString().Trim();
                            objSearchDetailData.childid = dr["pochildid"].ToString().Trim();
                            objSearchDetailData.productid = dr["itemid"].ToString().Trim();
                            objSearchDetailData.productqty = dr["approvqty"].ToString().Trim();
                            objSearchDetailData.qrqty = dr["QRQty"].ToString().Trim();
                            objSearchDetailData.partialout = Convert.ToBoolean(dr["PartialOut"].ToString().Trim());
                            objSearchDetailData.productcode = dr["ProductCode"].ToString().Trim();
                            objSearchDetailData.productname = dr["ProductCode1"].ToString().Trim();
                            objSearchDetailData.branchid = dr["BranchID"].ToString().Trim();
                            objSearchDetailData.warehouseid = dr["WarehouseID"].ToString().Trim();

                            objSearchDetailDatalst.Add(objSearchDetailData);
                        }

                        objPageDetailsList.data = objSearchDetailDatalst;
                        code = "200";
                        mesg = "Success";
                    }
                    else
                    {
                        code = "400";
                        mesg = "Search details not found";
                    }
                }
                else if (Type.ToUpper() == "DC")
                {

                    DataConnectionTrans objDataAccess = new DataConnectionTrans();
                    DataSet ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@dcid", ReferenceID);
                    ds = objDataAccess.FillDataSet("GetDCChildQRDatascan", param);
                    DataTable dtDCDetails = new DataTable();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dtDCDetails = ds.Tables[0];
                    }


                    if (dtDCDetails.Rows.Count > 0)
                    {
                        List<SearchDetailData> objSearchDetailDatalst = new List<SearchDetailData>();
                        foreach (DataRow dr in dtDCDetails.Rows)
                        {
                            SearchDetailData objSearchDetailData = new SearchDetailData();
                            objSearchDetailData.headid = dr["dcid"].ToString().Trim();
                            objSearchDetailData.childid = dr["dcchildid"].ToString().Trim();
                            objSearchDetailData.productid = dr["itemid"].ToString().Trim();
                            objSearchDetailData.productqty = dr["itemqty"].ToString().Trim();
                            objSearchDetailData.qrqty = dr["QRQty"].ToString().Trim();
                            objSearchDetailData.partialout = Convert.ToBoolean(dr["PartialOut"].ToString().Trim());
                            objSearchDetailData.productcode = dr["ProductCode"].ToString().Trim();
                            objSearchDetailData.productname = dr["ProductCode1"].ToString().Trim();
                            objSearchDetailData.branchid = dr["BranchID"].ToString().Trim();
                            objSearchDetailData.warehouseid = dr["WarehouseID"].ToString().Trim();

                            objSearchDetailDatalst.Add(objSearchDetailData);
                        }

                        objPageDetailsList.data = objSearchDetailDatalst;
                        code = "200";
                        mesg = "Success";

                    }
                    else
                    {
                        code = "400";
                        mesg = "Search details not found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "Search details not found";
                }


            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;


            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "SearchDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            return resp;
        }

        public HttpResponseMessage QRDetails(QRDetails objQRDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objQRDetails);

            String code = "";
            String mesg = "";

            QRDetailDataList objQRDetailDataList = new QRDetailDataList();

            try
            {
                String QRCode = objQRDetails.qrcode.Trim();
                String Type = objQRDetails.type.Trim();
                String VendorID = objQRDetails.vendorid.Trim();
                String WarehouseID = objQRDetails.warehouseid.Trim();
                String ProductID = objQRDetails.productid.Trim();
                String UserID = objQRDetails.userid.Trim();

                if (ProductID == "")
                {
                    ProductID = "0";
                }

                if (WarehouseID == "")
                {
                    WarehouseID = "0";
                }


                if (VendorID == "")
                {
                    VendorID = "0";
                }

                if (QRCode.Trim() != "")
                {
                    QRCode = QRCode.ToUpper();
                    if (QRCode.Contains("HTTP"))
                    {
                        Uri myUri = new Uri(QRCode);
                        if (myUri.Query.Trim().Contains("QRCODE="))
                        {
                            QRCode = HttpUtility.ParseQueryString(myUri.Query).Get("QRCODE").Trim();
                        }
                        else if (myUri.Query.Trim().Contains("Q="))
                        {
                            if (myUri.Query.Trim().Contains("T="))
                            {
                                QRCode = HttpUtility.ParseQueryString(myUri.Query).Get("Q").Trim() + "#" + HttpUtility.ParseQueryString(myUri.Query).Get("T").Trim() + "#" + HttpUtility.ParseQueryString(myUri.Query).Get("K").Trim();
                            }
                            else if (myUri.ToString().Trim().Contains("#"))
                            {
                                QRCode = HttpUtility.ParseQueryString(myUri.Query + myUri.Fragment).Get("Q").Trim();
                            }
                            else
                            {
                                QRCode = HttpUtility.ParseQueryString(myUri.Query).Get("Q").Trim();
                            }
                        }
                    }



                    if (Type.ToUpper() == "PURIN")
                    {
                        DataConnectionTrans objDataAccess = new DataConnectionTrans();
                        DataSet ds = new DataSet();
                        SqlParameter[] param = new SqlParameter[8];
                        param[0] = new SqlParameter("@VendorID", VendorID);
                        param[1] = new SqlParameter("@ProductID", ProductID);
                        param[2] = new SqlParameter("@QRCODE", QRCode);
                        param[3] = new SqlParameter("@Type", "PURINNW");
                        param[4] = new SqlParameter("@POID", "0");
                        param[5] = new SqlParameter("@WareHouseID", "0");
                        param[6] = new SqlParameter("@DCID", "0");
                        param[7] = new SqlParameter("@UserorPartyID", UserID.Trim());
                        ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);
                        DataTable dtQRDetails = new DataTable();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dtQRDetails = ds.Tables[0];
                        }


                        if (dtQRDetails.Rows.Count > 0)
                        {
                            List<QRDetailData> objQRDetailDatalst = new List<QRDetailData>();
                            foreach (DataRow dr in dtQRDetails.Rows)
                            {
                                QRDetailData objQRDetailData = new QRDetailData();
                                objQRDetailData.qrtype = dr["QRType"].ToString().Trim();
                                objQRDetailData.vendorid = dr["VendorID"].ToString().Trim();
                                objQRDetailData.qrcode = dr["QRCode"].ToString().Trim();
                                objQRDetailData.productid = dr["ProductID"].ToString().Trim();
                                objQRDetailData.productqty = dr["ProductQty"].ToString().Trim();
                                objQRDetailData.pqr = dr["PQR"].ToString().Trim();
                                objQRDetailData.iqr = dr["IQR"].ToString().Trim();
                                objQRDetailData.oqr = dr["OQR"].ToString().Trim();
                                objQRDetailData.cqr = dr["CQR"].ToString().Trim();
                                objQRDetailData.poid = dr["POID"].ToString().Trim();
                                objQRDetailData.isinnerproductsame = dr["IsInnerProductSame"].ToString().Trim();
                                objQRDetailData.productinnerqty = dr["ProductInnerQty"].ToString().Trim();
                                objQRDetailDatalst.Add(objQRDetailData);
                            }

                            objQRDetailDataList.data = objQRDetailDatalst;
                            code = "200";
                            mesg = "Success";
                        }
                        else
                        {
                            String Mesg = "";
                            objDataAccess = new DataConnectionTrans();
                            ds = new DataSet();
                            param = new SqlParameter[8];
                            param[0] = new SqlParameter("@VendorID", "0");
                            param[1] = new SqlParameter("@ProductID", "0");
                            param[2] = new SqlParameter("@QRCODE", QRCode);
                            param[3] = new SqlParameter("@Type", "STATUS");
                            param[4] = new SqlParameter("@POID", "0");
                            param[5] = new SqlParameter("@WareHouseID", "0");
                            param[6] = new SqlParameter("@DCID", "0");
                            param[7] = new SqlParameter("@UserorPartyID", "0");
                            ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);
                            DataTable dtQRStatusDetails = new DataTable();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dtQRStatusDetails = ds.Tables[0];
                            }




                            if (dtQRStatusDetails.Rows.Count > 0)
                            {
                                Mesg = "QR Code already added in stock current status is " + dtQRStatusDetails.Rows[0]["Status"].ToString();
                            }
                            else
                            {
                                Mesg = "Please scan valid QR Code or QR Code not found in Database. please contact Administrator";
                            }

                            code = "400";
                            mesg = Mesg;
                        }
                    }
                    else if (Type.ToUpper() == "DC")
                    {

                        DataConnectionTrans objDataAccess = new DataConnectionTrans();
                        DataSet ds = new DataSet();
                        SqlParameter[] param = new SqlParameter[8];
                        param[0] = new SqlParameter("@VendorID", "0");
                        param[1] = new SqlParameter("@ProductID", "0");
                        param[2] = new SqlParameter("@QRCODE", QRCode);
                        param[3] = new SqlParameter("@Type", "DC");
                        param[4] = new SqlParameter("@POID", "0");
                        param[5] = new SqlParameter("@WareHouseID", WarehouseID);
                        param[6] = new SqlParameter("@DCID", "0");
                        param[7] = new SqlParameter("@UserorPartyID", UserID.Trim());
                        ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);
                        DataTable dtQRDetails = new DataTable();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dtQRDetails = ds.Tables[0];
                        }



                        if (dtQRDetails.Rows.Count > 0)
                        {
                            List<QRDetailData> objQRDetailDatalst = new List<QRDetailData>();
                            foreach (DataRow dr in dtQRDetails.Rows)
                            {
                                QRDetailData objQRDetailData = new QRDetailData();
                                objQRDetailData.qrtype = dr["QRType"].ToString().Trim();
                                objQRDetailData.vendorid = dr["VendorID"].ToString().Trim();
                                objQRDetailData.qrcode = dr["QRCode"].ToString().Trim();
                                objQRDetailData.productid = dr["ProductID"].ToString().Trim();
                                objQRDetailData.productqty = dr["ProductQty"].ToString().Trim();
                                objQRDetailData.pqr = dr["PQR"].ToString().Trim();
                                objQRDetailData.iqr = dr["IQR"].ToString().Trim();
                                objQRDetailData.oqr = dr["OQR"].ToString().Trim();
                                objQRDetailData.cqr = dr["CQR"].ToString().Trim();
                                objQRDetailData.poid = dr["POID"].ToString().Trim();
                                objQRDetailData.isinnerproductsame = dr["IsInnerProductSame"].ToString().Trim();
                                objQRDetailData.productinnerqty = dr["ProductInnerQty"].ToString().Trim();
                                objQRDetailDatalst.Add(objQRDetailData);
                            }
                            objQRDetailDataList.data = objQRDetailDatalst;
                            code = "200";
                            mesg = "Success";
                        }
                        else
                        {
                            String Mesg = "";

                            objDataAccess = new DataConnectionTrans();
                            ds = new DataSet();
                            param = new SqlParameter[8];
                            param[0] = new SqlParameter("@VendorID", "0");
                            param[1] = new SqlParameter("@ProductID", "0");
                            param[2] = new SqlParameter("@QRCODE", QRCode);
                            param[3] = new SqlParameter("@Type", "STATUS");
                            param[4] = new SqlParameter("@POID", "0");
                            param[5] = new SqlParameter("@WareHouseID", "0");
                            param[6] = new SqlParameter("@DCID", "0");
                            param[7] = new SqlParameter("@UserorPartyID", "0");
                            ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);
                            DataTable dtQRStatusDetails = new DataTable();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dtQRStatusDetails = ds.Tables[0];
                            }




                            if (dtQRStatusDetails.Rows.Count > 0)
                            {
                                Mesg = "QR Code already added in stock current status is " + dtQRStatusDetails.Rows[0]["Status"].ToString();
                            }
                            else
                            {
                                Mesg = "Please scan valid QR Code or QR Code not found in Database. please contact Administrator";
                            }

                            code = "400";
                            mesg = Mesg;
                        }
                    }
                    else if (Type.ToUpper() == "WHIN")
                    {
                        DataConnectionTrans objDataAccess = new DataConnectionTrans();
                        DataSet ds = new DataSet();
                        SqlParameter[] param = new SqlParameter[8];
                        param[0] = new SqlParameter("@VendorID", "0");
                        param[1] = new SqlParameter("@ProductID", ProductID);
                        param[2] = new SqlParameter("@QRCODE", QRCode);
                        param[3] = new SqlParameter("@Type", "STADD");
                        param[4] = new SqlParameter("@POID", "0");
                        param[5] = new SqlParameter("@WareHouseID", WarehouseID);
                        param[6] = new SqlParameter("@DCID", "0");
                        param[7] = new SqlParameter("@UserorPartyID", UserID.Trim());
                        ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);

                        DataTable dtQRDetails = new DataTable();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dtQRDetails = ds.Tables[0];
                        }



                        if (dtQRDetails.Rows.Count > 0)
                        {
                            List<QRDetailData> objQRDetailDatalst = new List<QRDetailData>();
                            foreach (DataRow dr in dtQRDetails.Rows)
                            {
                                QRDetailData objQRDetailData = new QRDetailData();
                                objQRDetailData.qrtype = dr["QRType"].ToString().Trim();
                                objQRDetailData.vendorid = dr["VendorID"].ToString().Trim();
                                objQRDetailData.qrcode = dr["QRCode"].ToString().Trim();
                                objQRDetailData.productid = dr["ProductID"].ToString().Trim();
                                objQRDetailData.productqty = dr["ProductQty"].ToString().Trim();
                                objQRDetailData.pqr = dr["PQR"].ToString().Trim();
                                objQRDetailData.iqr = dr["IQR"].ToString().Trim();
                                objQRDetailData.oqr = dr["OQR"].ToString().Trim();
                                objQRDetailData.cqr = dr["CQR"].ToString().Trim();
                                objQRDetailData.poid = dr["POID"].ToString().Trim();
                                objQRDetailData.isinnerproductsame = dr["IsInnerProductSame"].ToString().Trim();
                                objQRDetailData.productinnerqty = dr["ProductInnerQty"].ToString().Trim();
                                objQRDetailDatalst.Add(objQRDetailData);
                            }
                            objQRDetailDataList.data = objQRDetailDatalst;
                            code = "200";
                            mesg = "Success";
                        }
                        else
                        {
                            String Mesg = "";

                            objDataAccess = new DataConnectionTrans();
                            ds = new DataSet();
                            param = new SqlParameter[8];
                            param[0] = new SqlParameter("@VendorID", "0");
                            param[1] = new SqlParameter("@ProductID", "0");
                            param[2] = new SqlParameter("@QRCODE", QRCode);
                            param[3] = new SqlParameter("@Type", "STATUS");
                            param[4] = new SqlParameter("@POID", "0");
                            param[5] = new SqlParameter("@WareHouseID", "0");
                            param[6] = new SqlParameter("@DCID", "0");
                            param[7] = new SqlParameter("@UserorPartyID", "0");
                            ds = objDataAccess.FillDataSet("GETQRCodeDetailsProductIDWise", param);
                            DataTable dtQRStatusDetails = new DataTable();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dtQRStatusDetails = ds.Tables[0];
                            }



                            if (dtQRStatusDetails.Rows.Count > 0)
                            {
                                Mesg = "QR Code already added in stock current status is " + dtQRStatusDetails.Rows[0]["Status"].ToString();
                            }
                            else
                            {
                                Mesg = "Please scan valid QR Code or QR Code not found in Database. please contact Administrator";
                            }

                            code = "400";
                            mesg = Mesg;
                        }
                    }
                    else
                    {
                        code = "400";
                        mesg = "Search details not found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "QR code not found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objQRDetailDataList.result = true;
                objQRDetailDataList.message = "Success";
            }
            else
            {
                objQRDetailDataList.result = false;
                objQRDetailDataList.message = mesg;
            }
            objQRDetailDataList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objQRDetailDataList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "QRDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetStockDetails(StockDetails objStockDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objStockDetails);

            String code = "";
            String mesg = "";

            StockDataList objStockDataList = new StockDataList();

            try
            {
                String BranchID = objStockDetails.branchid.Trim();
                String WarehouseID = objStockDetails.warehouseid.Trim();
                String ProductID = objStockDetails.productid.Trim();


                if (ProductID == "")
                {
                    ProductID = "0";
                }

                if (WarehouseID == "")
                {
                    WarehouseID = "0";
                }


                if (BranchID == "")
                {
                    BranchID = "0";
                }

          
                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@ProductID", ProductID.Trim());
                param[1] = new SqlParameter("@BranchID", BranchID.Trim());
                param[2] = new SqlParameter("@WarehouseID", WarehouseID.Trim());
                ds = objDataAccess.FillDataSet("GetStockCalItemWise", param);

                DataTable dtQRDetails = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtQRDetails = ds.Tables[0];
                }


                if (dtQRDetails.Rows.Count > 0)
                {
                    List<StockDataDetails> objStockDataDetailslst = new List<StockDataDetails>();
                    foreach (DataRow dr in dtQRDetails.Rows)
                    {
                        StockDataDetails objStockDataDetails = new StockDataDetails();
                        objStockDataDetails.productid = dr["itemid"].ToString().Trim();
                        objStockDataDetails.branchid = dr["branchid"].ToString().Trim();
                        objStockDataDetails.warehouseid = dr["WarehouseID"].ToString().Trim();
                        objStockDataDetails.stockqty = dr["StockQty"].ToString().Trim();
                        objStockDataDetails.productcode = dr["ProductCode"].ToString().Trim();
                        objStockDataDetails.productname = dr["ProductCode1"].ToString().Trim();
                        objStockDataDetails.stockqrqty = dr["StockQRQty"].ToString().Trim();
                        objStockDataDetailslst.Add(objStockDataDetails);
                    }

                    objStockDataList.data = objStockDataDetailslst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "No details found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objStockDataList.result = true;
                objStockDataList.message = "Success";
            }
            else
            {
                objStockDataList.result = false;
                objStockDataList.message = mesg;
            }
            objStockDataList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objStockDataList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "StockDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage PostWarehouseData(PostWarehouseDetailslst objPostWarehouseDetailslst)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objPostWarehouseDetailslst);

            String code = "";
            String mesg = "";

            QRPostDetailsList objQRPostDetailsList = new QRPostDetailsList();

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = new DataTable();
                if (objPostWarehouseDetailslst.data != null)
                {
                    String BranchID = objPostWarehouseDetailslst.branchid.Trim();
                    String WarehouseID = objPostWarehouseDetailslst.warehouseid.Trim();
                    String UserID = objPostWarehouseDetailslst.userid.Trim();

                    dtData = ToDataTable(objPostWarehouseDetailslst.data, BranchID, WarehouseID, UserID);
                }

                if (dtData.Rows.Count > 0)
                {
                    String BranchID = objPostWarehouseDetailslst.branchid.Trim();
                    String WarehouseID = objPostWarehouseDetailslst.warehouseid.Trim();
                    String UserID = objPostWarehouseDetailslst.userid.Trim();


                    String strSessionID = "WH|" + UserID + "|" + BranchID.ToString() + "|" + WarehouseID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");

                    DataColumn newColumn = new DataColumn("SessionID", typeof(System.String));
                    newColumn.DefaultValue = strSessionID.ToString().ToUpper();
                    dtData.Columns.Add(newColumn);

                    newColumn = new DataColumn("CreatedDate", typeof(System.DateTime));
                    newColumn.DefaultValue = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                    dtData.Columns.Add(newColumn);


                   

                    DataConnectionTrans objDataAccess = new DataConnectionTrans();
                    String Data = objDataAccess.BulkInsert(dtData, "TempWINQRPostDetails");

                    DataSet dsData = new DataSet();
                    objDataAccess = new DataConnectionTrans();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@SESSIONID", strSessionID.ToString().ToUpper());
                    param[1] = new SqlParameter("@UserID", UserID.Trim());
                    dsData = objDataAccess.FillDataSet("UpdateWINQRPostDetailsBulkData", param);
                    DataTable dtQRDetails = new DataTable();
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        dtQRDetails = dsData.Tables[0];
                    }


                    if (dtQRDetails.Rows.Count > 0)
                    {
                        List<QRPostDetails> objQRPostDetailslst = new List<QRPostDetails>();
                        foreach (DataRow dr in dtQRDetails.Rows)
                        {
                            QRPostDetails objQRPostDetails = new QRPostDetails();
                            objQRPostDetails.qrcode = dr["QRCode"].ToString().Trim();
                            objQRPostDetailslst.Add(objQRPostDetails);
                        }

                        objQRPostDetailsList.data = objQRPostDetailslst;
                        code = "200";
                        mesg = "Success";
                    }
                    else
                    {
                        code = "400";
                        mesg = "Data not found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "Request Data not found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objQRPostDetailsList.result = true;
                objQRPostDetailsList.message = "Success";
            }
            else
            {
                objQRPostDetailsList.result = false;
                objQRPostDetailsList.message = mesg;
            }
            objQRPostDetailsList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objQRPostDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PostWarehouseData", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage PostPurchaseInData(PostPurchaseINDetailslst objPostPurchaseINDetailslst)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objPostPurchaseINDetailslst);

            String code = "";
            String mesg = "";

            QRPostDetailsList objQRPostDetailsList = new QRPostDetailsList();

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = new DataTable();
                if (objPostPurchaseINDetailslst.data != null)
                {
                    String BranchID = objPostPurchaseINDetailslst.branchid.Trim();
                    String WarehouseID = objPostPurchaseINDetailslst.warehouseid.Trim();
                    String UserID = objPostPurchaseINDetailslst.userid.Trim();

                    dtData = ToDataTable(objPostPurchaseINDetailslst.data, BranchID, WarehouseID, UserID);
                }

                if (dtData.Rows.Count > 0)
                {
                    String BranchID = objPostPurchaseINDetailslst.branchid.Trim();
                    String WarehouseID = objPostPurchaseINDetailslst.warehouseid.Trim();
                    String UserID = objPostPurchaseINDetailslst.userid.Trim();


                    String strSessionID = "PURIN|" + UserID + "|" + BranchID.ToString() + "|" + WarehouseID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");

                    DataColumn newColumn = new DataColumn("SessionID", typeof(System.String));
                    newColumn.DefaultValue = strSessionID.ToString().ToUpper();
                    dtData.Columns.Add(newColumn);

                    newColumn = new DataColumn("CreatedDate", typeof(System.DateTime));
                    newColumn.DefaultValue = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                    dtData.Columns.Add(newColumn);


               

                    DataConnectionTrans objDataAccess = new DataConnectionTrans();
                    String Data = objDataAccess.BulkInsert(dtData, "TempPURINQRPostDetails");

                    DataSet dsData = new DataSet();
                    objDataAccess = new DataConnectionTrans();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@SESSIONID", strSessionID.ToString().ToUpper());
                    param[1] = new SqlParameter("@UserID", UserID.Trim());
                    dsData = objDataAccess.FillDataSet("UpdatePURINQRPostDetailsBulkData", param);
                    DataTable dtQRDetails = new DataTable();
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        dtQRDetails = dsData.Tables[0];
                    }



                    if (dtQRDetails.Rows.Count > 0)
                    {
                        List<QRPostDetails> objQRPostDetailslst = new List<QRPostDetails>();
                        foreach (DataRow dr in dtQRDetails.Rows)
                        {
                            QRPostDetails objQRPostDetails = new QRPostDetails();
                            objQRPostDetails.qrcode = dr["QRCode"].ToString().Trim();
                            objQRPostDetailslst.Add(objQRPostDetails);
                        }

                        objQRPostDetailsList.data = objQRPostDetailslst;
                        code = "200";
                        mesg = "Success";
                    }
                    else
                    {
                        code = "400";
                        mesg = "No Data Found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "Request Data Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objQRPostDetailsList.result = true;
                objQRPostDetailsList.message = "Success";
            }
            else
            {
                objQRPostDetailsList.result = false;
                objQRPostDetailsList.message = mesg;
            }
            objQRPostDetailsList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objQRPostDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PostPurchaseInData", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage PostDCData(PostDCDetailslst objPostDCDetailslst)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objPostDCDetailslst);

            String code = "";
            String mesg = "";

            QRPostDetailsList objQRPostDetailsList = new QRPostDetailsList();

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = new DataTable();
                if (objPostDCDetailslst.data != null)
                {
                    String BranchID = objPostDCDetailslst.branchid.Trim();
                    String WarehouseID = objPostDCDetailslst.warehouseid.Trim();
                    String UserID = objPostDCDetailslst.userid.Trim();

                    dtData = ToDataTable(objPostDCDetailslst.data, BranchID, WarehouseID, UserID);
                }

                if (dtData.Rows.Count > 0)
                {
                    String BranchID = objPostDCDetailslst.branchid.Trim();
                    String WarehouseID = objPostDCDetailslst.warehouseid.Trim();
                    String UserID = objPostDCDetailslst.userid.Trim();


                    String strSessionID = "DC|" + UserID + "|" + BranchID.ToString() + "|" + WarehouseID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");

                    DataColumn newColumn = new DataColumn("SessionID", typeof(System.String));
                    newColumn.DefaultValue = strSessionID.ToString().ToUpper();
                    dtData.Columns.Add(newColumn);

                    newColumn = new DataColumn("CreatedDate", typeof(System.DateTime));
                    newColumn.DefaultValue = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                    dtData.Columns.Add(newColumn);


            

                    DataConnectionTrans objDataAccess = new DataConnectionTrans();
                    String Data = objDataAccess.BulkInsert(dtData, "TempDCQRPostDetails");

                    DataSet dsData = new DataSet();
                    objDataAccess = new DataConnectionTrans();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@SESSIONID", strSessionID.ToString().ToUpper());
                    param[1] = new SqlParameter("@UserID", UserID.Trim());
                    dsData = objDataAccess.FillDataSet("UpdateDCQRPostDetailsBulkData", param);
                    DataTable dtQRDetails = new DataTable();
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        dtQRDetails = dsData.Tables[0];
                    }




                    if (dtQRDetails.Rows.Count > 0)
                    {
                        List<QRPostDetails> objQRPostDetailslst = new List<QRPostDetails>();
                        foreach (DataRow dr in dtQRDetails.Rows)
                        {
                            QRPostDetails objQRPostDetails = new QRPostDetails();
                            objQRPostDetails.qrcode = dr["QRCode"].ToString().Trim();
                            objQRPostDetailslst.Add(objQRPostDetails);
                        }

                        objQRPostDetailsList.data = objQRPostDetailslst;
                        code = "200";
                        mesg = "Success";
                    }
                    else
                    {
                        code = "400";
                        mesg = "No Data Found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "Request Data Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }



            if (code == "200")
            {
                objQRPostDetailsList.result = true;
                objQRPostDetailsList.message = "Success";
            }
            else
            {
                objQRPostDetailsList.result = false;
                objQRPostDetailsList.message = mesg;
            }
            objQRPostDetailsList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objQRPostDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PostDCData", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }








        public HttpResponseMessage GetDCWarehouseDetails(GetPartyDetails objGetPartyDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetPartyDetails);

            String code = "";
            String mesg = "";

            WarehouseDataList objWarehouseDataList = new WarehouseDataList();

            try
            {
                String BranchID = objGetPartyDetails.branchid.Trim();

                DataTable dtVendor = new DataTable();
                DataConnectionTrans g1 = new DataConnectionTrans();
                dtVendor = g1.return_dt("exec GetBillingWarehouse " + BranchID.Trim());
                if (dtVendor.Rows.Count > 0)
                {
                    List<WarehouseData> objPageDetailDatalst = new List<WarehouseData>();
                    foreach (DataRow dr in dtVendor.Rows)
                    {
                        WarehouseData objPageDetailData = new WarehouseData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.text = dr["locnm"].ToString().Trim();
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objWarehouseDataList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Party Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objWarehouseDataList.result = true;
                objWarehouseDataList.message = "Success";
            }
            else
            {
                objWarehouseDataList.result = false;
                objWarehouseDataList.message = mesg;
            }
            objWarehouseDataList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objWarehouseDataList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DCWarehouseDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetDCPartyDetails(GetPartyDetails objGetPartyDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetPartyDetails);

            String code = "";
            String mesg = "";

            PageDetailsDCList objPageDetailsList = new PageDetailsDCList();

            try
            {

                String BranchID = objGetPartyDetails.branchid.Trim();

                DataTable dtVendor = new DataTable();
                DataConnectionTrans g1 = new DataConnectionTrans();
                dtVendor = g1.return_dt("exec getpartyfordcfromqrapp " + BranchID.Trim());
                if (dtVendor.Rows.Count > 0)
                {
                    List<PageDetailDCData> objPageDetailDatalst = new List<PageDetailDCData>();
                    foreach (DataRow dr in dtVendor.Rows)
                    {
                        PageDetailDCData objPageDetailData = new PageDetailDCData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.cinnum = dr["cinnum"].ToString().Trim();
                        objPageDetailData.name = dr["name"].ToString().Trim();
                        objPageDetailData.typecat = dr["typecat"].ToString().Trim();
                        objPageDetailData.cdname = dr["cdname"].ToString().Trim();
                        objPageDetailData.dealertype = dr["dealertype"].ToString().Trim();
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Party Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DCPartyDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetDCDivisionDetails(GetPartyDetails objGetPartyDetails)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetPartyDetails);

            String code = "";
            String mesg = "";

            DivisionDCList objPageDetailsList = new DivisionDCList();

            try
            {

                String BranchID = objGetPartyDetails.branchid.Trim();

                DataTable dtVendor = new DataTable();
                DataConnectionTrans g1 = new DataConnectionTrans();
                dtVendor = g1.return_dt("exec divisionselectdc " + BranchID.Trim());
                if (dtVendor.Rows.Count > 0)
                {
                    List<DivisionDCData> objPageDetailDatalst = new List<DivisionDCData>();
                    foreach (DataRow dr in dtVendor.Rows)
                    {
                        DivisionDCData objPageDetailData = new DivisionDCData();
                        objPageDetailData.slno = dr["slno"].ToString().Trim();
                        objPageDetailData.divisioncode = dr["divisioncode"].ToString().Trim();
                        objPageDetailData.divisionnm = dr["divisionnm"].ToString().Trim();
                        objPageDetailData.printnm = dr["printnm"].ToString().Trim();
                        objPageDetailDatalst.Add(objPageDetailData);
                    }

                    objPageDetailsList.data = objPageDetailDatalst;
                    code = "200";
                    mesg = "Success";
                }
                else
                {
                    code = "400";
                    mesg = "Party Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objPageDetailsList.result = true;
                objPageDetailsList.message = "Success";
            }
            else
            {
                objPageDetailsList.result = false;
                objPageDetailsList.message = mesg;
            }
            objPageDetailsList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPageDetailsList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DCPartyDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage GetDCItemDetails(DCItemRequest objDCItemRequest)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objDCItemRequest);

            String code = "";
            String mesg = "";

            DCItemList objDCItemList = new DCItemList();
            DCItemDetailData objDCItemDetailData = new DCItemDetailData();

            try
            {
                String Branch = "0";
                String Warehouse = "0";
                String PartyCat = "0";
                String Division = "0";
                String SOCategory = "";
                String CUSID = "0";
                String GSR = "NO";
                String Quotation = "NO";

                if (objDCItemRequest.branchid != null)
                {
                    if (objDCItemRequest.branchid.ToString() != "")
                    {
                        Branch = objDCItemRequest.branchid.ToString().Trim();
                    }
                }

                if (objDCItemRequest.warehouseid != null)
                {
                    if (objDCItemRequest.warehouseid.ToString() != "")
                    {
                        Warehouse = objDCItemRequest.warehouseid.ToString().Trim();
                    }
                }

                if (objDCItemRequest.partycat != null)
                {
                    if (objDCItemRequest.partycat.ToString() != "")
                    {
                        PartyCat = objDCItemRequest.partycat.ToString().Trim();
                    }
                }

                if (objDCItemRequest.divisionid != null)
                {
                    if (objDCItemRequest.divisionid.ToString() != "")
                    {
                        Division = objDCItemRequest.divisionid.ToString().Trim();
                    }
                }


                if (objDCItemRequest.socategory != null)
                {
                    if (objDCItemRequest.socategory.ToString() != "")
                    {
                        SOCategory = objDCItemRequest.socategory.ToString().Trim();
                    }
                }

                if (objDCItemRequest.customerid != null)
                {
                    if (objDCItemRequest.customerid.ToString() != "")
                    {
                        CUSID = objDCItemRequest.customerid.ToString().Trim();
                    }
                }

                if (objDCItemRequest.gsr != null)
                {
                    if (objDCItemRequest.gsr.ToString() != "")
                    {
                        GSR = objDCItemRequest.gsr.ToString().Trim();
                    }
                }

                if (objDCItemRequest.quotation != null)
                {
                    if (objDCItemRequest.quotation.ToString() != "")
                    {
                        Quotation = objDCItemRequest.quotation.ToString().Trim();
                    }
                }


                String Data = "";
                String Rmesg = "";

                if (Warehouse != null && Warehouse != "" && CUSID != "0" && CUSID != "" && SOCategory != "Select" && Division != null && PartyCat != "0")
                {
                    if (Quotation == "")
                    {
                        Quotation = "No";
                    }

                    if (PartyCat != "5")
                    {
                        string returnData = outstandinglimitdayscheck(CUSID, PartyCat);
                        string[] returnDataSplit = returnData.Split(new string[] { "`" }, StringSplitOptions.None);
                        if (returnDataSplit.Length > 0)
                        {
                            Data = returnDataSplit[1].ToString().Trim();
                            Rmesg = returnDataSplit[0].ToString().Trim();
                        }

                        if (Data.Trim() == "")
                        {
                            DataTable dt = new DataTable();
                            dt = itembind(Branch, Warehouse, PartyCat, Division, SOCategory, CUSID, GSR, Quotation);

                            if (dt.Rows.Count > 0)
                            {
                                code = "200";
                                mesg = "";

                                List<ItemDetailData> objItemDetailDatalst = new List<ItemDetailData>();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ItemDetailData objItemDetailData = new ItemDetailData();
                                    objItemDetailData.slno = dr["slno"].ToString().Trim();
                                    objItemDetailData.itemname = dr["itemname"].ToString().Trim();
                                    objItemDetailData.icode = dr["icode"].ToString().Trim();
                                    objItemDetailData.poqty = dr["poqty"].ToString().Trim();
                                    objItemDetailData.stockqty = dr["stockqty"].ToString().Trim();
                                    objItemDetailDatalst.Add(objItemDetailData);
                                }

                                objDCItemDetailData.data = objItemDetailDatalst;
                                objDCItemDetailData.responsemesg = Data;
                                objDCItemList.data = objDCItemDetailData;
                            }
                            else
                            {
                                code = "310";
                                mesg = "No Item Found";
                            }
                        }
                        else
                        {
                            if (Rmesg == "1")
                            {
                                DataTable dt = new DataTable();
                                dt = itembind(Branch, Warehouse, PartyCat, Division, SOCategory, CUSID, GSR, Quotation);

                                if (dt.Rows.Count > 0)
                                {
                                    code = "200";
                                    mesg = "";

                                    List<ItemDetailData> objItemDetailDatalst = new List<ItemDetailData>();
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        ItemDetailData objItemDetailData = new ItemDetailData();
                                        objItemDetailData.slno = dr["slno"].ToString().Trim();
                                        objItemDetailData.itemname = dr["itemname"].ToString().Trim();
                                        objItemDetailData.icode = dr["icode"].ToString().Trim();
                                        objItemDetailData.poqty = dr["poqty"].ToString().Trim();
                                        objItemDetailData.stockqty = dr["stockqty"].ToString().Trim();
                                        objItemDetailDatalst.Add(objItemDetailData);
                                    }

                                    objDCItemDetailData.data = objItemDetailDatalst;
                                    objDCItemDetailData.responsemesg = Data;
                                    objDCItemList.data = objDCItemDetailData;
                                }
                                else
                                {
                                    code = "310";
                                    mesg = "No Item Found";
                                }
                            }
                            else
                            {
                                code = "310";
                                mesg = Data;
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = itembind(Branch, Warehouse, PartyCat, Division, SOCategory, CUSID, GSR, Quotation);

                        if (dt.Rows.Count > 0)
                        {
                            code = "200";
                            mesg = "";

                            List<ItemDetailData> objItemDetailDatalst = new List<ItemDetailData>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                ItemDetailData objItemDetailData = new ItemDetailData();
                                objItemDetailData.slno = dr["slno"].ToString().Trim();
                                objItemDetailData.itemname = dr["itemname"].ToString().Trim();
                                objItemDetailData.icode = dr["icode"].ToString().Trim();
                                objItemDetailData.poqty = dr["poqty"].ToString().Trim();
                                objItemDetailData.stockqty = dr["stockqty"].ToString().Trim();
                                objItemDetailDatalst.Add(objItemDetailData);
                            }

                            objDCItemDetailData.data = objItemDetailDatalst;
                            objDCItemDetailData.responsemesg = Data;
                            objDCItemList.data = objDCItemDetailData;
                        }
                        else
                        {
                            code = "310";
                            mesg = "No Item Found";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }

            if (code == "200")
            {
                objDCItemList.result = true;
                objDCItemList.message = "Success";
            }
            else
            {
                objDCItemList.result = false;
                objDCItemList.message = mesg;
            }
            objDCItemList.servertime = DateTime.Now;

            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objDCItemList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "DCItemDetails", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }

        public HttpResponseMessage PostDCCreateData(PostDCCreateDetailslst objPostDCCreateDetailslst)
        {
            LogDetails objlogDetails = new LogDetails();
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objPostDCCreateDetailslst);

            String code = "";
            String mesg = "";

            DCCreateList objDCCreateList = new DCCreateList();

            try
            {
                DataSet ds = new DataSet();
                DataTable dtData = new DataTable();
                if (objPostDCCreateDetailslst.data != null)
                {
                    String BranchID = objPostDCCreateDetailslst.branchid.Trim();
                    String WarehouseID = objPostDCCreateDetailslst.warehouseid.Trim();
                    String UserID = objPostDCCreateDetailslst.userid.Trim();
                    String PartyCatid = objPostDCCreateDetailslst.partycatid.Trim();
                    String DivisionID = objPostDCCreateDetailslst.divisionid.Trim();
                    String SOCategory = objPostDCCreateDetailslst.socategory.Trim();

                    String Partyid = objPostDCCreateDetailslst.partyid.Trim();
                    String GSR = objPostDCCreateDetailslst.gsr.Trim();
                    String quotation = objPostDCCreateDetailslst.quotation.Trim();
                    String remarks = objPostDCCreateDetailslst.remarks.Trim();
                    String checkedby = objPostDCCreateDetailslst.checkedby.Trim();

                    dtData = ToDataTable(objPostDCCreateDetailslst.data);

                    if (dtData.Rows.Count > 0)
                    {
                        String Prefix = "";
                        String Digit = "";
                        String FullLengthCode = "";
                        String fullCode = "";
                        String LogNo = "050690";
                        AutoNoGen aGen = new AutoNoGen();
                        var result = aGen.GetAutoNo(26, Convert.ToInt32(BranchID));
                        if (result != null)
                        {
                            if (result.Length > 0)
                            {
                                Prefix = result[0];
                                Digit = result[2];
                                fullCode = result[3];
                                FullLengthCode = result[4];
                            }
                        }


                        List<DCResponse> objDCResponselst = new List<DCResponse>();
                        objDCResponselst = SubmitDC(BranchID, WarehouseID, UserID, PartyCatid, DivisionID, SOCategory, Partyid, GSR, Prefix, Digit, FullLengthCode, LogNo, remarks, checkedby, quotation, fullCode, dtData);

                        if (objDCResponselst.Count > 0)
                        {
                            if (objDCResponselst[0].Value == "200")
                            {
                                objDCCreateList.data = objDCResponselst;
                                code = "200";
                                mesg = "Success";
                            }
                            else
                            {
                                code = "400";
                                mesg = objDCResponselst[0].Text;
                                objDCCreateList.data = objDCResponselst;
                            }
                        }
                        else
                        {
                            code = "400";
                            mesg = "Request Data Not Found";
                        }
                    }
                    else
                    {
                        code = "400";
                        mesg = "Request Data Not Found";
                    }
                }
                else
                {
                    code = "400";
                    mesg = "Request Data Not Found";
                }

            }
            catch (Exception ex)
            {
                code = "400";
                mesg = ex.Message.ToString();
            }


            if (code == "200")
            {
                objDCCreateList.result = true;
                objDCCreateList.message = "Success";
            }
            else
            {
                objDCCreateList.result = false;
                objDCCreateList.message = mesg;
            }
            objDCCreateList.servertime = DateTime.Now;



            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objDCCreateList);
            objlogDetails.TraceService(LogJsonString, jsonstrings, "PostDCCreateData", "0");


            var resp = new HttpResponseMessage();


            resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
                StatusCode = HttpStatusCode.OK,
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            return resp;
        }


        public List<DCResponse> SubmitDC(String Branch, String WarehouseID, String UserID, String PartyCat, String Division, String SOCategory, String CUSID, String GSR, String Prefix, String Digit, String FullLengthCode, String LogNo, String Remark, String CheckedBy, String Quotation, String fullCode, DataTable DCChildData)
        {
            List<DCResponse> result = new List<DCResponse>();

            try
            {
                DataTable stockValidation = null;
                int StkFlag = 1;
                int rows = 0;

                TaxTypeResponse TaxType = new TaxTypeResponse();

                String lbid = "0";
                String itemcode3 = "0";
                String DispQty = "0";
                String boxqty2 = "0";
                String boxouterqty = "0";
                String boxshow2 = "0";
                String SessionID = "DCCR|" + UserID + "|" + Branch.ToString() + "|" + WarehouseID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");




                string orderdt = DateTime.Today.ToString();
                string val = string.Empty;
                bool childinsert = true;

                if (Quotation == "")
                {
                    Quotation = "No";
                }

                TaxType = ReCallTaxType(PartyCat, CUSID, Branch);

                var dcchildItemcount = 0;
                var dcchildItemInsertcount = 0;

                if (TaxType.Type != "")
                {
                    DataTable dtDcDetails = new DataTable();
                    dtDcDetails.Columns.Add("itemid");
                    dtDcDetails.Columns.Add("QRQty");
                    dtDcDetails.AcceptChanges();


                    DataTable dtDcQRDetails = new DataTable();
                    dtDcQRDetails.Columns.Add("UserID", typeof(Int64));
                    dtDcQRDetails.Columns.Add("QRCode", typeof(string));
                    dtDcQRDetails.Columns.Add("QRType", typeof(string));
                    dtDcQRDetails.Columns.Add("DCID", typeof(Int64));
                    dtDcQRDetails.Columns.Add("SessionID", typeof(string));
                    dtDcQRDetails.Columns.Add("ProductID", typeof(Int64));
                    dtDcQRDetails.Columns.Add("Qty", typeof(Int64));
                    dtDcQRDetails.Columns.Add("Type", typeof(string));
                    dtDcQRDetails.Columns.Add("PageType", typeof(string));
                    dtDcQRDetails.Columns.Add("CreatedDate", typeof(DateTime));
                    dtDcQRDetails.AcceptChanges();

                    if (DCChildData.Rows.Count > 0)
                    {
                        foreach (DataRow dresc in DCChildData.Rows)
                        {
                            string productid = dresc["productid"].ToString().Trim();
                            string qrcode = dresc["qrcode"].ToString().Trim();
                            string qrtype = dresc["qrtype"].ToString().Trim();
                            string qrqty = dresc["qrqty"].ToString().Trim();


                            DataRow drToAdd = dtDcQRDetails.NewRow();
                            drToAdd["UserID"] = UserID;
                            drToAdd["QRCode"] = qrcode;
                            drToAdd["QRType"] = qrtype + "QR";
                            drToAdd["DCID"] = "0";
                            drToAdd["SessionID"] = SessionID;
                            drToAdd["ProductID"] = productid;
                            drToAdd["Qty"] = Convert.ToInt64(qrqty).ToString();
                            drToAdd["Type"] = "ADD";
                            drToAdd["PageType"] = "DIRECTADD";
                            drToAdd["CreatedDate"] = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");


                            dtDcQRDetails.Rows.Add(drToAdd);
                            dtDcQRDetails.AcceptChanges();

                        }

                      

                        DataConnectionTrans objDataAccess = new DataConnectionTrans();
                        String Data = objDataAccess.BulkInsert(dtDcQRDetails, "DCQRTempMapping");
                    }



                    if (DCChildData.Rows.Count > 0)
                    {
                        foreach (DataRow dresc in DCChildData.Rows)
                        {
                            string productid = dresc["productid"].ToString().Trim();
                            string qrcode = dresc["qrcode"].ToString().Trim();
                            string qrtype = dresc["qrtype"].ToString().Trim();
                            string qrqty = dresc["qrqty"].ToString().Trim();

                            DataRow[] drList = dtDcDetails.Select("itemid = '" + productid + "' ");
                            if (drList.Length > 0)
                            {
                                drList[0]["QRQty"] = Convert.ToDecimal(Convert.ToDecimal(drList[0]["QRQty"]) + Convert.ToDecimal(qrqty)).ToString("0.00");
                                dtDcDetails.AcceptChanges();
                            }
                            else
                            {
                                DataRow drToAdd = dtDcDetails.NewRow();
                                drToAdd["itemid"] = productid;
                                drToAdd["QRQty"] = Convert.ToDecimal(qrqty).ToString("0.00");
                                dtDcDetails.Rows.Add(drToAdd);
                                dtDcDetails.AcceptChanges();
                            }
                        }
                    }

                    DataTable dt4 = new DataTable();
                    dt4 = itembind(Branch, WarehouseID, PartyCat, Division, SOCategory, CUSID, GSR, Quotation);

                    dcchildItemcount = dtDcDetails.Rows.Count;
                    dcchildItemInsertcount = 0;


                    foreach (DataRow drItems in dtDcDetails.Rows)
                    {
                        string ItemidDC = drItems["itemid"].ToString().Trim();
                        string DCQty = drItems["QRQty"].ToString().Trim();


                        DataRow[] drDCData = dt4.Select("SlNo = '" + ItemidDC + "' ");

                        if (drDCData.Length > 0 && childinsert == true)
                        {
                            String Spitem = drDCData[0]["icode"].ToString().Trim();
                            String Spitemtext = drDCData[0]["ItemName"].ToString().Trim();
                            string[] str5 = Spitem.Split(new string[] { "~" }, StringSplitOptions.None);

                            string[] str1 = Spitem.Split('-');
                            if (str1.Count() > 3)
                            {
                                itemcode3 = str1[0].ToString();
                            }
                            else
                            {
                                string[] str4 = Spitem.Split('~');
                                itemcode3 = str4[0].ToString();
                            }


                            DataTable dtrecord = new DataTable();

                            String billqty = DispQty;
                            String SchemePer = str5[12].ToString().Trim();
                            decimal offerprice = 0;
                            decimal disper = 0;

                            if (str5[4] != null)
                            {
                                offerprice = Convert.ToDecimal(str5[4]);
                            }
                            if (str5[5] != null)
                            {
                                disper = Convert.ToDecimal(str5[5]);
                            }

                            string[] str = Spitemtext.Split('-');
                            if (Convert.ToString(str[str.Count() - 1]) == "S")
                            {
                                DataConnectionTrans g1 = new DataConnectionTrans();
                                dtrecord = g1.return_dt("exec  getpopendingitempowisedc3 " + Branch + "," + Convert.ToInt32(PartyCat) + "," + Convert.ToInt32(CUSID) + "," + Division + ",'" + itemcode3 + "','" + SOCategory + "'," + billqty + "," + SchemePer + ",1,'" + offerprice + "','" + disper + "'");
                            }
                            else
                            {
                                DataConnectionTrans g1 = new DataConnectionTrans();
                                dtrecord = g1.return_dt("exec  getpopendingitempowisedc3 " + Branch + "," + Convert.ToInt32(PartyCat) + "," + Convert.ToInt32(CUSID) + "," + Division + ",'" + itemcode3 + "','" + SOCategory + "',0,0,0,'" + offerprice + "','" + disper + "'");
                            }

                            Decimal OrderPendingQty = 0;

                            int pocount = dtrecord.Rows.Count;
                            foreach (DataRow drepo in dtrecord.Rows)
                            {
                                OrderPendingQty += Convert.ToDecimal(drepo["pending"].ToString().Trim());
                            }

                            if (OrderPendingQty < Convert.ToDecimal(DCQty))
                            {
                                DCResponse objSubmitDC2 = new DCResponse();
                                objSubmitDC2.Value = "501";
                                objSubmitDC2.Text = "<b>Item quantity mismatch!!!!.</b><br/> <div style=" + "word-wrap: break-word;" + ">1. Please check the item  <strong>" + Convert.ToString(Spitemtext) + "</strong> with order quantity as <strong>" + Convert.ToString(OrderPendingQty) + "</strong> but scanned quantity <strong>" + Convert.ToString(DCQty) + "</strong></div> ";

                                result.Add(objSubmitDC2);
                                DataConnectionTrans g1 = new DataConnectionTrans();
                                int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);

                                return result;
                            }


                            Decimal OrderDispQty = Convert.ToDecimal(DCQty);
                            DispQty = DCQty;
                            if (Convert.ToDecimal(DCQty) > 0)
                            {
                                if (OrderPendingQty >= OrderDispQty)
                                {
                                    if (lbid == "0")
                                    {
                                        DataConnectionTrans g1 = new DataConnectionTrans();
                                        rows = g1.ExecDB("exec dcchildadd " + Convert.ToInt32(CUSID) + ", " + Convert.ToInt32(PartyCat) + "," + Convert.ToInt32(Division) + ",'" + Convert.ToDateTime(orderdt).ToShortDateString() + "'," + Convert.ToInt32(lbid) + ",'" + itemcode3 + "'," + DispQty + "," + str5[12] + ",'temp'," + boxqty2 + "," + boxouterqty + ",'" + boxshow2 + "'," + str5[4].ToString() + "," + str5[5].ToString() + "," + str5[6].ToString() + "," + str5[7].ToString() + "," + str5[8].ToString() + "," + TaxType.Type + "," + TaxType.SourceState + "," + str5[11].ToString() + "," + UserID + "," + LogNo + ",0,'Insert','" + SOCategory + "','" + GSR + "','" + Quotation + "'," + str5[15].ToString() + "," + Convert.ToDecimal(str5[16]));
                                    }
                                    else
                                    {
                                        DataConnectionTrans g1 = new DataConnectionTrans();
                                        rows = g1.ExecDB("exec dcchildadd " + Convert.ToInt32(CUSID) + "," + Convert.ToInt32(PartyCat) + "," + Convert.ToInt32(Division) + ",'" + Convert.ToDateTime(orderdt).ToShortDateString() + "'," + Convert.ToInt32(lbid) + ",'" + itemcode3 + "'," + DispQty + "," + str5[12] + ",'open'," + boxqty2 + "," + boxouterqty + ",'" + boxshow2 + "'," + str5[4].ToString() + "," + str5[5].ToString() + "," + str5[6].ToString() + "," + str5[7].ToString() + "," + str5[8].ToString() + "," + TaxType.Type + "," + TaxType.SourceState + "," + str5[11].ToString() + "," + UserID + "," + LogNo + ",0,'Insert','" + SOCategory + "','" + GSR + "','" + Quotation + "'," + str5[15].ToString() + "," + Convert.ToDecimal(str5[16]));
                                    }

                                }
                            }

                            if (rows > 0)
                            {

                                childinsert = true;
                                dcchildItemInsertcount += 1;
                                if (dtrecord.Rows.Count > 0)
                                {

                                    foreach (DataRow drpodata in dtrecord.Rows)
                                    {
                                        if (OrderDispQty > 0)
                                        {
                                            decimal poq = Convert.ToDecimal(drpodata["pending"].ToString().Trim());
                                            if (poq >= OrderDispQty)
                                            {
                                                string POID = drpodata["poslno"].ToString().Trim();
                                                DataConnectionTrans g1 = new DataConnectionTrans();
                                                string dcchildid = g1.reterive_val("exec getdcchilid " + LogNo); ;


                                                g1 = new DataConnectionTrans();
                                                rows = g1.ExecDB("exec dcchildpoadd " + Convert.ToInt32(dcchildid) + "," + Convert.ToInt32(POID) + "," + Convert.ToDecimal(OrderDispQty) + "," + Convert.ToDecimal(SchemePer) + ",'temp'," + UserID + "," + LogNo + ",0,'Insert'");
                                                OrderDispQty = OrderDispQty - Convert.ToDecimal(OrderDispQty);
                                            }
                                            else
                                            {
                                                string POID = drpodata["poslno"].ToString().Trim();
                                                DataConnectionTrans g1 = new DataConnectionTrans();
                                                string dcchildid = g1.reterive_val("exec getdcchilid " + LogNo); ;

                                                g1 = new DataConnectionTrans();
                                                rows = g1.ExecDB("exec dcchildpoadd " + Convert.ToInt32(dcchildid) + "," + Convert.ToInt32(POID) + "," + Convert.ToDecimal(poq) + "," + Convert.ToDecimal(SchemePer) + ",'temp'," + UserID + "," + LogNo + ",0,'Insert'");
                                                OrderDispQty = OrderDispQty - poq;
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                childinsert = false;
                            }

                        }

                    }

                }


                DCResponse objSubmitDC = new DCResponse();


                if (childinsert == true && dcchildItemcount == dcchildItemInsertcount && dcchildItemInsertcount != 0)
                {
                    if (Branch != "10" || UserID != "1")
                    {
                        if (lbid == "0")
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            stockValidation = g1.return_dt("exec checkItemCurrPendingNStock_DC " + StkFlag + ",'" + orderdt + "'," + PartyCat + "," + CUSID +
                                "," + Branch + ",'" + SOCategory + "'," + Division + "," + UserID +
                                "," + LogNo + "," + WarehouseID);
                        }
                    }




                    if (Branch != "10" || UserID != "1")
                    {
                        if (stockValidation.Rows[0]["status"].ToString() == "")
                        {
                            DataTable dtData = new DataTable();

                            DataConnectionTrans g1 = new DataConnectionTrans();
                            val = g1.reterive_val(string.Format("exec dcheadaddEditedQRScan '{0}','{1}',{2},{3},{4},'{5}','{6}',{7},{8},{9},'{10}',{11},'{12}','{13}',{14},{15},0,'Insert',{16},'{17}','{18}','{19}','{20}','{21}' ,'{22}' ", fullCode, Prefix, FullLengthCode, Digit, false, SOCategory, Convert.ToDateTime(orderdt).ToShortDateString(), Division, PartyCat, CUSID, "", Branch, Remark, CheckedBy, UserID, LogNo, WarehouseID, "", GSR, true, false, SessionID, Quotation));
                        }
                    }
                    else
                    {

                        DataTable dtData = new DataTable();

                        DataConnectionTrans g1 = new DataConnectionTrans();
                        val = g1.reterive_val(string.Format("exec dcheadaddEditedQRScan '{0}','{1}',{2},{3},{4},'{5}','{6}',{7},{8},{9},'{10}',{11},'{12}','{13}',{14},{15},0,'Insert',{16},'{17}','{18}','{19}','{20}','{21}' ,'{22}' ", fullCode, Prefix, FullLengthCode, Digit, false, SOCategory, Convert.ToDateTime(orderdt).ToShortDateString(), Division, PartyCat, CUSID, "", Branch, Remark, CheckedBy, UserID, LogNo, WarehouseID, "", GSR, true, false, SessionID, Quotation));
                    }

                    val = val.ToUpper();

                    String DCID = "0";
                    String DCMesg = val;
                    if (val.Contains("`"))
                    {
                        string[] returnDataSplit = val.Split(new string[] { "`" }, StringSplitOptions.None);
                        if (returnDataSplit.Length > 0)
                        {
                            DCMesg = returnDataSplit[0].ToString().Trim();
                            DCID = returnDataSplit[1].ToString().Trim();
                        }

                        if (DCID != "0")
                        {
                            val = DCMesg;
                        }
                    }


                    if (!string.IsNullOrEmpty(val))
                    {
                        if (val == "DUPLICATE")
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                            objSubmitDC.Value = "300";
                            objSubmitDC.Text = "This DC no Already exists! Try with another one!";

                        }
                        else if (val == "DC")
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                            objSubmitDC.Value = "305";
                            objSubmitDC.Text = "Dc No. should be less than what you have put!";

                        }
                        else if (val == "SUCCESSFULLY" || val == "SUCCESS")
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                            objSubmitDC.Value = "200";
                            objSubmitDC.Text = ReturnSAPAPICall(DCID, LogNo, UserID, Branch);

                        }
                        else
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                            objSubmitDC.Value = "400";
                            objSubmitDC.Text = val;
                        }
                    }
                    else
                    {
                        if (Branch != "10" || UserID != "1")
                        {
                            if (stockValidation.Rows[0]["status"].ToString() != "")
                            {
                                string itmStr = "<b>Something is going wrong!!!!.</b><br/>1. Following Base Code are found with invalid quantity <br/> <div style=" + "word-wrap: break-word;" + "> <strong>" + Convert.ToString(stockValidation.Rows[0]["ItmCodes"]) + "</strong></div> <br/><br/> 2.Please delete above mentioned Base Codes and add again with proper quantity.";

                                DataConnectionTrans g1 = new DataConnectionTrans();
                                int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                                objSubmitDC.Value = "500";
                                objSubmitDC.Text = itmStr;

                            }
                            else
                            {
                                DataConnectionTrans g1 = new DataConnectionTrans();
                                int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                                objSubmitDC.Value = "501";
                                objSubmitDC.Text = "<b>Something is going wrong!!!!.</b><br/>1. Please Check the DC List Properly? if any things wrong ,contact back-end Team <br/> 2.Refresh the page and try again.";

                            }
                        }
                        else
                        {
                            DataConnectionTrans g1 = new DataConnectionTrans();
                            int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                            objSubmitDC.Value = "501";
                            objSubmitDC.Text = "<b>Something is going wrong!!!!.</b><br/>1. Please Check the DC List Properly? if any things wrong ,contact back-end Team <br/> 2.Refresh the page and try again.";

                        }
                    }
                }
                else
                {
                    DataConnectionTrans g1 = new DataConnectionTrans();
                    int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                    objSubmitDC.Value = "501";
                    objSubmitDC.Text = "<b>Something is going wrong!!!!.</b><br/>1. Please Check the DC List Properly? if any things wrong ,contact back-end Team <br/> 2.Refresh the page and try again.";
                }
                result.Add(objSubmitDC);
            }
            catch (Exception ex)
            {
                DataConnectionTrans g1 = new DataConnectionTrans();
                int data = g1.ExecDB("exec dcchilddeltemp " + LogNo);
                DCResponse objSubmitDC = new DCResponse();
                objSubmitDC.Value = "501";
                objSubmitDC.Text = ex.ToString();
                result.Add(objSubmitDC);
            }


            //DC Head Entry//

            return result;
        }


        public TaxTypeResponse ReCallTaxType(String PartyCat, String CUSID, String Branch)
        {
            DataTable dispatchaddress = new DataTable();

            TaxTypeResponse result = new TaxTypeResponse();

            DataTable dtaddress1 = new DataTable();

            DataConnectionTrans g1 = new DataConnectionTrans();
            dtaddress1 = g1.return_dt("exec branchshow " + Branch);
            if (dtaddress1.Rows.Count > 0)
            {
                result.SourceState = Convert.ToString(dtaddress1.Rows[0]["stateid"]);
                result.SourceCountry = Convert.ToString(dtaddress1.Rows[0]["countryid"]);
            }

             g1 = new DataConnectionTrans();
            dispatchaddress = g1.return_dt("exec getdispatchaddressdetail " + PartyCat + "," + CUSID);
            if (dispatchaddress.Rows.Count > 0)
            {
                result.DispatchState = Convert.ToString(dispatchaddress.Rows[0]["stateid"]);
                result.DispatchCountry = Convert.ToString(dispatchaddress.Rows[0]["countryid"]);

                result.CST = Convert.ToString(dispatchaddress.Rows[0]["cst"]);
            }

            if (result.SourceCountry == result.DispatchCountry)
            {

                if (PartyCat == "3")
                {
                    result.Type = "4";
                }
                else
                {
                    if (result.DispatchState == result.SourceState)
                    {
                        result.Type = "1";
                        if (PartyCat == "5")
                        {
                            result.Type = "6";
                            string gst1 = string.Empty;
                            string gst2 = string.Empty;
                            DataTable showgst = new DataTable();
                            DataTable showgst2 = new DataTable();

                            g1 = new DataConnectionTrans();
                            showgst = g1.return_dt("exec getgstnodc " + CUSID + "");

                            if (showgst.Rows.Count > 0)
                            {
                                gst1 = Convert.ToString(showgst.Rows[0]["GSTNo"]);
                            }

                            g1 = new DataConnectionTrans();
                            showgst2 = g1.return_dt("exec getgstnodc " + Branch + "");
                            if (showgst2.Rows.Count > 0)
                            {
                                gst2 = Convert.ToString(showgst2.Rows[0]["GSTNo"]);
                            }
                            if (gst1 != gst2)
                            {
                                result.Type = "1";
                            }

                        }
                    }
                    else
                    {
                        result.Type = "2";
                    }
                }
            }
            else
            {
                result.Type = "4";
            }

            return result;
        }

        private DataTable itembind(String Branch, String Warehouse, String Party, String Division, String SOCategory, String CUSID, String GSR, String Quotation)
        {
            DataTable dtItem = new DataTable();
            string orderdt = DateTime.Today.ToString("yyyy-MM-dd");
            if (Warehouse != null && Warehouse != "" && CUSID != "0" && CUSID != "" && SOCategory != "Select" && Division != null && Party != "0")
            {
                DataConnectionTrans g1 = new DataConnectionTrans();
                dtItem = g1.return_dt("exec  getpopendingitemdc3forqr " + Branch + "," + Party + "," + CUSID + "," + Division + ",'" + SOCategory + "','" + orderdt + "'," + Warehouse + ",'" + GSR + "','" + Quotation + "' ");
            }

            return dtItem;
        }

        private String outstandinglimitdayscheck(String CustomerID, String PartyCat)
        {
            String ErrorMesg = "";

            DataTable dttolarencedays = new DataTable();
            DataTable chnlsearch = new DataTable();
            string orderdt2 = string.Empty;
            orderdt2 = DateTime.Now.ToString("dd-MMM-yyyy");

            DataConnectionTrans g1 = new DataConnectionTrans();
            chnlsearch = g1.return_dt("exec Getoutstandinhchanldcsearch '" + CustomerID + "','" + PartyCat + "','" + orderdt2 + "'");

            if (Convert.ToString(chnlsearch.Rows[0]["val"]) == "2")
            {
                g1 = new DataConnectionTrans();
                dttolarencedays = g1.return_dt("exec GetPartyCreditHistryCheck " + CustomerID + "," + PartyCat);
                if (dttolarencedays.Rows.Count > 0)
                {
                    g1 = new DataConnectionTrans();
                    string result2 = g1.reterive_val("exec getpartytemplimitanount " + CustomerID + "," + PartyCat);

                    if (result2 == "0")
                    {
                        ErrorMesg = "0`Billing is Block due to Outstanding.";
                    }
                    else
                    {
                        if (result2 != "500000000")
                        {
                            ErrorMesg = "1`Billing is temporary Allowed, Limit Is: " + result2 + " Please be aware of Amount";
                        }
                    }
                }

            }
            else if (Convert.ToString(chnlsearch.Rows[0]["val"]) == "0")
            {
                ErrorMesg = "0`ChannelFinance Billing is blocked because outstanding>balanceAmount .!";

            }
            else if (Convert.ToString(chnlsearch.Rows[0]["val"]) == "3")
            {
                ErrorMesg = "0`Dealer account frozen by channel finance  .!";
            }
            else
            {
                ErrorMesg = "1`ChannelFinance Billing  Limit Is: " + chnlsearch.Rows[0]["val"] + " Please be aware of Amount";
            }

            if (ErrorMesg.Trim() == "")
            {
                ErrorMesg = "0`";
            }
            return ErrorMesg;
        }

        public string ReturnSAPAPICall(String DCID, String Logno, String UserID, String BranchID)
        {
            String mesg = "DC Created Successfully!";

            String Validate = ConfigurationManager.AppSettings["SAPANDSTOCKBLOCK"].ToString().Trim().ToUpper();
            if (Validate == "YES")
            {
                var datavalidate = false;

                DataTable dtParty = new DataTable();
                DataTable dtInventory = new DataTable();

                DataConnectionTrans g1 = new DataConnectionTrans();
                dtParty = g1.return_dt("exec USP_findDcheadPartyid " + Convert.ToInt32(DCID));

                if (dtParty.Rows.Count > 0)
                {
                    g1 = new DataConnectionTrans();
                    dtInventory = g1.return_dt("exec USP_checkInventoryBranchesMultiandParty " + BranchID + "," + Convert.ToString(dtParty.Rows[0]["partyid"]) + "," + Convert.ToString(dtParty.Rows[0]["typeofparty"]));

                    if (Convert.ToInt32(dtParty.Rows[0]["typeofparty"]) == 2)
                    {
                        if (Convert.ToString(dtInventory.Rows[0]["tblState"]) == "Inventory")
                        {
                            mesg = apihit(Convert.ToInt32(DCID), "1", UserID);
                        }
                        else if (Convert.ToString(dtInventory.Rows[0]["tblState"]) == "Non Inventory")
                        {
                            String returndata = "";
                            String returndataMap = "";
                            datavalidate = InsertUpdateDeleteErpStockData(Convert.ToString(DCID), "I", "0", out returndata, out returndataMap);
                            if (returndata.Trim() == "")
                            {
                                if (returndataMap.Trim() == "")
                                {
                                    if (datavalidate == true)
                                    {
                                        mesg = "Stock hit successfully.";
                                    }
                                    else
                                    {
                                        mesg = "Issue found in Stock hit.";
                                    }
                                }
                                else
                                {
                                    mesg = "Stock hit not Successfully due to " + returndataMap.Trim() + "";
                                }
                            }
                            else
                            {
                                mesg = "Stock hit not Successfully due to " + returndata.Trim() + "";
                            }
                        }
                    }
                    else if (Convert.ToInt32(dtParty.Rows[0]["typeofparty"]) == 5)
                    {
                        if (Convert.ToString(dtInventory.Rows[0]["tblState"]) == "IBIB" || Convert.ToString(dtInventory.Rows[0]["tblState"]) == "IBNIB")
                        {
                            mesg = apihit(Convert.ToInt32(DCID), "1", UserID);
                        }
                        else
                        {
                            String returndata = "";
                            String returndataMap = "";
                            datavalidate = InsertUpdateDeleteErpStockData(Convert.ToString(DCID), "I", "0", out returndata, out returndataMap);
                            if (returndata.Trim() == "")
                            {
                                if (returndataMap.Trim() == "")
                                {
                                    if (datavalidate == true)
                                    {
                                        mesg = "Stock hit successfully.";
                                    }
                                    else
                                    {
                                        mesg = "Issue found in Stock hit.";
                                    }
                                }
                                else
                                {
                                    mesg = "Stock hit not Successfully due to " + returndataMap.Trim() + "";
                                }
                            }
                            else
                            {
                                mesg = "Stock hit not Successfully due to " + returndata.Trim() + "";
                            }
                        }
                    }
                    else
                    {
                        mesg = "Party type not found in Stock hit";
                    }
                }
                else
                {
                    mesg = "Party not found in Stock hit";
                }

            }
            else
            {
                mesg = "Config for SAP/Stock is false";
            }


            return "DC Created Successfully. " + mesg;
        }




        public bool InsertUpdateDeleteErpStockData(String POID, String Type, String POChildslno, out String returndata, out String returndataMap)
        {
            returndata = "";
            returndataMap = "";
            bool validate = false;
            int dtcount = 0;
            int postcount = 0;
            String Validate = ConfigurationManager.AppSettings["SAPANDSTOCKBLOCK"].ToString().Trim().ToUpper();
            if (Validate == "YES")
            {
                DataTable dt = new DataTable();
                DataConnectionTrans g1 = new DataConnectionTrans();
                dt = g1.return_dt("exec Usp_Erp_stockDcHead " + POID.Trim() + "," + POChildslno.Trim() + " ");
                if (dt.Rows.Count > 0)
                {
                    if (Type == "I")
                    {
                        dtcount = dt.Rows.Count;
                        postcount = 0;

                        for (int i2 = 0; i2 < dt.Rows.Count; i2++)
                        {
                            if (Convert.ToInt32(dt.Rows[i2]["stockhit"]) == 0 && Convert.ToInt32(dt.Rows[i2]["stockhitid"]) == 0)
                            {
                                int branchid = Convert.ToInt32(dt.Rows[i2]["branchid"].ToString().Trim());
                                int WarehouseID = Convert.ToInt32(dt.Rows[i2]["WarehouseID"].ToString().Trim());
                                int itemid = Convert.ToInt32(dt.Rows[i2]["itemid"].ToString().Trim());
                                String ItemCode = Convert.ToString(dt.Rows[i2]["ItemCode"].ToString().Trim());
                                decimal approvqty = Convert.ToDecimal(dt.Rows[i2]["approvqty"].ToString().Trim());

                                bool datavalidate = CheckStockValue(branchid.ToString(), WarehouseID.ToString(), itemid.ToString(), approvqty);
                                if (datavalidate == false)
                                {
                                    returndata += "Stock of " + ItemCode + " is not available.";
                                }
                            }
                        }

                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            if (Convert.ToInt32(dt.Rows[i1]["stockhit"]) == 0 && Convert.ToInt32(dt.Rows[i1]["stockhitid"]) == 0)
                            {
                                int branchid = Convert.ToInt32(dt.Rows[i1]["branchid"].ToString().Trim());
                                int WarehouseID = Convert.ToInt32(dt.Rows[i1]["WarehouseID"].ToString().Trim());
                                int itemid = Convert.ToInt32(dt.Rows[i1]["itemid"].ToString().Trim());
                                String ItemCode = Convert.ToString(dt.Rows[i1]["ItemCode"].ToString().Trim());

                                bool datavalidateMap = CheckMEPValue(branchid.ToString(), WarehouseID.ToString(), itemid.ToString());

                                if (datavalidateMap == false)
                                {
                                    returndataMap += "MAP of " + ItemCode + " is not available.";
                                }
                            }
                        }

                        if (returndata.Trim() == "" && returndataMap.Trim() == "")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dt.Rows[i]["stockhit"]) == 0 && Convert.ToInt32(dt.Rows[i]["stockhitid"]) == 0)
                                {
                                    int branchid = Convert.ToInt32(dt.Rows[i]["branchid"].ToString().Trim());
                                    int WarehouseID = Convert.ToInt32(dt.Rows[i]["WarehouseID"].ToString().Trim());
                                    int binid = Convert.ToInt32(dt.Rows[i]["binid"].ToString().Trim());
                                    int partyid = Convert.ToInt32(dt.Rows[i]["partyid"].ToString().Trim());
                                    int typeofparty = Convert.ToInt32(dt.Rows[i]["typeofparty"].ToString().Trim());
                                    int itemid = Convert.ToInt32(dt.Rows[i]["itemid"].ToString().Trim());
                                    String MMType = Convert.ToString(dt.Rows[i]["MMType"].ToString().Trim());
                                    String StockType = Convert.ToString(dt.Rows[i]["StockType"].ToString().Trim());
                                    decimal approvqty = Convert.ToDecimal(dt.Rows[i]["approvqty"].ToString().Trim());
                                    decimal QuntityValue = Convert.ToDecimal(dt.Rows[i]["QuntityValue"].ToString().Trim());
                                    String unittype = Convert.ToString(dt.Rows[i]["unittype"].ToString().Trim());
                                    int Unitid = Convert.ToInt32(dt.Rows[i]["Unitid"].ToString().Trim());
                                    String receivedate = Convert.ToString(dt.Rows[i]["receivedate"].ToString().Trim());
                                    String Postdate = Convert.ToString(dt.Rows[i]["Postdate"].ToString().Trim());
                                    int ISMAP = Convert.ToInt32(dt.Rows[i]["ISMAP"].ToString().Trim());

                                    int headslno = Convert.ToInt32(dt.Rows[i]["HeadSlno"].ToString().Trim());
                                    int childslno = Convert.ToInt32(dt.Rows[i]["ChildSlno"].ToString().Trim());
                                    String tablename = dt.Rows[i]["tablename"].ToString().Trim();
                                    String refno = dt.Rows[i]["refno"].ToString().Trim();
                                    String BranchType = dt.Rows[i]["BranchType"].ToString().Trim();

                                    if (typeofparty == 5)
                                    {
                                        DataTable dtERPStockHit = new DataTable();
                                        dtERPStockHit = InsertERPStockDetails(branchid, WarehouseID, binid, partyid, typeofparty, itemid, MMType, StockType, approvqty, QuntityValue, unittype, Unitid, Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), ISMAP, headslno, childslno, tablename, refno);

                                        if (dtERPStockHit.Rows.Count > 0)
                                        {
                                            String ESlno = dtERPStockHit.Rows[0]["Slno"].ToString();
                                            String Emap = dtERPStockHit.Rows[0]["MAP"].ToString();
                                            String Smap = dtERPStockHit.Rows[0]["SAPMAP"].ToString();
                                            String TxnValue = dtERPStockHit.Rows[0]["TxnValue"].ToString();

                                            if (ESlno != "" && ESlno != "0")
                                            {

                                                if (BranchType == "IB")
                                                {
                                                    String QueryData = " UPDATE Dcchild SET Tx_MepValue = null , Smap='" + Smap + "', MepValue='" + Emap + "', stockhitid = '" + ESlno + "' , stockhit = 1  WHERE SLNo = '" + childslno + "'  exec Usp_UpdateStockhitDCHead '" + childslno + "'  ";
                                                    g1.ExecDB(QueryData);
                                                }
                                                else
                                                {
                                                    String QueryData = " UPDATE Dcchild SET Tx_MepValue = '" + TxnValue + "' , Smap='" + Smap + "', MepValue='" + Emap + "', stockhitid = '" + ESlno + "' , stockhit = 1  WHERE SLNo = '" + childslno + "'  exec Usp_UpdateStockhitDCHead '" + childslno + "'  ";
                                                    g1.ExecDB(QueryData);
                                                }

                                                DataTable dtERPStockHit1 = new DataTable();
                                                dtERPStockHit1 = InsertERPStockDetails(partyid, 0, binid, branchid, 0, itemid, MMType + "C", "Intransit", approvqty, QuntityValue, unittype, Unitid, Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), ISMAP, headslno, childslno, tablename, refno);
                                                if (dtERPStockHit1.Rows.Count > 0)
                                                {
                                                    ESlno = "0";
                                                    Emap = "0";
                                                    Smap = "0";
                                                    TxnValue = "0";

                                                    ESlno = dtERPStockHit1.Rows[0]["Slno"].ToString();
                                                    Emap = dtERPStockHit1.Rows[0]["MAP"].ToString();
                                                    Smap = dtERPStockHit1.Rows[0]["SAPMAP"].ToString();
                                                    TxnValue = dtERPStockHit1.Rows[0]["TxnValue"].ToString();

                                                    if (ESlno != "" && ESlno != "0")
                                                    {
                                                        if (BranchType == "IB")
                                                        {
                                                            postcount = postcount + 1;
                                                            String QueryData = " UPDATE Dcchild SET Tx_MepValue2 = null , stockhitid2 = '" + ESlno + "' , stockhit = 1  WHERE SLNo = '" + childslno + "'  exec Usp_UpdateStockhitDCHead '" + childslno + "'  ";
                                                            g1.ExecDB(QueryData);
                                                        }
                                                        else
                                                        {
                                                            postcount = postcount + 1;
                                                            String QueryData = " UPDATE Dcchild SET Tx_MepValue2 = '" + TxnValue + "' , stockhitid2 = '" + ESlno + "' , stockhit = 1  WHERE SLNo = '" + childslno + "'  exec Usp_UpdateStockhitDCHead '" + childslno + "'  ";
                                                            g1.ExecDB(QueryData);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (typeofparty == 2)
                                    {
                                        DataTable dtERPStockHit = new DataTable();
                                        dtERPStockHit = InsertERPStockDetails(branchid, WarehouseID, binid, partyid, typeofparty, itemid, MMType, StockType, approvqty, QuntityValue, unittype, Unitid, Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), Convert.ToDateTime(receivedate).ToString("dd-MMM-yyyy HH:mm:ss"), ISMAP, headslno, childslno, tablename, refno);
                                        if (dtERPStockHit.Rows.Count > 0)
                                        {
                                            String ESlno = dtERPStockHit.Rows[0]["Slno"].ToString();
                                            String Emap = dtERPStockHit.Rows[0]["MAP"].ToString();
                                            String Smap = dtERPStockHit.Rows[0]["SAPMAP"].ToString();
                                            String TxnValue = dtERPStockHit.Rows[0]["TxnValue"].ToString();

                                            if (ESlno != "" && ESlno != "0")
                                            {
                                                postcount = postcount + 1;
                                                String QueryData = " UPDATE Dcchild SET Tx_MepValue = '" + TxnValue + "' ,  Smap='" + Smap + "', MepValue='" + Emap + "', stockhitid = '" + ESlno + "' , stockhit = 1  WHERE SLNo = '" + childslno + "'  exec Usp_UpdateStockhitDCHead '" + childslno + "' ";
                                                g1.ExecDB(QueryData);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (postcount == dtcount)
                    {
                        validate = true;
                    }
                    else
                    {
                        validate = false;
                    }

                }
            }
            return validate;
        }

        public bool CheckStockValue(String Branchid, String WarehouseID, String Itemid, decimal StockQty)
        {
            DataTable dts = new DataTable();
            bool validate = false;
            DataConnectionTrans g1 = new DataConnectionTrans();
            dts = g1.return_dt("exec Usp_Erp_FindStockERP " + Branchid + "," + WarehouseID + "," + Itemid);
            if (dts.Rows.Count > 0)
            {
                String StockValue = dts.Rows[0]["StockValue"].ToString().Trim();
                if (StockValue != "")
                {
                    if (Convert.ToDecimal(StockValue) > 0)
                    {
                        if (Convert.ToDecimal(StockValue) >= StockQty)
                        {
                            validate = true;
                        }
                    }
                }
            }

            return validate;
        }

        public bool CheckMEPValue(String Branchid, String WarehouseID, String Itemid)
        {
            DataTable dts = new DataTable();
            bool validate = false;
            DataConnectionTrans g1 = new DataConnectionTrans();
            dts = g1.return_dt("exec Usp_Erp_FindGetmappriceERP " + Branchid + "," + WarehouseID + "," + Itemid);
            if (dts.Rows.Count > 0)
            {
                String MAP = dts.Rows[0]["Map"].ToString().Trim();
                if (Convert.ToDecimal(MAP) > 0)
                {
                    validate = true;
                }
            }
            return validate;
        }

        public string apihit(int slno, String BranchType, string UserID)
        {
            string FormType = "Dchead";
            string TableName = "Dchead";
            bool IsReversal = true;
            var datavalidate = false;
            String Validate = ConfigurationManager.AppSettings["SAPANDSTOCKBLOCK"].ToString().Trim().ToUpper();
            if (Validate == "YES")
            {
                DataTable dtuniquekey = new DataTable();

                DataConnectionTrans g1 = new DataConnectionTrans();
                dtuniquekey = g1.return_dt("exec getDcheaduniquekey " + slno);

                if (Convert.ToString(dtuniquekey.Rows[0]["uniquekey"]) != "")
                {
                    Sapapi sa = new Sapapi();
                    String result = sa.GoodsReceiptStockTransfer(Convert.ToInt32(slno), Convert.ToString(dtuniquekey.Rows[0]["uniquekey"]), IsReversal, FormType, TableName, Convert.ToInt32(UserID), BranchType, "apihit");
                    String[] ArrResult = result.Split('~');
                    if (ArrResult[0] == "200" && ArrResult[2] != "0")
                    {
                        String returndata = "";
                        String returndataMap = "";
                        datavalidate = InsertUpdateDeleteErpStockData(Convert.ToString(slno), "I", "0", out returndata, out returndataMap);
                        if (returndata.Trim() == "")
                        {
                            if (returndataMap.Trim() == "")
                            {
                                if (datavalidate == true)
                                {
                                    return Convert.ToString(dtuniquekey.Rows[0]["dcno"]) + " - SAP api and stock hit successfull.";
                                }
                                else
                                {
                                    return Convert.ToString(dtuniquekey.Rows[0]["dcno"]) + " - SAP api successfull. Issue found in stock hit.";
                                }
                            }
                            else
                            {
                                return "Stock hit not Successfully due to " + returndataMap.Trim();

                            }
                        }
                        else
                        {
                            return "SAP api successfull. Issue found in stock hit due to " + returndata.Trim();
                        }
                    }
                    else
                    {
                        return "SAP api failed.";
                    }
                }
                else
                {
                    return "";
                }
            }


            return "";
        }



        public DataTable InsertERPStockDetails(int branchid, int warehouseid, int warehousebinid, int vendorpartyid, int typeofparty, int itemid, string mmtype, string stocktype, decimal qty, decimal qtyvalue, string unittype, int unitid, string docdate, string docpostdate, int ismapqtycost, int headslno, int childslno, string tablename, string refno)
        {
            DataTable dt = new DataTable();
            try
            {
                DataConnectionTrans g1 = new DataConnectionTrans();
                DataSet ds = new DataSet();
                SqlParameter[] param = new SqlParameter[21];
                param[0] = new SqlParameter("@branchid", branchid);
                param[1] = new SqlParameter("@warehouseid", warehouseid);
                param[2] = new SqlParameter("@warehousebinid", warehousebinid);
                param[3] = new SqlParameter("@vendorpartyid", vendorpartyid);
                param[4] = new SqlParameter("@typeofparty", typeofparty);
                param[5] = new SqlParameter("@itemid", itemid);
                param[6] = new SqlParameter("@mmtype", mmtype);
                param[7] = new SqlParameter("@stocktype", stocktype);
                param[8] = new SqlParameter("@qty", qty);
                param[9] = new SqlParameter("@qtyvalue", qtyvalue);
                param[10] = new SqlParameter("@unittype", unittype);
                param[11] = new SqlParameter("@unitid", unitid);
                param[12] = new SqlParameter("@docdate", docdate);
                param[13] = new SqlParameter("@docpostdate", docpostdate);
                param[14] = new SqlParameter("@ismapqtycost", ismapqtycost);
                param[15] = new SqlParameter("@headslno", headslno);
                param[16] = new SqlParameter("@childslno", childslno);
                param[17] = new SqlParameter("@tablename", tablename);
                param[18] = new SqlParameter("@refno", refno);
                param[19] = new SqlParameter("@reverserefno", 0);

                param[20] = new SqlParameter("@slno", SqlDbType.BigInt);
                param[20].Direction = ParameterDirection.Output;
                ds = g1.FillDataSet("usp_insertErpStockDetailsforerp", param);

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }

            return dt;
        }





        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable();
            if (items.Count > 0)
            {
                dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }

            return dataTable;
        }

        public DataTable ToDataTable<T>(List<T> items, String BranchID, String WarehouseID, String UserID)
        {
            DataTable dataTable = new DataTable();
            if (items.Count > 0)
            {
                dataTable = new DataTable(typeof(T).Name);

                DataColumn newColumn = new DataColumn("UserID", typeof(System.String));
                newColumn.DefaultValue = UserID.Trim();
                dataTable.Columns.Add(newColumn);

                newColumn = new DataColumn("BranchID", typeof(System.String));
                newColumn.DefaultValue = BranchID.Trim();
                dataTable.Columns.Add(newColumn);

                newColumn = new DataColumn("WarehouseID", typeof(System.String));
                newColumn.DefaultValue = WarehouseID.Trim();
                dataTable.Columns.Add(newColumn);

                dataTable.AcceptChanges();


                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    dataTable.Columns.Add(prop.Name);
                }

                foreach (T item in items)
                {
                    var values = new object[Props.Length + 3];
                    values[0] = UserID;
                    values[1] = BranchID;
                    values[2] = WarehouseID;
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i + 3] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            }

            return dataTable;
        }
    }
}