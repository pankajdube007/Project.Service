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
    public class GetVisitorListController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getlistofvisitor")]
        public HttpResponseMessage GetDetails(ListofVisitorDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetListofVisitorDetails> alldcr = new List<GetListofVisitorDetails>();
                    List<GetListofVisitorDetail> alldcr1 = new List<GetListofVisitorDetail>();
                    var dr = g1.return_dr("dbo.GetVisitorBasicDetails '" + ula.ExId + "','" + ula.SearchBy + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListofVisitorDetail
                            {
                                visitorid = Convert.ToInt32(dr["slno"].ToString()),
                                ExId = Convert.ToInt32(dr["ExId"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                tyepeofvisitor = Convert.ToInt32(dr["tyepeofvisitor"].ToString()),
                                visitorcode = Convert.ToString(dr["visitorcode"].ToString()),
                                visitortype = Convert.ToString(dr["visitortype"].ToString()),
                                visitorimg = Convert.ToString(dr["visitorimgLink"].ToString()),
                                fullaname = Convert.ToString(dr["fullaname"].ToString()),
                                Mobile = Convert.ToString(dr["Mobile"].ToString()),
                                emailid = Convert.ToString(dr["emailid"].ToString()),
                                pincode = Convert.ToString(dr["pincode"].ToString()),
                                cityid = Convert.ToInt32(dr["cityid"].ToString()),
                                Address = Convert.ToString(dr["Address"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                                companyname = Convert.ToString(dr["companyname"].ToString()),
                                concernperson = Convert.ToString(dr["concernperson"].ToString()),
                                designation = Convert.ToString(dr["designation"].ToString()),
                                contactno = Convert.ToString(dr["contactno"].ToString()),
                                Companyemail = Convert.ToString(dr["email"].ToString()),
                                Companypin = Convert.ToString(dr["pin"].ToString()),
                                CompanyCityId = Convert.ToInt32(dr["city1"].ToString()),
                                CompanyCityName = Convert.ToString(dr["citynm1"].ToString()),
                                CompanyAddress1 = Convert.ToString(dr["Address1"].ToString()),
                                CompanyAddress2 = Convert.ToString(dr["Address2"].ToString()),
                                visitingcardimg = Convert.ToString(dr["visitingcardimgLink"].ToString()),
                                typeofvisitor1 = Convert.ToString(dr["visitortype1"].ToString()),
                                FullNamevisitor1 = Convert.ToString(dr["FullNamevisitor1"].ToString()),
                                mobilevisitor1 = Convert.ToString(dr["mobilevisitor1"].ToString()),
                                typeofvisitor2 = Convert.ToString(dr["visitortype2"].ToString()),
                                FullNamevisitor2 = Convert.ToString(dr["FullNamevisitor2"].ToString()),
                                mobilevisitor2 = Convert.ToString(dr["mobilevisitor2"].ToString()),
                                leadtypeID = Convert.ToInt32(dr["leadtype"].ToString()),
                                leadtype = Convert.ToString(dr["leadtypeName"].ToString()),
                                followupdatetime = Convert.ToString(dr["followupdatetimeformat"].ToString()),
                                followupremark = Convert.ToString(dr["followupremark"].ToString()),
                                itemid = Convert.ToString(dr["itemidlist"].ToString()),
                                itemids = Convert.ToString(dr["itemid"].ToString()),
                                stateid = Convert.ToInt32(dr["stateidData"].ToString()),
                                statename = Convert.ToString(dr["statename"].ToString()),
                                companystateid = Convert.ToInt32(dr["companystateid"].ToString()),
                                companystatename = Convert.ToString(dr["companystatename"].ToString()),
                                ShowroomName = Convert.ToString(dr["sName"].ToString()),
                                Showroomid = Convert.ToInt32(dr["showroomid1"])                                                              

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetListofVisitorDetails
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