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

namespace Project.Service.Controllers
{
    public class ProblemListForItemController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetProblemListForItem")]
        public HttpResponseMessage GetProblemDetails(ProblemListForItem ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();


            try
            {
                string data1;

                List<GetProblemList> alldcr = new List<GetProblemList>();
                List<ProblemList> alldcr1 = new List<ProblemList>();

                var dr = g2.return_dr("ItemProbList '" + ula.itemid + "'");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new ProblemList
                        {

                            slno = Convert.ToInt32(dr["slno"].ToString()),
                            ItemProblem = Convert.ToString(dr["ItemProblem"].ToString()),

                        });
                    }
                    g2.close_connection();

                    alldcr.Add(new GetProblemList
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
                    g2.close_connection();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
    }
}