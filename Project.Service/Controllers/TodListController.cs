using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TodListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTODList")]
        public HttpResponseMessage GetDetails(ListTodList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<TodLists> alldcr = new List<TodLists>();
                    List<TodList> alldcr1 = new List<TodList>();

                    var dr = g1.return_dr("todagreementselectApp '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            alldcr1.Add(new TodList
                            {
                                GroupName = Convert.ToString(dr["GroupName"].ToString()),
                                GroupId = Convert.ToString(dr["GroupId"].ToString()),
                                YearlyTargetAmt = Convert.ToString(dr["YearlyTargetAmt"].ToString()),
                                YearlySalesAmt = Convert.ToString(dr["yearlySaleAmt"].ToString()),

                                q1amt = Convert.ToString(dr["q1amt"].ToString()),
                                q2amt = Convert.ToString(dr["q2amt"].ToString()),
                                q3amt = Convert.ToString(dr["q3amt"].ToString()),
                                q4amt = Convert.ToString(dr["q4amt"].ToString()),
                                apramt = Convert.ToString(dr["apramt"].ToString()),
                                mayamt = Convert.ToString(dr["mayamt"]),
                                junamt = Convert.ToString(dr["junamt"]),
                                julamt = Convert.ToString(dr["julamt"]),
                                augamt = Convert.ToString(dr["augamt"]),
                                sepamt = Convert.ToString(dr["sepamt"]),
                                octamt = Convert.ToString(dr["octamt"]),
                                novamt = Convert.ToString(dr["novamt"]),
                                decamt = Convert.ToString(dr["decamt"]),
                                janamt = Convert.ToString(dr["janamt"]),
                                febamt = Convert.ToString(dr["febamt"]),
                                maramt = Convert.ToString(dr["maramt"]),

                                q1amts = Convert.ToString(dr["q1sale"].ToString()),
                                q2amts = Convert.ToString(dr["q2sale"].ToString()),
                                q3amts = Convert.ToString(dr["q3sale"].ToString()),
                                q4amts = Convert.ToString(dr["q4sale"].ToString()),
                                apramts = Convert.ToString(dr["aprsale"].ToString()),
                                mayamts = Convert.ToString(dr["maysale"]),
                                junamts = Convert.ToString(dr["junsale"]),
                                julamts = Convert.ToString(dr["julsale"]),
                                augamts = Convert.ToString(dr["augsale"]),
                                sepamts = Convert.ToString(dr["sepsale"]),
                                octamts = Convert.ToString(dr["octsale"]),
                                novamts = Convert.ToString(dr["novsale"]),
                                decamts = Convert.ToString(dr["decsale"]),
                                janamts = Convert.ToString(dr["jansale"]),
                                febamts = Convert.ToString(dr["febsale"]),
                                maramts = Convert.ToString(dr["marsale"]),



                                YearlytradeSale=Convert.ToString(dr["yearlyTradeSale"]),
                                q1tradesale =Convert.ToString(dr["q1tradesale"]),
                                q2tradesale =Convert.ToString(dr["q2tradesale"]),
                                q3tradesale =Convert.ToString(dr["q3tradesale"]),
                                q4tradesale =Convert.ToString(dr["q4tradesale"]),
                                aprtradesale=Convert.ToString(dr["aprtradesale"]),
                                maytradesale=Convert.ToString(dr["maytradesale"]),
                                juntradesale=Convert.ToString(dr["juntradesale"]),
                                jultradesale=Convert.ToString(dr["jultradesale"]),
                                augtradesale=Convert.ToString(dr["augtradesale"]),
                                septradesale=Convert.ToString(dr["septradesale"]),
                                octtradesale=Convert.ToString(dr["octtradesale"]),
                                novtradesale=Convert.ToString(dr["novtradesale"]),
                                dectradesale=Convert.ToString(dr["dectradesale"]),
                                jantradesale=Convert.ToString(dr["jantradesale"]),
                                febtradesale=Convert.ToString(dr["febtradesale"]),
                                martradesale=Convert.ToString(dr["martradesale"]),

                                YearlyprojectSale = Convert.ToString(dr["yearlyProjectSale"]),
                                q1projectsale = Convert.ToString(dr["q1projectsale"]),
                                q2projectsale = Convert.ToString(dr["q2projectsale"]),
                                q3projectsale = Convert.ToString(dr["q3projectsale"]),
                                q4projectsale = Convert.ToString(dr["q4projectsale"]),
                                aprprojectsale = Convert.ToString(dr["aprprojectsale"]),
                                mayprojectsale = Convert.ToString(dr["mayprojectsale"]),
                                junprojectsale = Convert.ToString(dr["junprojectsale"]),
                                julprojectsale = Convert.ToString(dr["julprojectsale"]),
                                augprojectsale = Convert.ToString(dr["augprojectsale"]),
                                sepprojectsale = Convert.ToString(dr["sepprojectsale"]),
                                octprojectsale = Convert.ToString(dr["octprojectsale"]),
                                novprojectsale = Convert.ToString(dr["novprojectsale"]),
                                decprojectsale = Convert.ToString(dr["decprojectsale"]),
                                janprojectsale = Convert.ToString(dr["janprojectsale"]),
                                febprojectsale = Convert.ToString(dr["febprojectsale"]),
                                marprojectsale = Convert.ToString(dr["marprojectsale"]),


                                YearlyEarnedAmt=Convert.ToString(dr["yearlytodAmt"]),
                                q1earnedamt =Convert.ToString(dr["q1todamt"]),
                                q2earnedamt =Convert.ToString(dr["q2todamt"]),
                                q3earnedamt =Convert.ToString(dr["q3todamt"]),
                                q4earnedamt =Convert.ToString(dr["q4todamt"]),
                                aprearnedamt=Convert.ToString(dr["aprtodamt"]),
                                mayearnedamt=Convert.ToString(dr["maytodamt"]),
                                junearnedamt=Convert.ToString(dr["juntodamt"]),
                                julearnedamt=Convert.ToString(dr["jultodamt"]),
                                augearnedamt=Convert.ToString(dr["augtodamt"]),
                                sepearnedamt=Convert.ToString(dr["septodamt"]),
                                octearnedamt=Convert.ToString(dr["octtodamt"]),
                                novearnedamt=Convert.ToString(dr["novtodamt"]),
                                decearnedamt=Convert.ToString(dr["dectodamt"]),
                                janearnedamt=Convert.ToString(dr["jantodamt"]),
                                febearnedamt=Convert.ToString(dr["febtodamt"]),
                                marearnedamt=Convert.ToString(dr["martodamt"]),



                                isAccept = Convert.ToBoolean(dr["isAccept"])

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data"), Encoding.UTF8, "application/json");

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
