using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GetPointSchemeGStarController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetPointSchemeStar")]
        public HttpResponseMessage GetDetails(GetPointSchemeGStarList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetPointSchemeGStar> alldcr = new List<GetPointSchemeGStar>();
                    List<GetPointSchemeGStars> alldcr1 = new List<GetPointSchemeGStars>();
                    var dr = g1.return_dr($"dbo.usp_pointschemeappstar {ula.ExId} , {ula.SchemeId}");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetPointSchemeGStars
                            {
                                DisplayName = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                SchemeId = Convert.ToString(dr["schemeid"].ToString()),
                                SchemeName = Convert.ToString(dr["SchemeName"].ToString()),
                                BranchName = Convert.ToString(dr["HomeBranch"].ToString()),
                                Cin = Convert.ToString(dr["cin"].ToString()),
                                TotalSale = Convert.ToString(dr["TotalSale"].ToString()),
                                BonustotalSale = Convert.ToString(dr["bonustotalSale"].ToString()),
                                Point = Convert.ToString(dr["point"].ToString()),
                                BonusPoint = Convert.ToString(dr["BonusPoint"].ToString()),
                                TotalPoint = Convert.ToString(dr["TotalPoint"].ToString()),
                                Gift = Convert.ToString(dr["Gift"].ToString()),
                                NextGift = Convert.ToString(dr["NextGift"].ToString()),
                                GiftImage = Convert.ToString(dr["giftimg"].ToString()),
                                NextGiftImage = Convert.ToString(dr["nextgiftimg"].ToString()),
                                PdfLink = Convert.ToString(dr["pdflink"].ToString())
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetPointSchemeGStar
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
    }
}