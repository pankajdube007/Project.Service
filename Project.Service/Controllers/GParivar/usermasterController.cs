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
    public class usermasterController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getusermaster")]
        public HttpResponseMessage userdetails(usermaster user)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            string data1 = string.Empty;
            //  DataConnectionTrans g2 = new DataConnectionTrans();

            List<usermasters> alldcr = new List<usermasters>();
            List<userdetails> alldcr1 = new List<userdetails>();

            var dr = g1.return_dr("getusermast '"+  user.emailid +"'");

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    alldcr1.Add(new userdetails
                    {
                        slno = Convert.ToInt32(dr["SlNo"].ToString()),
                        name = Convert.ToString(dr["name"].ToString()),
                        usernm = Convert.ToString(dr["usernm"].ToString()),
                        password = Convert.ToString(dr["password"].ToString()),
                    });
                }
                g1.close_connection();
                alldcr.Add(new usermasters
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
                response.Content = new StringContent(cm.StatusTime(false, "No Items available"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}