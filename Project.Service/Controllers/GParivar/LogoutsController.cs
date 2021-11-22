using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "POST")]
    public class LogoutsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Logout")]
        public HttpResponseMessage Logout(LogoutAction la)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(la.uniquekey))
            {
                try
                {
                    string key = cm.GetValidKey(la.uniquekey);
                    if (!string.IsNullOrEmpty(key))
                    {
                        int row = g1.ExecDB("exec dcrlogout " + la.userid + ",'" + key + "'");
                        if (row > 0)
                        {
                            g1.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "Logout Sucessfully"), Encoding.Unicode);

                            return response;
                        }
                        else
                        {
                            g1.close_connection();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong"), Encoding.Unicode);

                            return response;
                        }
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong"), Encoding.Unicode);

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.Unicode);

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.Unicode);

                return response;
            }
        }
    }
}