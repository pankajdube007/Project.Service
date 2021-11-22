using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Project.Service.Controllers
{
    public class EInvoiceGenerateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EInvoiceGenerate")]
        public HttpResponseMessage GetDetails(ListsofEInvoiceGenerate ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            var key = new EinvoiceKey();
            var data = string.Empty;
            if (ula.userid != 0 && ula.slno != 0)
            {
                try
                {
                    List<EInvoiceGenerateS> head = new List<EInvoiceGenerateS>();
                    List<EInvoiceGenerateItemList> child = new List<EInvoiceGenerateItemList>();
                    List<EInvoice> ewaybill = new List<EInvoice>();
                    List<EInvoices> result = new List<EInvoices>();


                    var dr3 = g2.return_dt("EInvoiceDatabySlno " + ula.slno);


                    if (dr3.Rows.Count > 0)
                    {

                        //key = cm.GetEInvoiceTokanno(dr3.Rows[0]["GSTUser"].ToString(), dr3.Rows[0]["GSTPassword"].ToString(), ConfigurationManager.AppSettings["Gold.Eway.AppId"].ToString(), ConfigurationManager.AppSettings["Gold.Eway.AppSecret"].ToString());
                      //  key = cm.GetEInvoiceTokanno(dr3.Rows[0]["GSTUser"].ToString(), dr3.Rows[0]["GSTPassword"].ToString(), dr3.Rows[0]["ClientID"].ToString(), dr3.Rows[0]["ClientSecret"].ToString());
                           key = cm.GetEInvoiceTokanno("goldluein", "111@Abc@", "AACCG09TXPMK3F7", "Ppd4J8XLl2zFeTshMBa1");

                        if (!string.IsNullOrEmpty(key.AuthToken) && !string.IsNullOrEmpty(key.Sek))
                        {

                            if (!String.IsNullOrEmpty(dr3.Rows[0]["SellerDtlsPin"].ToString()) && !String.IsNullOrEmpty(dr3.Rows[0]["BuyerDtlsPin"].ToString()))
                            {


                                //if (!String.IsNullOrEmpty(dr3.Rows[0]["EwbDtlsTransId"].ToString()) && !String.IsNullOrEmpty(dr3.Rows[0]["EwbDtlsTransName"].ToString()) && distance != 0)
                                //{
                                var dr4 = g2.return_dt("EInvoiceDataChildbySlno " + ula.slno);
                                for (int i = 0; i < dr4.Rows.Count; i++)

                                {
                                    List<AttribDtl> abt = new List<AttribDtl>();
                                    
                                    // for production

                                    abt.Add(new AttribDtl
                                    {
                                        Nm = dr4.Rows[i]["PrdDesc"].ToString(),
                                        Val = dr4.Rows[i]["TotAmt"].ToString()
                                    });

                                    child.Add(new EInvoiceGenerateItemList
                                    {
                                        SlNo = dr4.Rows[i]["PrdSlNo"].ToString(),
                                        PrdDesc = dr4.Rows[i]["PrdDesc"].ToString(),
                                        IsServc = dr4.Rows[i]["IsServc"].ToString(),
                                        HsnCd = dr4.Rows[i]["HsnCd"].ToString(),
                                        Barcde = dr4.Rows[i]["Barcde"].ToString().ToUpper(),
                                        Qty = Convert.ToDecimal(dr4.Rows[i]["Qty"]),
                                        FreeQty = Convert.ToInt32(dr4.Rows[i]["FreeQty"].ToString()),
                                        Unit = dr4.Rows[i]["Unit"].ToString(),
                                        UnitPrice = Convert.ToDecimal(dr4.Rows[i]["UnitPrice"]),
                                        TotAmt = Convert.ToDecimal(dr4.Rows[i]["TotAmt"]),
                                        Discount = Convert.ToInt32(dr4.Rows[i]["Discount"]),
                                        // PreTaxVal = Convert.ToInt32(dr4.Rows[i]["TotAmt"]),
                                        PreTaxVal = 1,
                                        AssAmt = Convert.ToDecimal(dr4.Rows[i]["TotAmt"]) - Convert.ToDecimal(dr4.Rows[i]["Discount"]),
                                        GstRt = Convert.ToDecimal(dr4.Rows[i]["GstRt"]),
                                        IgstAmt = Convert.ToDecimal(dr4.Rows[i]["IgstAmt"]),
                                        CgstAmt = Convert.ToDecimal(dr4.Rows[i]["SgstAmt"]),
                                        SgstAmt = Convert.ToDecimal(dr4.Rows[i]["CgstAmt"]),
                                        CesRt = Convert.ToInt32(dr4.Rows[i]["CesRt"]),
                                        CesAmt = Convert.ToInt32(dr4.Rows[i]["CesAmt"]),
                                        CesNonAdvlAmt = Convert.ToInt32(dr4.Rows[i]["CesNonAdvlAmt"]),
                                        StateCesRt = Convert.ToInt32(dr4.Rows[i]["StateCesAmt"]),
                                        StateCesNonAdvlAmt = Convert.ToInt32(dr4.Rows[i]["StateCesNonAdvlAmt"]),
                                        OthChrg = Convert.ToInt32(dr4.Rows[i]["OthChrg"]),
                                        TotItemVal = Convert.ToDecimal(dr4.Rows[i]["TotItemVal"]),
                                        OrdLineRef = dr4.Rows[i]["OrdLineRef"].ToString(),
                                        OrgCntry = dr4.Rows[i]["OrgCntry"].ToString(),
                                        PrdSlNo = dr4.Rows[i]["PrdSlNo"].ToString(),
                                        BchDtls = new BchDtls
                                        {
                                            Nm = dr4.Rows[i]["PrdDesc"].ToString(),
                                            ExpDt = dr4.Rows[i]["BchDtlsExpDt"].ToString(),
                                            WrDt = dr4.Rows[i]["BchDtlsWrpDt"].ToString(),
                                        },
                                        AttribDtls = abt


                                    });
                                }





                                // var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno);
                                if (dr3.Rows.Count > 0)
                                {

                                    var data1 = new EInvoiceGenerateS
                                    {

                                        // for Production //
                                        Version = "1.03",


                                        TranDtls = new TranDtls
                                        {
                                            TaxSch = "GST",
                                            SupTyp = "B2B",
                                            RegRev = "Y",
                                            EcmGstin = null,
                                            IgstOnIntra = "N"
                                        },

                                        DocDtls = new DocDtls
                                        {
                                            Typ = dr3.Rows[0]["DocDtlsTypDocDtls"].ToString(),
                                            No = dr3.Rows[0]["DocDtlsNo"].ToString(),
                                            Dt = dr3.Rows[0]["DocDtlsDt"].ToString(),
                                        },

                                        SellerDtls = new SellerDtls
                                        {
                                            // for test

                                            //  Gstin = "09AACCG9397F1Z4",


                                            Gstin = dr3.Rows[0]["SellerDtlsGstin"].ToString(),
                                            LglNm = dr3.Rows[0]["SellerDtlsLglNm"].ToString(),
                                            TrdNm = dr3.Rows[0]["SellerDtlsTrdNm"].ToString(),
                                            Addr1 = dr3.Rows[0]["SellerDtlsAddr1"].ToString(),
                                            Addr2 = string.IsNullOrEmpty(dr3.Rows[0]["SellerDtlsAddr2"].ToString().Trim()) ? "Not Applicable" : dr3.Rows[0]["SellerDtlsAddr2"].ToString(),
                                            Loc = dr3.Rows[0]["SellerDtlsLoc"].ToString(),
                                            Pin = Convert.ToInt32(dr3.Rows[0]["SellerDtlsPin"].ToString()),
                                            Stcd = dr3.Rows[0]["SellerDtlsStcd"].ToString(),
                                            Ph = dr3.Rows[0]["SellerDtlsPh"].ToString(),
                                            Em = dr3.Rows[0]["SellerDtlsEm"].ToString()
                                        },

                                        BuyerDtls = new BuyerDtls
                                        {
                                            Gstin = dr3.Rows[0]["BuyerDtlsGstin"].ToString(),
                                            LglNm = dr3.Rows[0]["BuyerDtlsLglNm"].ToString(),
                                            TrdNm = dr3.Rows[0]["BuyerDtlsTrdNm"].ToString(),
                                            Pos = dr3.Rows[0]["BuyerDtlsStcd"].ToString(),
                                            Addr1 = dr3.Rows[0]["BuyerDtlsAddr1"].ToString(),
                                            Addr2 = string.IsNullOrEmpty(dr3.Rows[0]["BuyerDtlsAddr2"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["BuyerDtlsAddr2"].ToString(),
                                            Loc = dr3.Rows[0]["BuyerDtlsLoc"].ToString(),
                                            Pin = Convert.ToInt32(dr3.Rows[0]["BuyerDtlsPin"].ToString()),
                                            Stcd = dr3.Rows[0]["BuyerDtlsStcd"].ToString(),
                                            Ph = dr3.Rows[0]["BuyerDtlsPh"].ToString(),
                                            Em = dr3.Rows[0]["BuyerDtlsEm"].ToString()
                                        },

                                        DispDtls = new DispDtls
                                        {
                                            Nm = dr3.Rows[0]["DispDtlsNm"].ToString(),
                                            Addr1 = dr3.Rows[0]["DispDtlsAddr1"].ToString(),
                                            Addr2 = string.IsNullOrEmpty(dr3.Rows[0]["DispDtlsAddr2"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["DispDtlsAddr2"].ToString(),
                                            Loc = dr3.Rows[0]["DispDtlsLoc"].ToString(),
                                            Pin = Convert.ToInt32(dr3.Rows[0]["DispDtlsPin"].ToString()),
                                            Stcd = dr3.Rows[0]["DispDtlsStcd"].ToString()
                                        },
                                        //   ShipDtls = new ShipDtls(),
                                        ShipDtls = new ShipDtls
                                        {
                                            Gstin = dr3.Rows[0]["ShipDtlsGstin"].ToString(),
                                            LglNm = dr3.Rows[0]["ShipDtlsLglNm"].ToString(),
                                            TrdNm = dr3.Rows[0]["ShipDtlsTrdNm"].ToString(),
                                            Addr1 = Convert.ToString(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsAddr1"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["ShipDtlsAddr1"].ToString()),
                                            Addr2 = Convert.ToString(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsAddr2"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["ShipDtlsAddr2"].ToString()),
                                            Loc = dr3.Rows[0]["ShipDtlsLoc"].ToString(),
                                            Pin = Convert.ToInt32(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsPin"].ToString()) ? "0000" : dr3.Rows[0]["ShipDtlsPin"].ToString()),
                                            Stcd = dr3.Rows[0]["ShipDtlsStcd"].ToString()
                                        },


                                        ItemList = child,

                                        //ValDtls = new ValDtls
                                        //{
                                        //    AssVal = 9978.84,
                                        //    CgstVal = 599,
                                        //    SgstVal = 599,
                                        //    IgstVal = 0,
                                        //    CesVal = 508.94,
                                        //    StCesVal = 1202.46,
                                        //    Discount = 10,
                                        //    OthChrg = 20,
                                        //    RndOffAmt = 0.3,
                                        //    TotInvVal = 12908,
                                        //    TotInvValFc = 12897.7

                                        //},


                                        // for production

                                        ValDtls = new ValDtls
                                        {
                                            AssVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsAssVal"]),
                                            CgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsCgstVal"]),
                                            SgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsSgstVal"]),
                                            IgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsIgstVal"]),
                                            CesVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsCesVal"]),
                                            StCesVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsStCesVal"]),
                                            //   Discount = Convert.ToInt32(dr3.Rows[0]["ValDtlsDiscount"]),
                                            Discount = 0,
                                            OthChrg = Convert.ToInt32(dr3.Rows[0]["ValDtlsOthChrg"]),
                                            RndOffAmt = Convert.ToDecimal(dr3.Rows[0]["ValDtlsRndOffAmt"]),
                                            //  TotInvVal = Convert.ToInt32(dr3.Rows[0]["ValDtlsTotInvVal"])+ Convert.ToInt32(dr3.Rows[0]["ValDtlsDiscount"]),
                                            TotInvVal = Convert.ToInt32(dr3.Rows[0]["ValDtlsTotInvVal"]),
                                            TotInvValFc = Convert.ToDecimal(dr3.Rows[0]["ValDtlsTotInvValFc"])

                                        },


                                        PayDtls = new PayDtls(),

                                        //PayDtls = new PayDtls
                                        //{
                                        //    Nm = dr3.Rows[0]["PayDtlsName"].ToString(),
                                        //    AccDet = dr3.Rows[0]["PayDtlsAccDet"].ToString(),
                                        //    Mode = dr3.Rows[0]["PayDtlsMode"].ToString(),
                                        //    FinInsBr = dr3.Rows[0]["PayDtlsFinInsBr"].ToString(),
                                        //    PayTerm = dr3.Rows[0]["PayDtlsPayTerm"].ToString(),
                                        //    PayInstr = dr3.Rows[0]["PayDtlsPayInstr"].ToString(),
                                        //    CrTrn = dr3.Rows[0]["PayDtlsCrTrn"].ToString(),
                                        //    DirDr = dr3.Rows[0]["PayDtlsDirDr"].ToString(),
                                        //    CrDay = Convert.ToInt32(dr3.Rows[0]["PayDtlsCrDay"]),
                                        //    PaidAmt = Convert.ToInt32(dr3.Rows[0]["PayDtlsPaidAmt"]),
                                        //    PaymtDue = Convert.ToInt32(dr3.Rows[0]["PayDtlsPaymtDue"]),
                                        //},

                                        RefDtls = null,
                                        AddlDocDtls = null,
                                        ExpDtls = new ExpDtls(),
                                        //ExpDtls = new ExpDtls
                                        //{
                                        //    ShipBNo = dr3.Rows[0]["ExpDtlsShipBNo"].ToString(),
                                        //    ShipBDt = dr3.Rows[0]["ExpDtlsShipBDt"].ToString(),
                                        //    Port = dr3.Rows[0]["ExpDtlsPort"].ToString(),
                                        //    RefClm = dr3.Rows[0]["ExpDtlsRefClm"].ToString(),
                                        //    ForCur = dr3.Rows[0]["ExpDtlsForCur"].ToString(),
                                        //    CntCode = dr3.Rows[0]["ExpDtlsCntCode"].ToString(),
                                        //    ExpDuty = null
                                        //},
                                        EwbDtls = new EwbDtls()

                                        //EwbDtls = new EwbDtls
                                        //{
                                        //    TransId = dr3.Rows[0]["EwbDtlsTransId"].ToString(),
                                        //    TransName = dr3.Rows[0]["EwbDtlsTransName"].ToString(),
                                        //    Distance = Convert.ToInt32(distance),
                                        //    TransDocNo = dr3.Rows[0]["EwbDtlsTransDocNo"].ToString(),
                                        //    TransDocDt = dr3.Rows[0]["EwbDtlsTransDocDt"].ToString(),
                                        //    VehNo = dr3.Rows[0]["EwbDtlsVehNo"].ToString(),
                                        //    VehType = "R",
                                        //    TransMode = "1"
                                        //}
                                    };


                                    var baseurl = ConfigurationManager.AppSettings["Gold.Invoice.API"] + "eicore/v1.03/Invoice";
                                    System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                                    // for production //
                                    //request1.Method = "POST";
                                    //request1.ContentType = "application/json";
                                    //request1.Headers.Add("client_id", dr3.Rows[0]["ClientID"].ToString());
                                    //request1.Headers.Add("client_secret", dr3.Rows[0]["ClientSecret"].ToString());
                                    //request1.Headers.Add("gstin", dr3.Rows[0]["SellerDtlsGstin"].ToString());
                                    //request1.Headers.Add("user_name", dr3.Rows[0]["GSTUser"].ToString());
                                    //request1.Headers.Add("AuthToken", key.AuthToken);

                                    // for testing //

                                    request1.Method = "POST";
                                    request1.ContentType = "application/json";
                                    request1.Headers.Add("client_id", "AACCG09TXPMK3F7");
                                    request1.Headers.Add("client_secret", "Ppd4J8XLl2zFeTshMBa1");
                                    request1.Headers.Add("gstin", "09AACCG9397F1Z4");
                                    request1.Headers.Add("user_name", "goldup_API_118");
                                    request1.Headers.Add("AuthToken", key.AuthToken);


                                    //Encryt the data


                                    var json1 = new JavaScriptSerializer().Serialize(data1);

                                    var datatocall = cm.EncryptBySymmetricKey(json1, key.Sek);


                                    RootInvoice root = new RootInvoice
                                    {
                                        Data = datatocall
                                    };

                                    //Place the serialized content of the object to be posted into the request stream
                                    using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                    {
                                        var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(root);
                                        streamWriter.Write(json);
                                        streamWriter.Flush();
                                        streamWriter.Close();
                                    }


                                    //  System.Net.HttpWebResponse response1 = null;
                                    System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();




                                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                                    {

                                        dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());

                                        if (_output.Data != "" && _output.Data != null)
                                        {


                                            var jsondata = cm.DecryptBySymmetricKey1(_output.Data.ToString(), key.Sek);
                                            //   var jjjj = (_output.Data.ToString());
                                            //   var jsondata = cm.DecryptBySymmetricKey1( key.Sek, _output.Data.ToString());

                                            //  dynamic _output1 = JsonConvert.DeserializeObject((new StreamReader(jsondata)).ReadToEnd());

                                            ResponseEInvoice flight = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseEInvoice>(jsondata);
                                         //   var signedinvoice = cm.Decode(flight.SignedInvoice.ToString());
                                           // var signedqr = cm.Decode(flight.SignedQRCode.ToString());

                                            var sqlstring = ula.slno + ",'"
                                + flight.AckNo.ToString() + "','"
                                + flight.AckDt.ToString() + "','"
                                + flight.Irn.ToString() + "','"
                                + flight.SignedInvoice.ToString() + "','"
                                + flight.SignedQRCode.ToString() + "'";
                                            //+ flight.Status.ToString() + "','"
                                            //+ flight.EwbNo.ToString() + "','"
                                            //+ flight.EwbDt.ToString() + "','";
                                            //+ flight.EwbValidTill.ToString() + "','"
                                            // +flight.Remarks.ToString() + "'";
                                            //+ ula.userid.ToString();

                                            var dr6 = g2.return_dt("EwayInvoiceupdatebyAPI " + sqlstring

                                );

                                            



                                            if (dr6.Rows.Count > 0)
                                            {
                                                ewaybill.Add(new EInvoice
                                                {
                                                    ewaybillno = flight.AckNo.ToString(),
                                                });

                                                g2.close_connection();
                                                result.Add(new EInvoices
                                                {
                                                    result = true,
                                                    message = string.Empty,
                                                    servertime = DateTime.Now.ToString(),
                                                    data = ewaybill,
                                                });
                                                data = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                                HttpResponseMessage response8 = Request.CreateResponse(HttpStatusCode.OK);

                                                response8.Content = new StringContent(data, Encoding.UTF8, "application/json");

                                                return response8;
                                            }
                                            else
                                            {
                                                HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                response9.Content = new StringContent(cm.StatusTime(false, "Oops! Ewaybill not updated in database!!!!!!!!"), Encoding.UTF8, "application/json");

                                                return response9;

                                            }
                                        }
                                        else
                                        {
                                           // var dr9 = g2.return_dt("EInvoiceinvalidtokan '" + dr3.Rows[0]["GSTUser"].ToString() + "'");

                                             var dr9 = g2.return_dt("EInvoiceinvalidtokan " + "goldluein");
                                            HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                            response9.Content = new StringContent(cm.StatusTime(false, _output.ErrorDetails.ToString()), Encoding.UTF8, "application/json");

                                            return response9;

                                        }
                                    }
                                    else
                                    {
                                        HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                        response9.Content = new StringContent(cm.StatusTime(false, "Oops! Not a sucess Result from API!!!!!!!!"), Encoding.UTF8, "application/json");

                                        return response9;

                                    }


                                }
                                else
                                {
                                    HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                    response9.Content = new StringContent(cm.StatusTime(false, "Oops! No Proper Header or Incorrect Userid and Password!!!!!!!!"), Encoding.UTF8, "application/json");

                                    return response9;

                                }
                                //}
                                //else
                                //{
                                //    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                //    response.Content = new StringContent(cm.StatusTime(false, "Oops! Atleast Transporter Id or Transporter GSTNo,Transporter Name Required and distance, Thats missing!!!!!!!!"), Encoding.UTF8, "application/json");

                                //    return response;
                                //}
                            }
                            else
                            {
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Oops! From Pincode or To Pincode or both missing !!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                        }
                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Key not generated!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response;
                        }
                    }

                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! No Data Available in database!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }





                }
                catch (Exception ex)
                {


                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message.ToString()), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Not a valid invoice or user."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}