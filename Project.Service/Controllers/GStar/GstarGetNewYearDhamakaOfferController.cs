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
    public class GstarGetNewYearDhamakaOfferController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GstarGetNewYearDhamakaOffer")]
        public HttpResponseMessage GetDetails(GstarGetNewYearDhamakaOfferList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GstarGetNewYearDhamakaOffer> alldcr = new List<GstarGetNewYearDhamakaOffer>();
                    List<GstarGetNewYearDhamakaOffers> alldcr1 = new List<GstarGetNewYearDhamakaOffers>();
                    var dr = g1.return_dr($"dbo.usp_PartywiseitemWisepoinappstar {ula.ExId}");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GstarGetNewYearDhamakaOffers
                            {
                                CIN = Convert.ToString(dr["cin"].ToString()),
                                Name = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                DivisionName = Convert.ToString(dr["Division"].ToString()),
                                Sale = Convert.ToString(dr["salequantity"].ToString()),
                                Point = Convert.ToString(dr["SaleQuantityPoint"].ToString()),
                                TotalPoint = Convert.ToString(dr["totalpoint"].ToString()),
                                Bonus = Convert.ToString(dr["bouns"].ToString()),
                                Reward = Convert.ToString(dr["reward"].ToString()),
                                NextReward = Convert.ToString(dr["Nextreward"].ToString()),
                                Rewarding = Convert.ToString(dr["Rewarding"].ToString()),
                                NextRewardImage = Convert.ToString(dr["NextRewardImage"].ToString()),
                                PdfLink = Convert.ToString(dr["pdflink"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GstarGetNewYearDhamakaOffer
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