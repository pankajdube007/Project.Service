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
    public class PassengersDetailsByBranchStateAirportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpassengersDetailsByBranchStateAirport")]
        public HttpResponseMessage GetDetails(ListofPassengersDetailsByBranchStateAirport ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PassengersDetailsByBranchStateAirportFinal> final = new List<PassengersDetailsByBranchStateAirportFinal>();
                   // List<PassengersDetailsByBranchStateAirport> head = new List<PassengersDetailsByBranchStateAirport>();
                   
                    List<PassengersDetailsByBranchStateAirports> head1 = new List<PassengersDetailsByBranchStateAirports>();



                    var dr = g1.return_dt("GetPassengersDetailsByBranchStateAirport '" + ula.CIN + "','" + ula.Category + "','" + ula.Type + "','" + ula.Typeid + "'");

                    if (dr.Rows.Count>0)
                    {
                        for(int i=0;i<dr.Rows.Count;i++)
                        {

                            var dr1 = g1.return_dr("GetPassengerGroupDetails '" + Convert.ToString(dr.Rows[i]["ProfileID"].ToString()) + "'");
                            List<PassengersDetailsByBranchStateAirportdetail> child = new List<PassengersDetailsByBranchStateAirportdetail>();
                            if (dr1.HasRows)
                            {

                                //child = null;
                                while (dr1.Read())
                                {
                                    child.Add(new PassengersDetailsByBranchStateAirportdetail
                                    {
                                        PassengerName = Convert.ToString(dr1["PassengerName"].ToString()),
                                        RelationName = Convert.ToString(dr1["RelationName"].ToString()),
                                        MobileNo = Convert.ToString(dr1["MobileNo"].ToString()),
                                        TravelIDNo = Convert.ToString(dr1["TravelIDNo"].ToString()),
                                        ProfileID = Convert.ToString(dr1["ProfileID"].ToString()),
                                        UserType = Convert.ToString(dr1["UserType"].ToString()),
                                        ApprovalStatus = Convert.ToString(dr1["ApprovalStatus"].ToString()),

                                    });
                                }

                            }
     

                            final.Add(new PassengersDetailsByBranchStateAirportFinal
                            {
                                PassengerName = Convert.ToString(dr.Rows[i]["PassengerName"].ToString()),
                                RelationName = Convert.ToString(dr.Rows[i]["RelationName"].ToString()),
                                MobileNo = Convert.ToString(dr.Rows[i]["MobileNo"].ToString()),
                                TravelIDNo = Convert.ToString(dr.Rows[i]["TravelIDNo"].ToString()),
                                BranchName = Convert.ToString(dr.Rows[i]["BranchName"].ToString()),
                                CategoryName = Convert.ToString(dr.Rows[i]["CategoryName"].ToString()),
                                ProfileID = Convert.ToString(dr.Rows[i]["ProfileID"].ToString()),
                                ShopName = Convert.ToString(dr.Rows[i]["ShopName"].ToString()),
                                ApprovalStatus = Convert.ToString(dr.Rows[i]["ApprovalStatus"].ToString()),
                                child =child

                            });


                  
                        }
                       

                    }

                   

                 
                    g1.close_connection();
                    head1.Add(new PassengersDetailsByBranchStateAirports
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = final,
                    });
                    data1 = JsonConvert.SerializeObject(head1, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;

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