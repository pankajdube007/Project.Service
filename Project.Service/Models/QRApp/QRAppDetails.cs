using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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