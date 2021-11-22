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
    public class salereturnrequestselectexecwiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getsalereturnrequestselectexecwise")]
        public HttpResponseMessage GetDetails(Listsalereturnrequestselectexecwise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<salereturnrequestselectexecwises> alldcr = new List<salereturnrequestselectexecwises>();
                    List<salereturnrequestselectexecwise> alldcr1 = new List<salereturnrequestselectexecwise>();

                    var dr = g1.return_dr("salereturnrequestselectexecwise " + ula.ExId + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new salereturnrequestselectexecwise
                            {
                                Slno = Convert.ToInt32(dr["Slno"].ToString()),
                                requestno = Convert.ToString(dr["requestno"].ToString()),
                                qty = Convert.ToInt32(dr["qty"].ToString()),

                                divid = Convert.ToInt32(dr["divid"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                requestdt = Convert.ToString(dr["requestdt"].ToString()),

                                levelid = Convert.ToInt32(dr["levelid"].ToString()),
                                partyname = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                rtype = Convert.ToString(dr["rtype"].ToString()),

                                qtytype = Convert.ToString(dr["qtytype"].ToString()),
                                level = Convert.ToString(dr["level"].ToString()),

                                pickupdate = Convert.ToString(dr["pickupdt"].ToString()),
                                pickuptime = Convert.ToString(dr["pickuptime"].ToString()),

                                amount = Convert.ToInt32(dr["amount"].ToString()),
                                finalize= Convert.ToString(dr["finalize"].ToString()),
                                finalizedate = Convert.ToString(dr["finalitmedtt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new salereturnrequestselectexecwises
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