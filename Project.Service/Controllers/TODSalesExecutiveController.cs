using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static Project.Service.Models.TODSalesExecutive;

namespace Project.Service.Controllers
{
    public class TODSalesExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTODSalesExecutive")]
        public HttpResponseMessage GetDetails(InputRequest inputRequest)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (inputRequest.ExId != 0)
            {
                try
                {
                    string data1;

                    List<OutputResponse> alldcr = new List<OutputResponse>();
                    List<TODSalesExecutives> alldcr1 = new List<TODSalesExecutives>();

                    var dr = g1.return_dr("spgetTodExecutive " +inputRequest.ExId+","+inputRequest.groupId+",'"+Convert.ToBoolean(inputRequest.isTODAccepted)+"','"+inputRequest.CIN+"'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TODSalesExecutives
                            {
                                dealernm = Convert.ToString(dr["dealernm"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                qtysale = Convert.ToString(dr["qtysale"].ToString()),
                                qtytarget = Convert.ToString(dr["quatertod"].ToString()),
                                qtyshortamt = Convert.ToString(dr["qtyshortamt"].ToString()),
                                curmnthtarget = Convert.ToString(dr["monthtod"].ToString()),
                                curmnthsale = Convert.ToString(dr["curmnthsale"].ToString()),
                                curmnthshortamt = Convert.ToString(dr["curmnthshortamt"].ToString()),
                                isaccepted = Convert.ToInt32(dr["isaccepted"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OutputResponse
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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