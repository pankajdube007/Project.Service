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
    public class CatListDivWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCategoryDivisionWise")]
        public HttpResponseMessage GetDetails(CategoryDivWie ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
          
            if (ula.ExId !=0)
            {
                try
                {
                    string data1;

                    List<CategoryDivWieLists> alldcr = new List<CategoryDivWieLists>();
                    List<CategoryDivWieList> alldcr1 = new List<CategoryDivWieList>();
                    var dr = g1.return_dr("AppCatList '" + ula.Div + "'");

                    if (dr.HasRows)
                    {
                        
                        while (dr.Read())
                        {
                            alldcr1.Add(new CategoryDivWieList
                            {
                                CatID = Convert.ToInt32(dr["SlNo"]),
                                Cat = dr["categorynm"].ToString(),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CategoryDivWieLists
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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