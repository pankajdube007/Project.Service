using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Management;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class MaterialInspectionHistoryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getMaterialInspectionHistory")]
        public HttpResponseMessage GetDetails(MaterialInspectionHistory ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<MaterialInspectionHistorye> alldcr = new List<MaterialInspectionHistorye>();
                    List<MaterialInspectionHistoryes> alldcr1 = new List<MaterialInspectionHistoryes>();

                    var dr = g1.return_dr("MaterialInspectionHistory '"+ ula.Vendorid + "','" + ula.Category + "'");
                    

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new MaterialInspectionHistoryes
                            {

                                Slno = Convert.ToString(dr["Slno"].ToString()),
                                ReferenceNo = Convert.ToString(dr["ReferenceNo"].ToString()),
                                Status = Convert.ToString(dr["status"].ToString()),
                                Date = Convert.ToString(dr["date"].ToString()),
                                Remark = Convert.ToString(dr["remark"].ToString()),
                                Cnt = Convert.ToString(dr["cnt"].ToString()),
                                

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new MaterialInspectionHistorye
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