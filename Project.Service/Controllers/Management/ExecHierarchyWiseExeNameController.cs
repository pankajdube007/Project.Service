using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.Management;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class ExecHierarchyWiseExeNameController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ExecWiseExecName")]
        public HttpResponseMessage GetDetails(ExecHierarchyWiseExeName ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ExecHierarchyWiseExeNameLists> alldcr = new List<ExecHierarchyWiseExeNameLists>();
                    List<ExecHierarchyWiseExeNameList> alldcr1 = new List<ExecHierarchyWiseExeNameList>();

                    var dr = g1.return_dr($"exechyexecname {ula.ExecId} , '{ula.Category}'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecHierarchyWiseExeNameList
                            {
                                ExecId = Convert.ToString(dr["execid"].ToString()),
                                Name = Convert.ToString(dr["name"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecHierarchyWiseExeNameLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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