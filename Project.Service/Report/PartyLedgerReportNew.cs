using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for PartyLedgerReport
/// </summary>
public class PartyLedgerReportNew : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLine xrLine2;
    private XRLine xrLine1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel17;
    private XRLine xrLine10;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLine xrLine8;
    private XRLine xrLine7;
    private XRLine xrLine6;
    private XRLine xrLine5;
    private XRLine xrLine4;
    private XRLine xrLine3;
    private XRLine xrLine9;
    private XRCrossBandLine xrCrossBandLine1;
    private DevExpress.XtraReports.Parameters.Parameter parameter1;
    private DevExpress.XtraReports.Parameters.Parameter parameter4;
    private DevExpress.XtraReports.Parameters.Parameter parameter5;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRLabel xrLabel18;
    private XRLabel xrLabel27;
    private XRLabel xrLabel26;
    private XRLabel xrLabel28;
    private XRPageInfo xrPageInfo2;
    private CalculatedField calculatedField1;
    private CalculatedField calculatedField2;
    private CalculatedField calculatedField3;
    private XRPageInfo xrPageInfo1;
    private CalculatedField calculatedField4;
    private CalculatedField calculatedField5;
    private XRLine xrLine11;
    private XRLabel xrLabel19;
    private FormattingRule formattingRule1;
    private FormattingRule formattingRule2;
    private FormattingRule formattingRule3;
    private DevExpress.XtraReports.Parameters.Parameter parameter6;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel34;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel4;
    private DevExpress.XtraReports.Parameters.Parameter parameter2;
    public double totalCredits = 0;
    double pack = 15;
    private XRLabel xrLabel30;
    private XRLabel xrLabel7;
    private XRLabel xrLabel29;
    private XRLabel xrLabel36;
    private XRLabel xrLabel32;
    private XRLabel xrLabel31;
    private FormattingRule formattingRule4;
    private FormattingRule formattingRule5;
    private FormattingRule formattingRule6;
    private DevExpress.XtraReports.Parameters.Parameter parameter7;
    private DevExpress.XtraReports.Parameters.Parameter parameter3;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

	public PartyLedgerReportNew()
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
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
            //string resourceFileName = "PartyLedgerReportNew.resx";
        //System.Resources.ResourceManager resources = global::Resources.PartyLedgerReportNew.ResourceManager;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartyLedgerReportNew));
        this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule6 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule4 = new DevExpress.XtraReports.UI.FormattingRule();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.parameter4 = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.parameter5 = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine11 = new DevExpress.XtraReports.UI.XRLine();
            this.formattingRule3 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine10 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine8 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine9 = new DevExpress.XtraReports.UI.XRLine();
            this.xrCrossBandLine1 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.parameter7 = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule2 = new DevExpress.XtraReports.UI.FormattingRule();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField3 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField4 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField5 = new DevExpress.XtraReports.UI.CalculatedField();
            this.parameter6 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.formattingRule5 = new DevExpress.XtraReports.UI.FormattingRule();
            this.parameter3 = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel20});
            this.Detail.HeightF = 23F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.Credit", "{0:#.00}")});
            this.xrLabel25.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel25.FormattingRules.Add(this.formattingRule1);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(665.0001F, 0F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "xrLabel25";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "[Credit]=0.00";
            this.formattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // xrLabel24
            // 
            this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.creditparticular")});
            this.xrLabel24.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(444.5102F, 0F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(208.6629F, 23F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "xrLabel24";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.creditdate", "{0:dd/MM/yyyy}")});
            this.xrLabel23.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(386.0338F, 0F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(57.49103F, 23F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "xrLabel23";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel22
            // 
            this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.Debit", "{0:#.00}")});
            this.xrLabel22.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel22.FormattingRules.Add(this.formattingRule6);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(290.4036F, 0F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(87.63019F, 23F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "xrLabel22";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // formattingRule6
            // 
            this.formattingRule6.Condition = "[Debit]=0.00";
            this.formattingRule6.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule6.Name = "formattingRule6";
            // 
            // xrLabel21
            // 
            this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.debitparticular")});
            this.xrLabel21.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(80.91864F, 0F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(141.6485F, 23F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "xrLabel21";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel20
            // 
            this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.debitdate", "{0:dd/MM/yyyy}")});
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(12F, 0F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(55.88235F, 23F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "xrLabel20";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // formattingRule4
            // 
            this.formattingRule4.Condition = "[Debit]=0.00";
            this.formattingRule4.DataMember = "partyledgerprintNew";
            this.formattingRule4.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule4.Name = "formattingRule4";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 25F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 25.00001F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(366.25F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.RunningBand = this.GroupHeader1;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(27.08316F, 15.00001F);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel34,
            this.xrLabel1,
            this.xrLabel18,
            this.xrLabel19,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel6,
            this.xrLabel26,
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel14,
            this.xrPageInfo2,
            this.xrLabel5,
            this.xrLine1,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLabel8,
            this.xrLine2,
            this.xrLine11});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PartyName", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 204.4321F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.GroupHeader1.StylePriority.UsePadding = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "branchshow.add5")});
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(495.4682F, 20.20832F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(273.6829F, 40.62503F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.Text = "xrLabel4";
            // 
            // xrLabel34
            // 
            this.xrLabel34.CanGrow = false;
            this.xrLabel34.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(12.15138F, 123.13F);
            this.xrLabel34.Multiline = true;
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(756.9999F, 60.29167F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.Text = resources.GetString("xrLabel34.Text");
            // 
            // xrLabel1
            // 
            this.xrLabel1.CanGrow = false;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(11F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(32.29167F, 23F);
            this.xrLabel1.Text = "To :";
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.PartyName")});
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(43.2916F, 0F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(348.0416F, 23F);
            this.xrLabel18.Text = "xrLabel18";
            // 
            // xrLabel19
            // 
            this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.addline1")});
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(11F, 23.00001F);
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(380.3332F, 37.83334F);
            this.xrLabel19.Text = "xrLabel19";
            // 
            // xrLabel2
            // 
            this.xrLabel2.CanGrow = false;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(393.3332F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(40.96042F, 23F);
            this.xrLabel2.Text = "From :";
            // 
            // xrLabel3
            // 
            this.xrLabel3.CanGrow = false;
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(495.4682F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(194.0833F, 20.20833F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "Goldmedal Electricals Pvt. Ltd.";
            // 
            // xrLabel6
            // 
            this.xrLabel6.CanGrow = false;
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(302.2492F, 71.16169F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(186.2561F, 20.20834F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Sub: Confirmation of Accounts";
            // 
            // xrLabel26
            // 
            this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.parameter4, "Text", "{0:dd/MM/yyyy}")});
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(315.5033F, 91.37002F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(74.82993F, 23F);
            this.xrLabel26.StylePriority.UsePadding = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "xrLabel26";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // parameter4
            // 
            this.parameter4.Name = "parameter4";
            this.parameter4.Type = typeof(System.DateTime);
            this.parameter4.Visible = false;
            // 
            // xrLabel28
            // 
            this.xrLabel28.CanGrow = false;
            this.xrLabel28.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(390.3332F, 91.37004F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(26.76184F, 23.00001F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.StylePriority.UsePadding = false;
            this.xrLabel28.StylePriority.UseTextAlignment = false;
            this.xrLabel28.Text = "To ";
            this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.parameter5, "Text", "{0:dd/MM/yyyy}")});
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(417.0951F, 91.37004F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(71.41022F, 23F);
            this.xrLabel27.StylePriority.UsePadding = false;
            this.xrLabel27.Text = "xrLabel27";
            // 
            // parameter5
            // 
            this.parameter5.Name = "parameter5";
            this.parameter5.Type = typeof(System.DateTime);
            this.parameter5.Visible = false;
            // 
            // xrLabel14
            // 
            this.xrLabel14.CanGrow = false;
            this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(575.3455F, 91.37002F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(52.08331F, 23F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "Dated :";
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(627.4288F, 91.37004F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel5
            // 
            this.xrLabel5.CanGrow = false;
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(12.15138F, 91.37004F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(107.2917F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "Dear Sir/Madam,";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(12.15111F, 183.4216F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine1.SizeF = new System.Drawing.SizeF(757.0001F, 2F);
            this.xrLine1.StylePriority.UsePadding = false;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(113.849F, 185.4216F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(70.44271F, 17.01041F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "Particulars";
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(292.5547F, 185.4216F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(84.94429F, 17.01041F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.Text = "Debit Amount";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(385.9616F, 185.4216F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(33.07291F, 17.01041F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Date";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(443.6613F, 185.4216F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(70.44271F, 17.01041F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "Particulars";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(681.0707F, 185.4216F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(88.08038F, 17.01041F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "Credit Amount";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(12.15111F, 185.4216F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(33.07291F, 17.01041F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "Date";
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(12.15111F, 202.4321F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine2.SizeF = new System.Drawing.SizeF(757.0001F, 2F);
            this.xrLine2.StylePriority.UsePadding = false;
            // 
            // xrLine11
            // 
            this.xrLine11.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine11.LocationFloat = new DevExpress.Utils.PointFloat(378.1849F, 185.4216F);
            this.xrLine11.Name = "xrLine11";
            this.xrLine11.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine11.SizeF = new System.Drawing.SizeF(2F, 17.01039F);
            this.xrLine11.StylePriority.UsePadding = false;
            // 
            // formattingRule3
            // 
            this.formattingRule3.Condition = "[calculatedField1]=0.00";
            this.formattingRule3.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule3.Name = "formattingRule3";
            // 
            // xrLabel17
            // 
            this.xrLabel17.CanGrow = false;
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(524.1097F, 15.92334F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(103.3192F, 12.52793F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "Closing Balance";
            // 
            // xrLine10
            // 
            this.xrLine10.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine10.LocationFloat = new DevExpress.Utils.PointFloat(378.5249F, 0F);
            this.xrLine10.Name = "xrLine10";
            this.xrLine10.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine10.SizeF = new System.Drawing.SizeF(2F, 59.31566F);
            this.xrLine10.StylePriority.UsePadding = false;
            // 
            // xrLabel16
            // 
            this.xrLabel16.CanGrow = false;
            this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(639.2317F, 61.31568F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(103.3192F, 13.52526F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "Yours Faithfully,";
            // 
            // xrLabel15
            // 
            this.xrLabel15.CanGrow = false;
            this.xrLabel15.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(10.99999F, 62.31301F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(150.4139F, 82.07799F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.Text = "I/We hereby confirm the above";
            // 
            // xrLine8
            // 
            this.xrLine8.LocationFloat = new DevExpress.Utils.PointFloat(619.9442F, 50.45127F);
            this.xrLine8.Name = "xrLine8";
            this.xrLine8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine8.SizeF = new System.Drawing.SizeF(148.6057F, 2.000015F);
            this.xrLine8.StylePriority.UsePadding = false;
            // 
            // xrLine7
            // 
            this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(619.3424F, 28.92336F);
            this.xrLine7.Name = "xrLine7";
            this.xrLine7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine7.SizeF = new System.Drawing.SizeF(148.6057F, 2.000013F);
            this.xrLine7.StylePriority.UsePadding = false;
            // 
            // xrLine6
            // 
            this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(620.3425F, 0F);
            this.xrLine6.Name = "xrLine6";
            this.xrLine6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine6.SizeF = new System.Drawing.SizeF(148.2074F, 2F);
            this.xrLine6.StylePriority.UsePadding = false;
            // 
            // xrLine5
            // 
            this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(228.3056F, 49.45133F);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine5.SizeF = new System.Drawing.SizeF(150.2193F, 2.000004F);
            this.xrLine5.StylePriority.UsePadding = false;
            // 
            // xrLine4
            // 
            this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(229.3057F, 28.92337F);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine4.SizeF = new System.Drawing.SizeF(149.2191F, 2F);
            this.xrLine4.StylePriority.UsePadding = false;
            // 
            // xrLine3
            // 
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(229.3057F, 0F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine3.SizeF = new System.Drawing.SizeF(149.2192F, 2F);
            this.xrLine3.StylePriority.UsePadding = false;
            // 
            // xrLine9
            // 
            this.xrLine9.LocationFloat = new DevExpress.Utils.PointFloat(10.99999F, 59.31566F);
            this.xrLine9.Name = "xrLine9";
            this.xrLine9.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLine9.SizeF = new System.Drawing.SizeF(757.3985F, 2.000004F);
            this.xrLine9.StylePriority.UsePadding = false;
            // 
            // xrCrossBandLine1
            // 
            this.xrCrossBandLine1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrCrossBandLine1.EndBand = this.GroupFooter1;
            this.xrCrossBandLine1.EndPointFloat = new DevExpress.Utils.PointFloat(378.7074F, 0F);
            this.xrCrossBandLine1.LocationFloat = new DevExpress.Utils.PointFloat(378.7074F, 0F);
            this.xrCrossBandLine1.Name = "xrCrossBandLine1";
            this.xrCrossBandLine1.StartBand = this.Detail;
            this.xrCrossBandLine1.StartPointFloat = new DevExpress.Utils.PointFloat(378.7074F, 0F);
            this.xrCrossBandLine1.WidthF = 1F;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel36,
            this.xrLabel29,
            this.xrLabel7,
            this.xrLabel30,
            this.xrLine3,
            this.xrLine4,
            this.xrLine5,
            this.xrLine10,
            this.xrLine6,
            this.xrLine7,
            this.xrLine8,
            this.xrLabel17,
            this.xrLine9,
            this.xrLabel16,
            this.xrLabel15});
            this.GroupFooter1.HeightF = 153.391F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            this.GroupFooter1.PrintAtBottom = true;
            // 
            // xrLabel32
            // 
            this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.parameter7, "Text", "")});
            this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 7.5F);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(664.948F, 17.92336F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel32.Scripts.OnBeforePrint = "xrLabel32_BeforePrint";
            this.xrLabel32.Scripts.OnSummaryGetResult = "xrLabel32_SummaryGetResult";
            this.xrLabel32.SizeF = new System.Drawing.SizeF(100F, 10.5279F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UsePadding = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#.00}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel32.Summary = xrSummary1;
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // parameter7
            // 
            this.parameter7.Description = "Parameter7";
            this.parameter7.Name = "parameter7";
            this.parameter7.Type = typeof(decimal);
            this.parameter7.ValueInfo = "0";
            // 
            // xrLabel31
            // 
            this.xrLabel31.Font = new System.Drawing.Font("Times New Roman", 7.5F);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(278.499F, 17.92336F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel31.Scripts.OnBeforePrint = "xrLabel31_BeforePrint";
            this.xrLabel31.Scripts.OnSummaryGetResult = "xrLabel31_SummaryGetResult";
            this.xrLabel31.SizeF = new System.Drawing.SizeF(100F, 10.53F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.StylePriority.UsePadding = false;
            this.xrLabel31.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#.00}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel31.Summary = xrSummary2;
            this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(665.0001F, 30.92338F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel36.Scripts.OnBeforePrint = "xrLabel29_BeforePrint";
            this.xrLabel36.Scripts.OnSummaryCalculated = "xrLabel29_SummaryCalculated";
            this.xrLabel36.Scripts.OnSummaryGetResult = "xrLabel29_SummaryGetResult";
            this.xrLabel36.Scripts.OnSummaryReset = "xrLabel29_SummaryReset";
            this.xrLabel36.SizeF = new System.Drawing.SizeF(100F, 18.53F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.StylePriority.UsePadding = false;
            this.xrLabel36.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#.00}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel36.Summary = xrSummary3;
            this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(278.5131F, 30.92337F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel29.Scripts.OnBeforePrint = "xrLabel29_BeforePrint";
            this.xrLabel29.Scripts.OnSummaryCalculated = "xrLabel29_SummaryCalculated";
            this.xrLabel29.Scripts.OnSummaryGetResult = "xrLabel29_SummaryGetResult";
            this.xrLabel29.Scripts.OnSummaryReset = "xrLabel29_SummaryReset";
            this.xrLabel29.SizeF = new System.Drawing.SizeF(99.99998F, 18.52792F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UsePadding = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#.00}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel29.Summary = xrSummary4;
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.Debit", "{0:#.00}")});
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(278.499F, 2.923371F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel7.Scripts.OnAfterPrint = "xrLabel7_AfterPrint";
            this.xrLabel7.Scripts.OnSummaryGetResult = "xrLabel7_SummaryGetResult";
            this.xrLabel7.Scripts.OnSummaryReset = "xrLabel7_SummaryReset";
            this.xrLabel7.Scripts.OnSummaryRowChanged = "xrLabel7_SummaryRowChanged";
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 14.99999F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:#.00}";
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary5.IgnoreNullValues = true;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel7.Summary = xrSummary5;
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel30
            // 
            this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "partyledgerprintNew.Credit", "{0:#.00}")});
            this.xrLabel30.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(664.948F, 2.923362F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel30.Scripts.OnAfterPrint = "xrLabel30_AfterPrint";
            this.xrLabel30.Scripts.OnSummaryGetResult = "xrLabel30_SummaryGetResult";
            this.xrLabel30.Scripts.OnSummaryReset = "xrLabel30_SummaryReset";
            this.xrLabel30.Scripts.OnSummaryRowChanged = "xrLabel30_SummaryRowChanged";
            this.xrLabel30.SizeF = new System.Drawing.SizeF(100.0521F, 14.99999F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.StylePriority.UsePadding = false;
            this.xrLabel30.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:#.00}";
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary6.IgnoreNullValues = true;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel30.Summary = xrSummary6;
            this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // formattingRule2
            // 
            this.formattingRule2.Condition = "[calculatedField5]=0.00";
            this.formattingRule2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule2.Name = "formattingRule2";
            // 
            // parameter1
            // 
            this.parameter1.Name = "parameter1";
            this.parameter1.Type = typeof(int);
            this.parameter1.ValueInfo = "0";
            this.parameter1.Visible = false;
            // 
            // calculatedField1
            // 
            this.calculatedField1.DataMember = "partyledgerprintNew";
            this.calculatedField1.Expression = "[calculatedField4] - [calculatedField3]";
            this.calculatedField1.FieldType = DevExpress.XtraReports.UI.FieldType.Float;
            this.calculatedField1.Name = "calculatedField1";
            // 
            // calculatedField2
            // 
            this.calculatedField2.DataMember = "partyledgerprintNew";
            this.calculatedField2.Expression = "[].Sum([Debit])";
            this.calculatedField2.FieldType = DevExpress.XtraReports.UI.FieldType.Float;
            this.calculatedField2.Name = "calculatedField2";
            // 
            // calculatedField3
            // 
            this.calculatedField3.DataMember = "partyledgerprintNew";
            this.calculatedField3.Expression = "Iif([].Sum([Credit])==0, \'0.00\' ,[].Sum([Credit]) )";
            this.calculatedField3.FieldType = DevExpress.XtraReports.UI.FieldType.Float;
            this.calculatedField3.Name = "calculatedField3";
            // 
            // calculatedField4
            // 
            this.calculatedField4.DataMember = "partyledgerprintNew";
            this.calculatedField4.Expression = "Iif([calculatedField2] >[calculatedField3] , [calculatedField2] ,[calculatedField" +
    "3] )";
            this.calculatedField4.FieldType = DevExpress.XtraReports.UI.FieldType.Float;
            this.calculatedField4.Name = "calculatedField4";
            // 
            // calculatedField5
            // 
            this.calculatedField5.DataMember = "partyledgerprintNew";
            this.calculatedField5.Expression = "[calculatedField4] -  [calculatedField2]";
            this.calculatedField5.FieldType = DevExpress.XtraReports.UI.FieldType.Float;
            this.calculatedField5.Name = "calculatedField5";
            // 
            // parameter6
            // 
            this.parameter6.Description = "Parameter6";
            this.parameter6.Name = "parameter6";
            this.parameter6.Type = typeof(int);
            this.parameter6.ValueInfo = "0";
            this.parameter6.Visible = false;
            // 
            // parameter2
            // 
            this.parameter2.Name = "parameter2";
            this.parameter2.Type = typeof(int);
            this.parameter2.ValueInfo = "0";
            this.parameter2.Visible = false;
            // 
            // formattingRule5
            // 
            this.formattingRule5.Name = "formattingRule5";
            // 
            // parameter3
            // 
            this.parameter3.Description = "Parameter3";
            this.parameter3.Name = "parameter3";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "myconread";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "partyledgerprintNew";
            queryParameter1.Name = "@cin";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter3]", typeof(string));
            queryParameter2.Name = "@fromdate";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter4]", typeof(System.DateTime));
            queryParameter3.Name = "@todate";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.parameter5]", typeof(System.DateTime));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.StoredProcName = "partyledgerprintNew";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // PartyLedgerReportNew
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.calculatedField2,
            this.calculatedField3,
            this.calculatedField4,
            this.calculatedField5});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandLine1});
            this.DataMember = "partyledgerprintNew";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1,
            this.formattingRule2,
            this.formattingRule3,
            this.formattingRule4,
            this.formattingRule5,
            this.formattingRule6});
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 25, 25);
            this.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter4,
            this.parameter5,
            this.parameter6,
            this.parameter2,
            this.parameter7,
            this.parameter3});
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

   

    //private void xrLabel7_SummaryReset(object sender, EventArgs e)
    //{
    //   // totalCredits = 0;
    //}

    //private void xrLabel7_SummaryRowChanged(object sender, EventArgs e)
    //{
    //   // totalCredits += Convert.ToDouble(GetCurrentColumnValue("[Credit]"));
    //}
    //private void xrLabel7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    //{
    //    // Round the result, so that a pack will be taken into account 
    //    // even if it contains only one unit.

    //    //if (totalCredits == 0)
    //    //    totalCredits = 0.00;
    //    //e.Result = totalCredits;
    //    //e.Handled = true;
    //}
}
