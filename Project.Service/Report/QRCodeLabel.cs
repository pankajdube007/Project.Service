using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for LabelPrint2
/// </summary>
public class QRCodeLabel : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPanel xrPanel1;
    private FormattingRule formattingRule1;
    private DevExpress.XtraReports.Parameters.Parameter parameter2;
    private DevExpress.XtraReports.Parameters.Parameter parameter3;
    private DevExpress.XtraReports.Parameters.Parameter parameter4;
    private XRBarCode xrBarCode2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel2;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public QRCodeLabel()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode2 = new DevExpress.XtraReports.UI.XRBarCode();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter3 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter4 = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.Dpi = 254F;
            this.Detail.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.Detail.HeightF = 230F;
            this.Detail.KeepTogether = true;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.MultiColumn.ColumnWidth = 238F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel1,
            this.xrBarCode2});
            this.xrPanel1.Dpi = 254F;
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(230F, 230F);
            this.xrPanel1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 254F);
            this.xrPanel1.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrPanel1.StylePriority.UseBorders = false;
            this.xrPanel1.StylePriority.UsePadding = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Angle = 90F;
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.CanGrow = false;
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.itemcode")});
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial Black", 4F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.2034759F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(30F, 230F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Angle = 90F;
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.CanGrow = false;
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.LabelBarcode")});
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Arial Black", 4F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(200F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(29.99998F, 230F);
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Angle = 90F;
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.itemnm")});
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(296.5651F, 30.00003F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(66.79163F, 170F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.CanGrow = false;
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.prntnm2")});
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial Black", 4F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(30F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(170F, 29.99998F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UsePadding = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.CanGrow = false;
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.QRCode")});
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial Black", 4F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(30F, 200F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(170F, 29.99998F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrBarCode2
            // 
            this.xrBarCode2.AutoModule = true;
            this.xrBarCode2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrBarCode2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getQrCodePrint23ForQrApplication.Barcode")});
            this.xrBarCode2.Dpi = 254F;
            this.xrBarCode2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrBarCode2.ForeColor = System.Drawing.Color.Black;
            this.xrBarCode2.LocationFloat = new DevExpress.Utils.PointFloat(30F, 30.00001F);
            this.xrBarCode2.Module = 5.08F;
            this.xrBarCode2.Name = "xrBarCode2";
            this.xrBarCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrBarCode2.ShowText = false;
            this.xrBarCode2.SizeF = new System.Drawing.SizeF(170F, 170F);
            this.xrBarCode2.StylePriority.UseBorders = false;
            this.xrBarCode2.StylePriority.UseFont = false;
            this.xrBarCode2.StylePriority.UseForeColor = false;
            this.xrBarCode2.StylePriority.UsePadding = false;
            this.xrBarCode2.StylePriority.UseTextAlignment = false;
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            this.xrBarCode2.Symbology = qrCodeGenerator1;
            this.xrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "[name]=\'Unknown\'";
            this.formattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 10F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.StylePriority.UsePadding = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // parameter2
            // 
            this.parameter2.Name = "parameter2";
            this.parameter2.Type = typeof(int);
            this.parameter2.ValueInfo = "0";
            // 
            // parameter3
            // 
            this.parameter3.Description = "parameter3";
            this.parameter3.Name = "parameter3";
            // 
            // parameter4
            // 
            this.parameter4.Description = "Parameter4";
            this.parameter4.Name = "parameter4";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "myconread";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "getQrCodePrint23ForQrApplication";
            queryParameter1.Name = "@ProductID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter2]", typeof(long));
            queryParameter2.Name = "@QRCODE";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter3]", typeof(string));
            queryParameter3.Name = "@LabelBarcode";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter4]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.StoredProcName = "getQrCodePrint23ForQrApplication";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            // 
            // QRCodeLabel
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "getQrCodePrint23ForQrApplication";
            this.DataSource = this.sqlDataSource1;
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 10, 10);
            this.PageHeight = 250;
            this.PageWidth = 238;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter2,
            this.parameter3,
            this.parameter4});
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
