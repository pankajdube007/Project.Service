using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Project.Service.Models.GParivar;

namespace Project.Service.Controllers.GParivar
{
    public class GetPointSchemeHeadGParivarController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetPointSchemeHeadGParivardetails")]
        public HttpResponseMessage GetDetails(GetPointSchemeHeadGParivarList ula)
        {

            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    string ordermsg = string.Empty;

                    List<GetPointSchemeHeadGParivarLists> alldcr = new List<GetPointSchemeHeadGParivarLists>();
                    List<GetPointSchemeHeadGParivar> Scheme = new List<GetPointSchemeHeadGParivar>();
                    List<GetPointSchemeHeadGParivarListes> Details = new List<GetPointSchemeHeadGParivarListes>();

                    var dr = g1.return_dt("usp_pointschemeapppariwar '" + ula.CIN + "'," + ula.SchemeId + "");


                    if (dr.Rows.Count > 0)
                    {

                        var dr1 = g1.return_dr("usp_pointschemeapppariwardetails '" + ula.CIN + "'," + ula.SchemeId + "");
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            Details.Add(new GetPointSchemeHeadGParivarListes
                            {
                                DisplayName = Convert.ToString(dr.Rows[i]["displaynmwitharea"].ToString()),
                                BranchName = Convert.ToString(dr.Rows[i]["HomeBranch"].ToString()),
                                Division = Convert.ToString(dr.Rows[i]["Division"].ToString()),
                                Cin = Convert.ToString(dr.Rows[i]["cin"].ToString()),
                                Totalsale = Convert.ToString(dr.Rows[i]["TotalSale"].ToString()),
                                BonustotalSale = Convert.ToString(dr.Rows[i]["bonustotalSale"].ToString()),
                                Point = Convert.ToString(dr.Rows[i]["point"].ToString()),
                                BonusPoint = Convert.ToString(dr.Rows[i]["BonusPoint"].ToString()),
                                TotalPoint = Convert.ToString(dr.Rows[i]["TotalPoint"].ToString())
                            });
                        }

                        if (dr1.HasRows)
                        {

                            while (dr1.Read())
                            {
                                Scheme.Add(new GetPointSchemeHeadGParivar
                                {
                                    Details = Details,

                                    Gift = Convert.ToString(dr1["Gift"]).ToString(),
                                    NextGift = Convert.ToString(dr1["NextGift"]).ToString(),
                                    GiftImg = Convert.ToString(dr1["giftimg"]).ToString(),
                                    NextGiftImg = Convert.ToString(dr1["nextgiftimg"]).ToString(),
                                    PdfLink = Convert.ToString(dr1["pdflink"]).ToString(),
                                    FinalPoint = Convert.ToString(dr1["finalpoint"]).ToString(),
                                    GiftPoint = Convert.ToString(dr1["GiftPoint"]).ToString(),
                                    NextGiftPoint = Convert.ToString(dr1["NextGiftPoint"]).ToString(),

                                });
                            }
                        }

                        g1.close_connection();
                        alldcr.Add(new GetPointSchemeHeadGParivarLists
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