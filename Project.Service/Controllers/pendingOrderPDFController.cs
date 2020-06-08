using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class pendingOrderPDFController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPendingOrdersPDF")]
        public HttpResponseMessage GetDetails(ListsofPendingOrderPDFAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PendingOrderPDFs> alldcr = new List<PendingOrderPDFs>();
                    List<PendingOrderPDF> alldcr1 = new List<PendingOrderPDF>();

                    DataTable dr = g1.return_dt("App_PendingOrder '" + ula.CIN + "',0,'',0,9999,'" + ula.AsonDate + "'," + ula.Type);

                    if (dr.Rows.Count > 0)
                    {
                        string URL = PendingOrderReport(ula.CIN, ula.AsonDate, dr);
                        alldcr1.Add(new PendingOrderPDF
                        {
                            url = URL
                        });

                        g1.close_connection();
                        alldcr.Add(new PendingOrderPDFs
                        {
                            result = true,
                            message = "",
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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

        protected string PendingOrderReport(string CIN, string date, DataTable dtparty)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            string AttachmentName = string.Empty;
            string FileName = string.Empty;
            string link = string.Empty;
            string uniquefoldernm = string.Empty;
            //  DataTable dtparty = g1.return_dt("exec App_PendingOrder '" + CIN + "',0,'',0,9999,'" + date + "'");
            if (dtparty.Rows.Count > 0)
            {
                FileName = CIN;
                uniquefoldernm = "mobiledata/pending";
                byte[] report = createPDF(dtparty);
                Stream stream = new MemoryStream(report);
                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".pdf", stream, "application/pdf", false, false, true);

                //link = HttpContext.Current.Server.MapPath("~/MobileData/AppPending/" + uniquefoldernm + "/");
                ////  link = string.Format(WebConfigurationManager.AppSettings["AttachmentAppUrl"].ToString() + "{0}\\" + "{1}", "AppPending", uniquefoldernm);
                //string directoryPath = link;

                //if (Directory.Exists(link + "/" + FileName))
                //{
                //    Directory.Delete(link + "/" + FileName);
                //}
                //if (directoryPath != "")
                //{
                //    Directory.CreateDirectory(directoryPath);
                //}
            }

            // return HttpContext.Current.Server.MapPath(link + CIN + ".pdf");
            return _goldMedia.MapPathToPublicUrl(uniquefoldernm + "/" + FileName).ToString() + ".pdf";
        }

        public byte[] createPDF(DataTable dataTable)
        {
            dataTable.Columns.Remove("TotalCount");
            dataTable.Columns.Remove("slno");
            dataTable.Columns.Remove("itemcode");

            dataTable.Columns["itemnm"].ColumnName = "Item Name";
            dataTable.Columns["colornm"].ColumnName = "Color Name";
            dataTable.Columns["ponum"].ColumnName = "PO No.";
            dataTable.Columns["podt"].ColumnName = "PO Date";
            dataTable.Columns["pending"].ColumnName = "Pending Qty";
            dataTable.Columns["withtax"].ColumnName = "Amount";

            MemoryStream mStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, mStream);
            document.Open();

            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            table.WidthPercentage = 100;

            //Set columns names in the pdf file
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[k].ColumnName));

                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102);

                table.AddCell(cell);
            }

            //Add values of DataTable in pdf file
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString()));

                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
            }

            document.Add(table);
            document.Close();
            return mStream.ToArray();
        }
    }
}