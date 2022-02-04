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
    public class SignUpController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/SignUp")]
        public HttpResponseMessage GetItemDetails(SignUp ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();


            try
            {
                string data1;
                string Message = "Congratulations you have registered succesfully. After offline verification you will receive a link on your email, please click on it for succesfull login";

                List<SignUpData> alldcr = new List<SignUpData>();
                List<SignUpDataValue> alldcr1 = new List<SignUpDataValue>();

                var dr = g2.return_dr("getSignUpData '" + ula.Name + "','" + ula.ContactNumber + "','" + ula.Email + "','" + ula.Password + "','" + ula.ConfirmPassword + "'");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new SignUpDataValue
                        {


                            Name = Convert.ToString(dr["Name"].ToString()),
                            ContactNumber = Convert.ToString(dr["ContactNumber"].ToString()),
                            Email = Convert.ToString(dr["Email"].ToString()),
                            Password = Convert.ToString(dr["Password"].ToString()),
                            ConfirmPassword = Convert.ToString(dr["ConfirmPassword"].ToString()),

                        });
                    }
                    g2.close_connection();

                    alldcr.Add(new SignUpData
                    {
                        result = true,
                        message = Message,
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
                    g2.close_connection();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
    }
}