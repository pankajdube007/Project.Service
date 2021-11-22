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
    public class ListofBranchController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetBranchCategoryList")]
        public HttpResponseMessage GetAllUserLatLong(ListofBranchAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;

                    List<Branches> alldcr = new List<Branches>();
                    List<Branch> alldcr1 = new List<Branch>();
                    List<BranchD> alldcr2 = new List<BranchD>();
                    List<catD> alldcr3 = new List<catD>();
                    var dr = g1.return_dr("App_BranchDeatils " + ula.userid + ",'" + ula.usercat + "'");
                    var dr1 = g1.return_dr("App_CatDeatils");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr2.Add(new BranchD
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                branchnm = Convert.ToString(dr["locnm"].ToString()),
                            });

                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    alldcr3.Add(new catD
                                    {
                                        catid = Convert.ToInt32(dr1["SlNo"].ToString()),
                                        catname = Convert.ToString(dr1["categorynm"].ToString()),
                                    });
                                }
                            }
                        }

                        alldcr1.Add(new Branch
                        {
                            BranchData = alldcr2,
                            CatData = alldcr3,
                        });
                        g1.close_connection();
                        alldcr.Add(new Branches
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Branch available"), Encoding.UTF8, "application/json");

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