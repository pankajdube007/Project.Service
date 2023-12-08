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
    public class GetNewYearDamakaOfferController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetNewYearDamakaOffer")]
        public HttpResponseMessage GetDetails(GetNewYearDamakaOfferList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetNewYearDamakaOffer> alldcr = new List<GetNewYearDamakaOffer>();
                    List<GetNewYearDamakaOffers> alldcr1 = new List<GetNewYearDamakaOffers>();
                    var dr = g1.return_dr($"dbo.usp_PartywiseitemWisepoinapp  '{ula.CIN}' ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetNewYearDamakaOffers
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
                        alldcr.Add(new GetNewYearDamakaOffer
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