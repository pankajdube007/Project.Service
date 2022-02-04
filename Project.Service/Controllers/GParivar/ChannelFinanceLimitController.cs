using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class ChannelFinanceLimitController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getChannelFinanceLimit")]
        public HttpResponseMessage GetDetails()
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            //if (ula.CIN != "")
            //{
            try
            {
                string data1;
                List<AnalyticsDatas> alldcr = new List<AnalyticsDatas>();
                List<AnalyticsData> alldcr1 = new List<AnalyticsData>();

                //   var dr = g2.return_dr("AddAnalyticsData '" + ula.CIN + "','" + ula.DeviceId + "','" + ula.ClientSecret + "','" + ula.DateTime + "','" + ula.ScreenName + "','" + ula.ScreenId + "','" + ula.AppId + "','" + ula.OSVersion + "','" + ula.DeviceModel + "','" + ula.DeviceType + "'");
                var dr = GetDataTableFromExcel("D://Live Projects//ICICI//DealerWiseListingTypeII24Nov2021-419-1.xls");
                if (dr.Rows.Count>0)
                {
                    alldcr1.Add(new AnalyticsData
                    {
                        output = "Data Sucessfully inserted"
                    });

                    g2.close_connection();
                    alldcr.Add(new AnalyticsDatas
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
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Not Created!!!!!!!!"), Encoding.UTF8, "application/json");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                return response;
                //    }
                //}
                //else
                //{
                //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                //    response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                //    return response;
                //}
            }
        }


        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[9, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
    }
}