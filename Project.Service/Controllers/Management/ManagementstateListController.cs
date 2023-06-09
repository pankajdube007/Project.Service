using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Service.Models.Management;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Project.Service.Controllers.Management
{
    public class ManagementstateListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetManagementstateList")]
        public HttpResponseMessage GetDetails(ManagementstateList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ManagementstateLists> alldcr = new List<ManagementstateLists>();
                    List<dataManagementstateList> alldcr1 = new List<dataManagementstateList>();

                    var dr = g1.return_dr("usp_GetDhanbarseStateList '" + ula.Category +  "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new dataManagementstateList
                            {
                                Slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                Statename = Convert.ToString(dr["statenm"].ToString())
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementstateLists
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