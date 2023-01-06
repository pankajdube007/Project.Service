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
    public class MenInBlueHeadController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getmeninblueschememain")]
        public HttpResponseMessage GetAllUserdetails(ListMenInBlueHead ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<MenInBlueHeads> alldcr = new List<MenInBlueHeads>();
                    List<MenInBlueHead> alldcr1 = new List<MenInBlueHead>();
                    var dr = g1.return_dr("meninblueschememain '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                                

                            alldcr1.Add(new MenInBlueHead
                            {
                                Name = Convert.ToString(dr["displaynm"].ToString()),
                                Branch = Convert.ToString(dr["HomeBranch"].ToString()),
                                TotalPoint = Convert.ToString(dr["points"].ToString()),
                                AusPoint = Convert.ToString(dr["auspoint"].ToString()),
                                PendingPoints = Convert.ToString(dr["balpoint"].ToString()),
                                address = Convert.ToString(dr["address"].ToString()),
                                isselection = Convert.ToBoolean(dr["isselection"].ToString()),
                                isEditable = Convert.ToBoolean(dr["isEditable"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new MenInBlueHeads
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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