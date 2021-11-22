using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace Project.Service.Controllers
{
    public class EInvoiceGenerateAdaequareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EInvoiceBillGenerateAda")]
        public HttpResponseMessage GetDetails(ListsofEInvoiceGenerate ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            var key = string.Empty;
            var data = string.Empty;
            //  decimal distance = 0;
            if (ula.userid != 0 && ula.slno != 0)
            {
                try
                {
                    List<EInvoiceGenerateS> head = new List<EInvoiceGenerateS>();
                    List<EInvoiceGenerateItemList> child = new List<EInvoiceGenerateItemList>();
                    List<EInvoice> ewaybill = new List<EInvoice>();
                    List<EInvoices> result = new List<EInvoices>();
                    // int active = 0;
                    key = cm.GetEInvoiceTokannoAdaequare();


                    if (key != "")
                    {
                        var dr3 = g2.return_dt("EInvoiceDatabySlno " + ula.slno + "," + ula.type);


                        if (dr3.Rows.Count > 0)
                        {
                            var errortxt = einvoicevalidation(dr3);

                            if (errortxt == "")
                            {



                                var dr4 = g2.return_dt("EInvoiceDataChildbySlno " + ula.slno + "," + ula.type);
                                for (int i = 0; i < dr4.Rows.Count; i++)

                                {
                                    List<AttribDtl> abt = new List<AttribDtl>();

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
                                        Discount = Convert.ToDecimal(dr4.Rows[i]["Discount"]),
                                        PreTaxVal = Convert.ToDecimal(dr4.Rows[i]["TotAmt"]) - Convert.ToInt32(dr4.Rows[i]["Discount"]),
                                        //   PreTaxVal = 1,
                                        AssAmt = Convert.ToDecimal(dr4.Rows[i]["PreTaxVal"]),
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




                                var data1 = new EInvoiceGenerateS
                                {

                                    // for Production //
                                    Version = "1.03",


                                    TranDtls = new TranDtls
                                    {
                                        TaxSch = "GST",
                                        SupTyp = dr3.Rows[0]["SupTyp"].ToString().Trim(),
                                        RegRev = "N",
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

                                        //   Gstin = "02AMBPG7773M002",


                                        Gstin = dr3.Rows[0]["SellerDtlsGstin"].ToString().Trim(),
                                        LglNm = dr3.Rows[0]["SellerDtlsLglNm"].ToString(),
                                        TrdNm = dr3.Rows[0]["SellerDtlsTrdNm"].ToString(),
                                        Addr1 = dr3.Rows[0]["SellerDtlsAddr1"].ToString(),
                                        Addr2 = string.IsNullOrEmpty(dr3.Rows[0]["SellerDtlsAddr2"].ToString().Trim()) ? "Not Applicable" : dr3.Rows[0]["SellerDtlsAddr2"].ToString(),
                                        Loc = dr3.Rows[0]["SellerDtlsLoc"].ToString(),
                                        Pin = Convert.ToInt32(dr3.Rows[0]["SellerDtlsPin"].ToString()),
                                        Stcd = dr3.Rows[0]["SellerDtlsStcd"].ToString(),
                                        Ph = dr3.Rows[0]["SellerDtlsPh"].ToString(),
                                        // Ph = "9518957760",
                                        Em = dr3.Rows[0]["SellerDtlsEm"].ToString()
                                    },

                                    BuyerDtls = new BuyerDtls
                                    {
                                        Gstin = dr3.Rows[0]["BuyerDtlsGstin"].ToString().Trim(),
                                        LglNm = dr3.Rows[0]["BuyerDtlsLglNm"].ToString(),
                                        TrdNm = dr3.Rows[0]["BuyerDtlsTrdNm"].ToString(),
                                        Pos = dr3.Rows[0]["BuyerDtlsStcd"].ToString(),
                                        Addr1 = dr3.Rows[0]["BuyerDtlsAddr1"].ToString(),
                                        Addr2 = string.IsNullOrEmpty(dr3.Rows[0]["BuyerDtlsAddr2"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["BuyerDtlsAddr2"].ToString(),
                                        Loc = dr3.Rows[0]["BuyerDtlsLoc"].ToString(),
                                        Pin = Convert.ToInt32(dr3.Rows[0]["BuyerDtlsPin"].ToString()),
                                        Stcd = dr3.Rows[0]["BuyerDtlsStcd"].ToString(),
                                        Ph = dr3.Rows[0]["BuyerDtlsPh"].ToString(),
                                        // Ph = "9518957760",
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
                                        Gstin = dr3.Rows[0]["ShipDtlsGstin"].ToString().Trim(),
                                        LglNm = dr3.Rows[0]["ShipDtlsLglNm"].ToString(),
                                        TrdNm = dr3.Rows[0]["ShipDtlsTrdNm"].ToString(),
                                        Addr1 = Convert.ToString(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsAddr1"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["ShipDtlsAddr1"].ToString()),
                                        Addr2 = Convert.ToString(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsAddr2"].ToString().Trim()) ? "--Not Applicable--" : dr3.Rows[0]["ShipDtlsAddr2"].ToString()),
                                        Loc = dr3.Rows[0]["ShipDtlsLoc"].ToString(),
                                        Pin = Convert.ToInt32(string.IsNullOrEmpty(dr3.Rows[0]["ShipDtlsPin"].ToString()) ? "0000" : dr3.Rows[0]["ShipDtlsPin"].ToString()),
                                        Stcd = dr3.Rows[0]["ShipDtlsStcd"].ToString()
                                    },


                                    ItemList = child,


                                    // for production

                                    ValDtls = new ValDtls
                                    {
                                        AssVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsAssVal"]),
                                        CgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsCgstVal"]),
                                        SgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsSgstVal"]),
                                        IgstVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsIgstVal"]),
                                        CesVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsCesVal"]),
                                        StCesVal = Convert.ToDecimal(dr3.Rows[0]["ValDtlsStCesVal"]),
                                        Discount = Convert.ToInt32(dr3.Rows[0]["ValDtlsDiscount"]),
                                       // Discount = 0,
                                        OthChrg = Convert.ToInt32(dr3.Rows[0]["ValDtlsOthChrg"]),
                                        RndOffAmt = Convert.ToDecimal(dr3.Rows[0]["ValDtlsRndOffAmt"]),
                                        //  TotInvVal = Convert.ToInt32(dr3.Rows[0]["ValDtlsTotInvVal"])+ Convert.ToInt32(dr3.Rows[0]["ValDtlsDiscount"]),
                                        TotInvVal = Convert.ToInt32(dr3.Rows[0]["ValDtlsTotInvVal"]),
                                        TotInvValFc = Convert.ToDecimal(dr3.Rows[0]["ValDtlsTotInvValFc"])

                                    },


                                    PayDtls = new PayDtls(),
                                    RefDtls = null,
                                    AddlDocDtls = null,
                                    ExpDtls = new ExpDtls(),

                                    EwbDtls = new EwbDtls()


                                };


                                var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno + "," + ula.type);
                                if (dr5.Rows.Count > 0)
                                {

                                    string requtid = string.Empty;
                                    requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                                    var baseurl = ConfigurationManager.AppSettings["Gold.Eway.EAPI"] + "invoice";
                                    System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                                    // for production //
                                    request1.Method = "POST";
                                    request1.ContentType = "application/json";
                                    request1.Headers.Add("user_name", dr5.Rows[0]["userid"].ToString());
                                    request1.Headers.Add("password", dr5.Rows[0]["password"].ToString());
                                    request1.Headers.Add("gstin", dr5.Rows[0]["GSTNo"].ToString());
                                    request1.Headers.Add("requestid", requtid);
                                    request1.Headers.Add("Authorization", "Bearer " + key);

                                    //for testing //

                                    //request1.Method = "POST";
                                    //    request1.ContentType = "application/json";
                                    //    request1.Headers.Add("user_name", "adqgsphpusr1");
                                    //    request1.Headers.Add("password", "Gsp@1234");
                                    //    request1.Headers.Add("gstin", "02AMBPG7773M002");
                                    //    request1.Headers.Add("requestid", requtid);
                                    //    request1.Headers.Add("Authorization", "Bearer " + key);

                                    //Place the serialized content of the object to be posted into the request stream
                                    using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                    {
                                        var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data1);
                                        streamWriter.Write(json);
                                        streamWriter.Flush();
                                        streamWriter.Close();
                                        var dr99 = g2.return_dr("InsertEinvoiceInput " + ula.slno + ",'" + json.ToString() + "'");
                                    }

                                   

                                   System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();




                                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                                    {

                                        dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());

                                        if (_output.success = true && _output.message == "IRN generated successfully")
                                        {


                                            var dr6 = g2.return_dr("EwayInvoiceupdatebyAPI " + ula.slno + "," + ula.type + ",'"

                                 + _output.result.AckNo.ToString() + "','"
                            + _output.result.AckDt.ToString() + "','"
                            + _output.result.Irn.ToString() + "','"
                            + _output.result.SignedInvoice.ToString() + "','"
                            + _output.result.SignedQRCode.ToString() + "','"
                            + _output.result.Status.ToString()
                                + "'," + ula.userid

                                );
                                            if (dr6.HasRows)
                                            {
                                                ewaybill.Add(new EInvoice
                                                {
                                                    ewaybillno = _output.result.Irn.ToString(),
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
                                                response9.Content = new StringContent(cm.StatusTime(false, "Oops! EInvoice not updated in database!!!!!!!!"), Encoding.UTF8, "application/json");

                                                return response9;

                                            }
                                        }
                                        else
                                        {
                                            if (_output.message.ToString() == "2150 : Duplicate IRN" || _output.message.ToString() == "2150 : Duplicate IRN200")
                                            {
                                                var dr55 = g2.return_dt("EwayBillGenerateHeader " + ula.slno + "," + ula.type);
                                                if (dr55.Rows.Count > 0)
                                                {
                                                    string requtid1 = string.Empty;
                                                    requtid1 = dr55.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                                                    var baseurldupicate = ConfigurationManager.AppSettings["Gold.Eway.EAPI"] + "invoice/irn?irn="+ _output.result[0].Desc.Irn.ToString();
                                                    System.Net.HttpWebRequest request2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurldupicate);

                                                    // for production //
                                                    request2.Method = "GET";
                                                    request2.ContentType = "application/json";
                                                    request2.Headers.Add("user_name", dr55.Rows[0]["userid"].ToString());
                                                    request2.Headers.Add("password", dr55.Rows[0]["password"].ToString());
                                                    request2.Headers.Add("gstin", dr55.Rows[0]["GSTNo"].ToString());
                                                    request2.Headers.Add("requestid", requtid1);
                                                    request2.Headers.Add("Authorization", "Bearer " + key);

                                                    System.Net.HttpWebResponse response2 = (System.Net.HttpWebResponse)request2.GetResponse();
                                                    if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                                                    {
                                                        dynamic _output1 = JsonConvert.DeserializeObject((new StreamReader(response2.GetResponseStream())).ReadToEnd());
                                                        if (_output1.success = true && _output1.message == "E-Invoice fetched successfully")
                                                        {


                                                            var dr6 = g2.return_dr("EwayInvoiceupdatebyAPI " + ula.slno + "," + ula.type + ",'"

                                                 + _output1.result.AckNo.ToString() + "','"
                                            + _output1.result.AckDt.ToString() + "','"
                                            + _output1.result.Irn.ToString() + "','"
                                            + _output1.result.SignedInvoice.ToString() + "','"
                                            + _output1.result.SignedQRCode.ToString() + "','"
                                            + _output1.result.Status.ToString()
                                                + "'," + ula.userid

                                                );
                                                            if (dr6.HasRows)
                                                            {
                                                                ewaybill.Add(new EInvoice
                                                                {
                                                                    ewaybillno = _output1.result.Irn.ToString(),
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
                                                                HttpResponseMessage response99 = request.CreateResponse(HttpStatusCode.OK);
                                                                response99.Content = new StringContent(cm.StatusTime(false, "Oops! EInvoice not updated in database!!!!!!!!"), Encoding.UTF8, "application/json");

                                                                return response99;

                                                            }
                                                        }
                                                        else
                                                        {
                                                            HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                            response9.Content = new StringContent(cm.StatusTime(false, _output.message.ToString()+"200"), Encoding.UTF8, "application/json");

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

                                            }
                                            else
                                            {
                                                HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                response9.Content = new StringContent(cm.StatusTime(false, _output.message.ToString()+"100"), Encoding.UTF8, "application/json");

                                                return response9;
                                            }


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
                            }
                            else
                            {
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Oops!!" + errortxt), Encoding.UTF8, "application/json");

                                return response;
                            }

                        }

                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Same invoice no is deleted once, use different invoice or update invoice no!!!!!!!!"), Encoding.UTF8, "application/json");

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
                catch (Exception ex)
                {

                    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                    {
                        var dr5 = g2.return_dt("EwayBillKeyInactive");

                    }

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


        public string einvoicevalidation(DataTable dt)
        {
            string outstring = string.Empty;



            //Seller Details
            if (string.IsNullOrEmpty(dt.Rows[0]["SellerDtlsPh"].ToString()) || dt.Rows[0]["SellerDtlsPh"].ToString().Length < 7 || dt.Rows[0]["SellerDtlsPh"].ToString().Length > 12)
            {
                outstring = outstring = "Seller Phone No(" + dt.Rows[0]["SellerDtlsPh"].ToString() + ") missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SellerDtlsEm"].ToString()) || dt.Rows[0]["SellerDtlsEm"].ToString().Length < 4)
            {
                outstring = "Seller Email Id Missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SellerDtlsAddr1"].ToString()) || dt.Rows[0]["SellerDtlsAddr1"].ToString().Length < 4)
            {
                outstring = "Seller Address Missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SellerDtlsPin"].ToString()) || dt.Rows[0]["SellerDtlsPin"].ToString().Length < 4)
            {
                outstring = "Seller Pincode Missing or Invalid!!!";
            }




            //Buyer Details

            else if (string.IsNullOrEmpty(dt.Rows[0]["BuyerDtlsPh"].ToString()) || dt.Rows[0]["BuyerDtlsPh"].ToString().Length < 7 || dt.Rows[0]["BuyerDtlsPh"].ToString().Length > 12)
            {
                outstring = "Buyer Phone No(" + dt.Rows[0]["BuyerDtlsPh"].ToString() + ") missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["BuyerDtlsEm"].ToString()) || dt.Rows[0]["BuyerDtlsEm"].ToString().Length < 4)
            {
                outstring = "Buyer Email Id Missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["BuyerDtlsAddr1"].ToString()) || dt.Rows[0]["BuyerDtlsAddr1"].ToString().Length < 4)
            {
                outstring = "Buyer Address Missing or Invalid!!!";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["BuyerDtlsPin"].ToString()) || dt.Rows[0]["BuyerDtlsPin"].ToString().Length < 4)
            {
                outstring = "Buyer Pincode Missing or Invalid!!!";
            }


            return outstring;
        }
    }
}