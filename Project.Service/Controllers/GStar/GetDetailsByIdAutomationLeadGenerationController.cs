using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GetDetailsByIdAutomationLeadGenerationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetDetailsByIDAutomationLeadGeneration")]
        public HttpResponseMessage GetDetails(GetListofDetialsByIDAutomationLeadGeneration ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
              
                try
                {
                    string data1;

                    List<GetDetialsByIDAutomationLeadGenerationLists> alldcr = new List<GetDetialsByIDAutomationLeadGenerationLists>();
                    List<GetDetialsByIDAutomationLeadGenerationList> alldcr1 = new List<GetDetialsByIDAutomationLeadGenerationList>();
                    var dr = g1.return_dr("dbo.GetDetialsByIDAutomationLeadGeneration_GParivar_Gstar_API '" + ula.SlNo + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetDetialsByIDAutomationLeadGenerationList
                            {
                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                Reference_NO = Convert.ToString(dr["Reference_NO"].ToString()),
                                Cust_Name = Convert.ToString(dr["Cust_Name"].ToString()),
                                Cust_Mob_No = Convert.ToString(dr["Cust_Mob_No"].ToString()),
                                FullAddress = Convert.ToString(dr["FullAddress"].ToString()),
                                ItemNames = Convert.ToString(dr["ItemNames"].ToString()),
                                CategoryNames = Convert.ToString(dr["CategoryNames"].ToString()),
                                Remark = Convert.ToString(dr["Remark"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                Involve_Architech = Convert.ToString(dr["Involve_Architech"].ToString()),
                                Architech_No = Convert.ToString(dr["Architech_No"].ToString()),
                                Architech_Name = Convert.ToString(dr["Architech_Name"].ToString()),
                                Architech_CompanyName = Convert.ToString(dr["Architech_CompanyName"].ToString()),
                                RequestDate = Convert.ToString(dr["RequestDate"].ToString()),
                                Available_dt = Convert.ToString(dr["Available_dt"].ToString()),
                                Available_time = Convert.ToString(dr["Available_time"].ToString()),
                                QuotationNo = Convert.ToString(dr["QuotationNo"].ToString()),
                                SalesExName = Convert.ToString(dr["SalesExName"].ToString()),
                                EmpCode = Convert.ToString(dr["EmpCode"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                Involve_Builder = Convert.ToString(dr["isbuilderinvolved"].ToString()),
                                Builder_Name = Convert.ToString(dr["buildername"].ToString()),
                                Builder_No = Convert.ToString(dr["buildermobile"].ToString()),
                                Involve_Electric = Convert.ToString(dr["iselectricinvolved"].ToString()),
                                Electric_Name = Convert.ToString(dr["electricname"].ToString()),
                                Electric_No = Convert.ToString(dr["electricmobile"].ToString()),
                                Involve_Other = Convert.ToString(dr["isOthervolved"].ToString()),
                                Other_Name = Convert.ToString(dr["Othername"].ToString()),
                                Other_No = Convert.ToString(dr["Othermobile"].ToString()),
                                Lat = Convert.ToString(dr["Lat"].ToString()),
                                Long = Convert.ToString(dr["Long"].ToString()),
                                ImageName = Convert.ToString(dr["ImageName"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetDetialsByIDAutomationLeadGenerationLists
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