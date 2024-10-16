using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for LabelPrint2
/// </summary>
public class MasterCeilingFanEANPrint2 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPanel xrPanel1;
    private DevExpress.XtraReports.Parameters.Parameter parameter1;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private FormattingRule formattingRule1;
    private DevExpress.XtraReports.Parameters.Parameter parameter2;
    private DevExpress.XtraReports.Parameters.Parameter parameter3;
    private XRLabel xrLabel9;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel1;
    private XRLabel xrLabel5;
    private XRPanel xrPanel3;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRLabel xrLabel2;
    private XRTableCell xrTableCell2;
    private XRLabel xrLabel6;
    private XRTableCell xrTableCell5;
    private XRLabel xrLabel10;
    private XRTableCell xrTableCell4;
    private XRLabel xrLabel12;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel13;
    private XRLabel xrLabel17;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel18;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell11;
    private XRLabel xrLabel19;
    private XRTableCell xrTableCell12;
    private XRLabel xrLabel4;
    private XRLabel xrLabel8;
    private DevExpress.XtraReports.Parameters.Parameter parameter4;
    private DevExpress.XtraReports.Parameters.Parameter parameter5;
    private XRLabel xrLabel22;
    private XRLabel xrLabel7;
    private XRLabel xrLabel11;
    private XRBarCode xrBarCode2;
    private XRLabel xrLabel16;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public MasterCeilingFanEANPrint2()
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
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrBarCode2 = new DevExpress.XtraReports.UI.XRBarCode();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.parameter5 = new DevExpress.XtraReports.Parameters.Parameter();
        this.parameter4 = new DevExpress.XtraReports.Parameters.Parameter();
        this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
        this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
        this.parameter3 = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 750F;
        this.Detail.KeepTogether = true;
        this.Detail.KeepTogetherWithDetailReports = true;
        this.Detail.MultiColumn.ColumnSpacing = 15F;
        this.Detail.MultiColumn.ColumnWidth = 1250F;
        this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 15, 15, 254F);
        this.Detail.StylePriority.UsePadding = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.BorderWidth = 0F;
        this.xrPanel1.CanGrow = false;
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16,
            this.xrBarCode2,
            this.xrLabel11,
            this.xrLabel7,
            this.xrLabel22,
            this.xrPanel3});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(24.22912F, 12F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254F);
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1178.563F, 720F);
        this.xrPanel1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 254F);
        this.xrPanel1.StylePriority.UseBorders = false;
        this.xrPanel1.StylePriority.UseBorderWidth = false;
        this.xrPanel1.StylePriority.UsePadding = false;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.itemnm")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.ForeColor = System.Drawing.Color.Black;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(38.99361F, 118.9375F);
        this.xrLabel16.Multiline = true;
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(413.992F, 79.47577F);
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseForeColor = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrBarCode2
        // 
        this.xrBarCode2.AutoModule = true;
        this.xrBarCode2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrBarCode2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Content")});
        this.xrBarCode2.Dpi = 254F;
        this.xrBarCode2.Font = new System.Drawing.Font("Arial", 8F);
        this.xrBarCode2.ForeColor = System.Drawing.Color.Black;
        this.xrBarCode2.LocationFloat = new DevExpress.Utils.PointFloat(1022.349F, 17.60419F);
        this.xrBarCode2.Module = 5.08F;
        this.xrBarCode2.Name = "xrBarCode2";
        this.xrBarCode2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrBarCode2.ShowText = false;
        this.xrBarCode2.SizeF = new System.Drawing.SizeF(150F, 150F);
        this.xrBarCode2.StylePriority.UseBorders = false;
        this.xrBarCode2.StylePriority.UseFont = false;
        this.xrBarCode2.StylePriority.UseForeColor = false;
        this.xrBarCode2.StylePriority.UsePadding = false;
        this.xrBarCode2.StylePriority.UseTextAlignment = false;
        qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
        this.xrBarCode2.Symbology = qrCodeGenerator1;
        this.xrBarCode2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Barcode")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(970.7654F, 171.6042F);
        this.xrLabel11.Multiline = true;
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(203.7977F, 31.80907F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Content1")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 7F);
        this.xrLabel7.ForeColor = System.Drawing.Color.Black;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(207.8353F, 438.0002F);
        this.xrLabel7.Multiline = true;
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(968.3688F, 42.39243F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseForeColor = false;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.LabelBarcode")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel22.ForeColor = System.Drawing.Color.Black;
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(721.6764F, 25F);
        this.xrLabel22.Multiline = true;
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(299.6725F, 132.0209F);
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrTable4,
            this.xrTable3,
            this.xrTable2,
            this.xrTable1});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0.7708874F, 206.4375F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(1177.021F, 225.0211F);
        this.xrPanel3.StylePriority.UseBorders = false;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.colornm")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.ForeColor = System.Drawing.Color.Black;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(524.541F, 70.03952F);
        this.xrLabel9.Multiline = true;
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(189.5175F, 134.2813F);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseForeColor = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(782.5794F, 75.03941F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(391.2127F, 151.0023F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.StylePriority.UseBorders = false;
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.xrLabel19});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Multiline = true;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 1.3213608065732281D;
        // 
        // xrLabel15
        // 
        this.xrLabel15.BorderColor = System.Drawing.Color.Black;
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.MFDate")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.ForeColor = System.Drawing.Color.Black;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(5F, 99.8125F);
        this.xrLabel15.Multiline = true;
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(152.289F, 30.39243F);
        this.xrLabel15.StylePriority.UseBorderColor = false;
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseForeColor = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Arial", 7F);
        this.xrLabel19.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5.520752F);
        this.xrLabel19.Multiline = true;
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(187.1505F, 72.8959F);
        this.xrLabel19.StylePriority.UseBorders = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseForeColor = false;
        this.xrLabel19.Text = "Month & Year of Mfg.";
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Multiline = true;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 1.165645289793235D;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Price")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.ForeColor = System.Drawing.Color.Black;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(31F, 32.00003F);
        this.xrLabel14.Multiline = true;
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(142.8175F, 61.41653F);
        this.xrLabel14.StylePriority.UseBorders = false;
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseForeColor = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 157.8519F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(521.9485F, 68.18982F);
        this.xrTable3.StylePriority.UseBorders = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Multiline = true;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 0.98511375318419758D;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.sweep")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(55.08338F, 18.36002F);
        this.xrLabel4.Multiline = true;
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(168.2143F, 42.3924F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Multiline = true;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 1.1046729956972847D;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Voltage")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 7F);
        this.xrLabel8.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(30.25488F, 18.35995F);
        this.xrLabel8.Multiline = true;
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(227.0916F, 42.3924F);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseForeColor = false;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.85193F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(521.9485F, 81.60403F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Multiline = true;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 0.38761676434469922D;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.itemcode")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel18.ForeColor = System.Drawing.Color.Black;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(55.08338F, 3.051758E-05F);
        this.xrLabel18.Multiline = true;
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(189.2143F, 53.41656F);
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Multiline = true;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 0.43465999670934036D;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.prntnm2")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.Color.Black;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrLabel1.Multiline = true;
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(247.3465F, 67.604F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1177.792F, 68.85191F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell5,
            this.xrTableCell4,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Multiline = true;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 1.539338816769267D;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(4F, 9.819F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(153.3912F, 38.13487F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.Text = "Model No";
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Multiline = true;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 2.0057762305744307D;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(13.25488F, 9.819F);
        this.xrLabel6.Multiline = true;
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(208.0018F, 40.31587F);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseForeColor = false;
        this.xrLabel6.Text = "Product Name";
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Multiline = true;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.5722398352888187D;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(38.52979F, 14.81906F);
        this.xrLabel10.Multiline = true;
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(128.3911F, 35.31583F);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.Text = "Color";
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel12});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Multiline = true;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 1.9524713736364683D;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "getMasterBarcodePrintNewForAPI.Qty")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.ForeColor = System.Drawing.Color.Black;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(143.5085F, 40.81906F);
        this.xrLabel5.Multiline = true;
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(106.8112F, 21.31586F);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(12.33466F, 26.81905F);
        this.xrLabel12.Multiline = true;
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(131.1739F, 40.31581F);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.Text = "Quantity :";
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17,
            this.xrLabel13});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Multiline = true;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 1.7330403127969742D;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Arial", 6F);
        this.xrLabel17.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(11.03021F, 43.58438F);
        this.xrLabel17.Multiline = true;
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(151.0101F, 18.99998F);
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseForeColor = false;
        this.xrLabel17.Text = "Inc. of all taxes";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(11.03021F, 10.59305F);
        this.xrLabel13.Multiline = true;
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(130.9438F, 29.9913F);
        this.xrLabel13.StylePriority.UseBorders = false;
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.Text = "MRP";
        // 
        // formattingRule1
        // 
        this.formattingRule1.Condition = "[Barcode]==null";
        this.formattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
        this.formattingRule1.Name = "formattingRule1";
        // 
        // parameter5
        // 
        this.parameter5.Description = "Parameter5";
        this.parameter5.Name = "parameter5";
        // 
        // parameter4
        // 
        this.parameter4.Description = "Parameter4";
        this.parameter4.Name = "parameter4";
        // 
        // parameter1
        // 
        this.parameter1.Name = "parameter1";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 21.125F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.StylePriority.UsePadding = false;
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // sqlDataSource1
        // 
        this.sqlDataSource1.ConnectionName = "myconread";
        this.sqlDataSource1.Name = "sqlDataSource1";
        storedProcQuery1.Name = "getMasterBarcodePrintNewForAPI";
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
        storedProcQuery1.StoredProcName = "getMasterBarcodePrintNewForAPI";
        this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
        // 
        // parameter2
        // 
        this.parameter2.Name = "parameter2";
        this.parameter2.Type = typeof(int);
        this.parameter2.ValueInfo = "0";
        // 
        // parameter3
        // 
        this.parameter3.Description = "Parameter3";
        this.parameter3.Name = "parameter3";
        // 
        // MasterCeilingFanEANPrint2
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
        this.DataMember = "getMasterBarcodePrintNewForAPI";
        this.DataSource = this.sqlDataSource1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(0, 21, 0, 21);
        this.PageHeight = 750;
        this.PageWidth = 1246;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5});
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "18.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
