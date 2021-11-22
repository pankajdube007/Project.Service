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
    public class LeadSyncsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/LeadSync")]
        public HttpResponseMessage LeadSync(LeadSyncAction lsa)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(lsa.uniquekey))
            {
                if (lsa.flag == 1)
                {
                    try
                    {
                        string data1;
                        List<DcrDetailsByUser> alldcr = new List<DcrDetailsByUser>();
                        List<DcrDetailsByUsers> alldcr1 = new List<DcrDetailsByUsers>();
                        var dr = g1.return_dr("dcrdetailsbyuserid1 " + lsa.userid + string.Empty);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                alldcr1.Add(new DcrDetailsByUsers
                                {
                                    slno = Convert.ToInt32(dr["SLNo"].ToString()),
                                    excutivenm = Convert.ToString(dr["name"].ToString()),
                                    dcrdate = Convert.ToString(dr["dcrdate"].ToString()),
                                    dcrtime = String.Format("{0:00.00}", Convert.ToDouble(dr["dcrtime"].ToString())),
                                    // stat = Convert.ToString(dr["stat"].ToString()),
                                    duration = Convert.ToString(dr["duration"].ToString()),
                                    remark = Convert.ToString(dr["remark"].ToString()),
                                    contactmode = Convert.ToString(dr["contactmode"].ToString()),
                                    partycat = Convert.ToString(dr["partycat"].ToString()),
                                    orgnm = Convert.ToString(dr["orgnm"].ToString()),
                                    addr = Convert.ToString(dr["addr"].ToString()),
                                    purpose = Convert.ToString(dr["purpose"].ToString()),
                                    name = Convert.ToString(dr["excutivenm"].ToString()),
                                    priority = Convert.ToString(dr["dcrpriority"].ToString()),
                                    product = Convert.ToString(dr["product"].ToString()),
                                    areanm = Convert.ToString(dr["areanm"].ToString()),
                                    citynm = Convert.ToString(dr["citynm"].ToString()),
                                });
                            }
                            g1.close_connection();
                            alldcr.Add(new DcrDetailsByUser
                            {
                                result = "True",
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
                            response.Content = new StringContent(cm.StatusTime(false, "You have not a single entry In last 30 days"), Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                else if (lsa.flag == 2)
                {
                    try
                    {
                        int row = g1.ExecDB("exec dcraddforleadnew '" + lsa.dcrdate + "'," + lsa.dcrtime + ",'" + lsa.duration + "'," + lsa.modeid + "," + lsa.partycategoryid + "," + lsa.organizationid + "," + lsa.addressid + ",'" + lsa.cp + "','" + lsa.purposeid + "','" + lsa.productcategoryid + "','" + lsa.dcrpriority + "'," + lsa.refcat + "," + lsa.refid + ",'" + lsa.remark + "','" + lsa.lastsyncdt + "'," + lsa.userid + string.Empty);
                        if (row > 0)
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "Lead Sucessfully submitted"), Encoding.UTF8, "application/json");

                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Please flag should be 1 or 2"), Encoding.UTF8, "application/json");

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