using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;

namespace Project.Service.Models
{
    public class VendorQRDetails
    {
        public HttpResponseMessage GetQRDetails(GetQRData objGetQRData)
        {
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objGetQRData);

            GetQRDataResponse objGetQRDataResponse = new GetQRDataResponse();

            String VendorID = "0";
            String BranchID = "0";
            String Type = "PRODUCT";
            String UserType = "VENDOR";

            if (objGetQRData != null)
            {
                if (objGetQRData.VendorID != null)
                {
                    if (objGetQRData.VendorID.ToString().Trim() != null)
                    {
                        VendorID = objGetQRData.VendorID.ToString().Trim();
                    }
                }
            }


            if (objGetQRData != null)
            {
                if (objGetQRData.UserType != null)
                {
                    if (objGetQRData.UserType.ToString().Trim() != null)
                    {
                        UserType = objGetQRData.UserType.ToString().Trim();
                    }
                }
            }


            if (objGetQRData != null)
            {
                if (objGetQRData.BranchID != null)
                {
                    if (objGetQRData.BranchID.ToString().Trim() != null)
                    {
                        BranchID = objGetQRData.BranchID.ToString().Trim();
                    }
                }
            }

            if (objGetQRData != null)
            {
                if (objGetQRData.Type != null)
                {
                    if (objGetQRData.Type.ToString().Trim() != null)
                    {
                        Type = objGetQRData.Type.ToString().Trim();
                    }
                }
            }

            DataSet ds = new DataSet();
            try
            {
                //DataConnectionTrans objDataAccess = new DataConnectionTrans();
                //SqlParameter[] param = new SqlParameter[3];
                //param[0] = new SqlParameter("@VendorID", VendorID);
                //param[1] = new SqlParameter("@BranchID", BranchID);
                //param[2] = new SqlParameter("@Type", Type);
                //ds = objDataAccess.FillDataSet("GetProductDetailsForScanner", param);


                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@VendorID", VendorID);
                param[1] = new SqlParameter("@BranchID", BranchID);
                param[2] = new SqlParameter("@Type", Type);
                param[3] = new SqlParameter("@UserType", UserType);
                ds = objDataAccess.FillDataSet("GetProductDetailsForScanner", param);
            }
            catch (Exception ex)
            {

            }

            if (ds.Tables.Count > 0)
            {
                objGetQRDataResponse.Code = "200";
                objGetQRDataResponse.Message = "Success";
                objGetQRDataResponse.dsData = ds;
            }
            else
            {
                objGetQRDataResponse.Code = "400";
                objGetQRDataResponse.Message = "No Details Found";
                objGetQRDataResponse.dsData = ds;
            }


            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objGetQRDataResponse);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings),
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        public HttpResponseMessage PostQRMapingDetails(PostQRMapingData objPostQRMapingData)
        {
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objPostQRMapingData);

            PostQRMapingDataResponse objPostQRMapingDataResponse = new PostQRMapingDataResponse();

            String VendorID = "0";
            String BranchID = "0";

            if (objPostQRMapingData != null)
            {
                if (objPostQRMapingData.VendorID != null)
                {
                    if (objPostQRMapingData.VendorID.ToString().Trim() != null)
                    {
                        VendorID = objPostQRMapingData.VendorID.ToString().Trim();
                    }
                }
            }


            if (objPostQRMapingData != null)
            {
                if (objPostQRMapingData.BranchID != null)
                {
                    if (objPostQRMapingData.BranchID.ToString().Trim() != null)
                    {
                        BranchID = objPostQRMapingData.BranchID.ToString().Trim();
                    }
                }
            }

            List<PostQRMapingDetailsResponse> objPostQRMapingDetailsResponseList = new List<PostQRMapingDetailsResponse>();
            DataSet ds = new DataSet();
            DataTable dtData = new DataTable();
            if (objPostQRMapingData.objPostQRMapingDetails != null)
            {
                dtData = ToDataTable(objPostQRMapingData.objPostQRMapingDetails);
            }

            if (dtData.Rows.Count > 0)
            {
                String strSessionID = "User" + VendorID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");
                DataColumn newColumn = new DataColumn("SessionID", typeof(System.String));
                newColumn.DefaultValue = strSessionID.ToString().ToUpper();
                dtData.Columns.Add(newColumn);

                newColumn = new DataColumn("CreatedDate", typeof(System.DateTime));
                newColumn.DefaultValue = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                dtData.Columns.Add(newColumn);

                newColumn = new DataColumn("VendorID", typeof(System.String));
                newColumn.DefaultValue = VendorID.Trim();
                dtData.Columns.Add(newColumn);

                newColumn = new DataColumn("BranchID", typeof(System.String));
                newColumn.DefaultValue = BranchID.Trim();
                dtData.Columns.Add(newColumn);

                DataConnectionTrans objDataAccess = new DataConnectionTrans();
                String Data = objDataAccess.BulkInsert(dtData, "TempQRPostDetails");

                objDataAccess = new DataConnectionTrans();
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@VendorID", VendorID);
                param[1] = new SqlParameter("@BranchID", BranchID);
                param[2] = new SqlParameter("@SessionID", strSessionID.ToString().ToUpper());
                ds = objDataAccess.FillDataSet("UpdateProductDetailsForScannerQRBulk", param);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (dr["InvPostType"].ToString().Trim().ToUpper() != "AUTO")
                        {
                            PostQRMapingDetailsResponse objPostQRMapingDetailsResponse = new PostQRMapingDetailsResponse();
                            objPostQRMapingDetailsResponse.PQRCode = dr["PQRCode"].ToString().Trim();
                            objPostQRMapingDetailsResponseList.Add(objPostQRMapingDetailsResponse);
                        }
                    }

                    DataTable dtAutoQR = new DataTable();
                    dtAutoQR = ds.Tables[1];

                    if (dtAutoQR.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAutoQR.Rows)
                        {
                            PostQRMapingDetailsResponse objPostQRMapingDetailsResponse = new PostQRMapingDetailsResponse();
                            objPostQRMapingDetailsResponse.PQRCode = dr["PQRCode"].ToString().Trim();
                            objPostQRMapingDetailsResponseList.Add(objPostQRMapingDetailsResponse);
                        }
                    }
                }
            }

            if (objPostQRMapingDetailsResponseList.Count > 0)
            {
                objPostQRMapingDataResponse.Code = "200";
                objPostQRMapingDataResponse.Message = "Success";
                objPostQRMapingDataResponse.objPostQRMapingDetailsResponse = objPostQRMapingDetailsResponseList;
            }
            else
            {
                objPostQRMapingDataResponse.Code = "400";
                objPostQRMapingDataResponse.Message = "No Details Found";
                objPostQRMapingDataResponse.objPostQRMapingDetailsResponse = objPostQRMapingDetailsResponseList;
            }


            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPostQRMapingDataResponse);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        public HttpResponseMessage QRSyncPostAPIUrl(List<QRSyncUpdateData> objQRSyncUpdateDataList)
        {
            String LogJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objQRSyncUpdateDataList);

            PostQRMapingDataResponse objPostQRMapingDataResponse = new PostQRMapingDataResponse();

            String VendorID = "0";

            if (objQRSyncUpdateDataList != null)
            {
                if (objQRSyncUpdateDataList.Count > 0)
                {
                    VendorID = objQRSyncUpdateDataList[0].VendorID.Trim();


                    String strSessionID = "User" + VendorID.ToString() + DateTime.Now.ToString("ddMMMyyyyhhmmss");
                    DataTable dtData = new DataTable();
                    if (objQRSyncUpdateDataList != null)
                    {
                        dtData = ToDataTable(objQRSyncUpdateDataList);
                    }


                    if (dtData.Rows.Count > 0)
                    {

                        DataColumn newColumn = new DataColumn("SessionID", typeof(System.String));
                        newColumn.DefaultValue = strSessionID.ToString().ToUpper();
                        dtData.Columns.Add(newColumn);

                        newColumn = new DataColumn("CreatedDate", typeof(System.DateTime));
                        newColumn.DefaultValue = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                        dtData.Columns.Add(newColumn);

                        DataConnectionTrans objDataAccess = new DataConnectionTrans();
                        String Data = objDataAccess.BulkInsert(dtData, "TempQRQCGID");

                        DataSet ds = new DataSet();
                        objDataAccess = new DataConnectionTrans();
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@VendorID", VendorID);
                        param[1] = new SqlParameter("@SessionID", strSessionID.ToString().ToUpper());
                        ds = objDataAccess.FillDataSet("UpdateQRSyncDataBulk", param);

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                        }
                    }
                }
            }

            objPostQRMapingDataResponse.Code = "200";
            objPostQRMapingDataResponse.Message = "Success";


            string jsonstrings = Newtonsoft.Json.JsonConvert.SerializeObject(objPostQRMapingDataResponse);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonstrings)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
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
    }

}


public class GetQRData
{
    public string VendorID { get; set; }
    public string BranchID { get; set; }
    public string Type { get; set; }
    public string UserType { get; set; }

    
}


public class QRSyncUpdateData
{
    public string QCGID { get; set; }
    public string VendorID { get; set; }
}

public class GetQRDataResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public DataSet dsData { get; set; }
}


public class PostQRMapingData
{
    public string VendorID { get; set; }
    public string BranchID { get; set; }
    public List<PostQRMapingDetails> objPostQRMapingDetails { get; set; }
}

public class PostQRMapingDetails
{
    public string ProductID { get; set; }
    public string PQRCode { get; set; }
    public string IQRCode { get; set; }
    public string OQRCode { get; set; }
    public string CQRCode { get; set; }

    public string PComputerName { get; set; }
    public string PIPAddress { get; set; }
    public string PScannerName { get; set; }
    public string PDeviceID { get; set; }
    public string PMappedBy { get; set; }
    public string PMappedDate { get; set; }

    public string IComputerName { get; set; }
    public string IIPAddress { get; set; }
    public string IScannerName { get; set; }
    public string IDeviceID { get; set; }
    public string IMappedBy { get; set; }
    public string IMappedDate { get; set; }

    public string OComputerName { get; set; }
    public string OIPAddress { get; set; }
    public string OScannerName { get; set; }
    public string ODeviceID { get; set; }
    public string OMappedBy { get; set; }
    public string OMappedDate { get; set; }

    public string CComputerName { get; set; }
    public string CIPAddress { get; set; }
    public string CScannerName { get; set; }
    public string CDeviceID { get; set; }
    public string CMappedBy { get; set; }
    public string CMappedDate { get; set; }

    public string InvoiceRefNo { get; set; }
    public string InvoiceRefDate { get; set; }
    public string InvPostType { get; set; }
    public string InvPostDate { get; set; }
    public string IType { get; set; }
    public string POID { get; set; }
    public string BatchNo { get; set; }
}




public class PostQRMapingDataResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public List<PostQRMapingDetailsResponse> objPostQRMapingDetailsResponse { get; set; }
}

public class PostQRMapingDetailsResponse
{
    public string PQRCode { get; set; }
}