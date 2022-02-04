using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class NewYearSchemeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getNewYearScheme")]
        public HttpResponseMessage GetDetails(NewYearScheme ula)
        {

            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    string ordermsg = string.Empty;

                    List<SchemeForNewYear> alldcr = new List<SchemeForNewYear>();
                    List<SchemeData> Scheme = new List<SchemeData>();
                    List<DivisionWiseSale> Division = new List<DivisionWiseSale>();

                    var dr = g1.return_dt("newyearscheme '" + ula.CIN + "'");
                    var dr1 = g1.return_dr("newyearschememain '" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            Division.Add(new DivisionWiseSale
                            {
                                Division = Convert.ToString(dr.Rows[i]["divisionnm"]),
                                Sales = Convert.ToDecimal(dr.Rows[i]["Amount"]),
                                EarnedPoint = Convert.ToInt32(dr.Rows[i]["point"]),
                                OnePointPerSale = Convert.ToDecimal(dr.Rows[i]["pointonsale"]),
                            });
                        }

                        if (dr1.HasRows)
                        {
                            string baseurl = _goldMedia.MapPathToPublicUrl("");

                            while (dr1.Read())
                            {
                                Scheme.Add(new SchemeData
                                {
                                    division = Division,
                                   
                                    //SchemeName = Convert.ToString(dr.Rows[0]["SchemeName"]),
                                    TotalSale = Convert.ToDecimal(dr1["totalsale"]),
                                    TotalEarnedPoint = Convert.ToInt32(dr1["totalpoint"]),
                                    Currentslno = Convert.ToInt32(dr1["Currentslno"]),
                                    CurrentSlab = Convert.ToString(dr1["CurrentSlab"]),
                                    CurrentSlabPrice = Convert.ToString(dr1["CurrentSlabprice"]),
                                    CurrentSlabimg = string.IsNullOrEmpty(dr1["CurrentSlabimg"].ToString().Trim(',')) ? "" : (baseurl + "newyearbonanzaimg/" + dr1["CurrentSlabimg"].ToString().Trim(',')),
                                    CurrentSlabpoint = Convert.ToString(dr1["CurrentSlabpoint"]),
                                    NextSlab = Convert.ToString(dr1["nextSlab"]),
                                    NextSlabPrice = Convert.ToString(dr1["nextSlabprice"]),
                                    NextSlabimg = string.IsNullOrEmpty(dr1["nextSlabimg"].ToString().Trim(',')) ? "" : (baseurl + "newyearbonanzaimg/" + dr1["nextSlabimg"].ToString().Trim(',')),
                                    NextSlabpoint = Convert.ToString(dr1["nextSlabpoint"]),
                                    fileview = string.IsNullOrEmpty(dr1["newyearschme2022"].ToString().Trim(',')) ? "" : (baseurl + "newyearbonanzaimg/" + dr1["newyearschme2022"].ToString().Trim(',')),

                                });
                            }

                        }

                        g1.close_connection();
                        alldcr.Add(new SchemeForNewYear
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Scheme,
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
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}