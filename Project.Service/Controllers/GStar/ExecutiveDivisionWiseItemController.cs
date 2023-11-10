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
    public class ExecutiveDivisionWiseItemController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ExecutiveDivisionWiseItem")]
        public HttpResponseMessage GetDetails(ExecutiveDivisionWiseItem ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecutiveDivisionWiseItems> alldcr = new List<ExecutiveDivisionWiseItems>();
                    List<GetExecutiveDivisionWiseItems> alldcr1 = new List<GetExecutiveDivisionWiseItems>();
                    var dr = g1.return_dr($"dbo.ExecDivisionWiseItem  {ula.ExId} , '{ula.Search}'");
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new GetExecutiveDivisionWiseItems
                            {
                                SLno = Convert.ToString(dr["slno"].ToString()),
                                Searchitem = Convert.ToString(dr["ProductCode"].ToString()),
                                Divisionid = Convert.ToString(dr["divisionid"].ToString()),
                                DivisionName = Convert.ToString(dr["divisionnm"].ToString()),
                                CategoryId = Convert.ToString(dr["categoryid"].ToString()),
                                CategoryName = Convert.ToString(dr["categorynm"].ToString()),
                                ItemCode = Convert.ToString(dr["ProductCode1"].ToString()),
                                ItemDescription = Convert.ToString(dr["itemnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecutiveDivisionWiseItems
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available !!!"), Encoding.UTF8, "application/json");

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