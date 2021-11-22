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
    public class UserapprovalstatusanddistrictwisedetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getUserapprovalstatusanddistrictwisedetails")]
        public HttpResponseMessage GetDetails(ListUserapprovalstatusanddistrictwisedetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<UserapprovalstatusanddistrictwisedetailsLists> alldcr = new List<UserapprovalstatusanddistrictwisedetailsLists>();
                    List<UserapprovalstatusanddistrictwisedetailsList> alldcr1 = new List<UserapprovalstatusanddistrictwisedetailsList>();

                    var dr = g1.return_dr("Userapprovalstatusanddistrictwisedetails '"+ ula.Category+"','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.ApproveStatus + "','" + ula.Cat + "','" + ula.Districtname + "' ");

                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            alldcr1.Add(new UserapprovalstatusanddistrictwisedetailsList
                            {
                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                Name = Convert.ToString(dr["Name"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                Category = Convert.ToString(dr["Category"].ToString()),
                                Address = Convert.ToString(dr["Address"].ToString()),
                                City = Convert.ToString(dr["City"].ToString()),
                                State = Convert.ToString(dr["State"].ToString()),
                                District = Convert.ToString(dr["District"].ToString()),
                                ShopName = Convert.ToString(dr["ShopName"].ToString()),
                                JoinDate = Convert.ToString(dr["JoinDate"].ToString()),
                               
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new UserapprovalstatusanddistrictwisedetailsLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data"), Encoding.UTF8, "application/json");

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