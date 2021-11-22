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
    public class empdetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getemployeedetailList")]
        public HttpResponseMessage GetDetails(ListDetailEmp ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DetailEmpLists> alldcr = new List<DetailEmpLists>();
                    List<DetailEmpList> alldcr1 = new List<DetailEmpList>();

                    var dr = g1.return_dr("[hrm].[AllEmplistdetail]'" + ula.slno + "','" + ula.CIN + "'");
                   

                    if (dr.HasRows)
                    {
                        string baseurl1 = _goldMedia.MapPathToPublicUrl("");
                        //string baseurl = baseurl1.Replace("erp", "hrm");
                        string baseurl = "https://goldblobtest.blob.core.windows.net/goldappdata/goldapp/base/hrm/";

                        while (dr.Read())
                        {
                            alldcr1.Add(new DetailEmpList
                            {
                                
                                Name = Convert.ToString(dr["name"].ToString()),
                                Department = Convert.ToString(dr["dept"].ToString()),
                                Designation = Convert.ToString(dr["desg"].ToString()),
                                DOB = Convert.ToString(dr["bdt"].ToString()),
                                JoinDate = Convert.ToString(dr["jdt"].ToString()),
                                Location = Convert.ToString(dr["loc"].ToString()),
                                Sublocation = Convert.ToString(dr["sublo"].ToString()),
                                Father = Convert.ToString(dr["FatherName"].ToString()),
                                Mother = Convert.ToString(dr["MotherName"].ToString()),
                                WorkExp = Convert.ToString(dr["wrkyear"].ToString()),
                                ContactNo = Convert.ToString(dr["contactno"].ToString()),
                                Branch = Convert.ToString(dr["branch"].ToString()),
                                img = string.IsNullOrEmpty(dr["img"].ToString().Trim(',')) ? "" : (baseurl + "employeedocuments/" + dr["img"].ToString().Trim(',')),
                                CTC = Convert.ToString(dr["ctc"].ToString()),
                                Email = Convert.ToString(dr["email"].ToString()),
                                empcode = Convert.ToString(dr["empcode"].ToString()),
                                address = Convert.ToString(dr["address"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DetailEmpLists
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