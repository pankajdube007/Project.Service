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
    public class GetHelpDeskConfigurationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorHelpDeskConfiguration")]
        public HttpResponseMessage GetDetails(ListofHelpDeskConfiguration ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetHelpDeskConfigurationDetails> alldcr = new List<GetHelpDeskConfigurationDetails>();
                    List<GetHelpDeskConfigurationDetail> alldcr1 = new List<GetHelpDeskConfigurationDetail>();
                    var dr = g1.return_dr("dbo.spVendProcHelpDeskConfigurationSelectapp '" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetHelpDeskConfigurationDetail
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                WorkDesc = Convert.ToString(dr["WorkDesc"].ToString()),
                                EmpId = Convert.ToString(dr["EmpId"].ToString()),
                                Status = Convert.ToString(dr["Status"].ToString()),
                                createdBy = Convert.ToString(dr["createuid"].ToString()),
                                createdDate = Convert.ToString(dr["createdt"].ToString()),
                                modifyBy = Convert.ToString(dr["lmodifyuid"].ToString()),
                                modifyDate = Convert.ToString(dr["lmodifydt"].ToString()),
                                DeletedBy = Convert.ToString(dr["deluid"].ToString()),
                                DeletedDate = Convert.ToString(dr["deldt"].ToString()),
                                logno = Convert.ToString(dr["logno"].ToString()),
                                application = Convert.ToString(dr["application"].ToString()),
                                ContactPerson = Convert.ToString(dr["ContactPerson"].ToString()),
                                contactno = Convert.ToString(dr["contactno"].ToString()),
                                contactnocur = Convert.ToString(dr["contactnocur"].ToString()),
                                email = Convert.ToString(dr["email"].ToString()),
                                Department = Convert.ToString(dr["Department"].ToString()),
                                

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetHelpDeskConfigurationDetails
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