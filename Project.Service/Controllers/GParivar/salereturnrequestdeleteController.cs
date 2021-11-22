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
    public class salereturnrequestdeleteController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/salereturnrequestdeletes")]
        public HttpResponseMessage GetDetails(salereturnrequestdeletelist ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    //string data;
                    List<salereturnrequestdeletes> alldcr = new List<salereturnrequestdeletes>();

                    var dr = g2.return_dr("salereturnrequestdelete " + "0," + ula.slno);

                    if (dr.HasRows)
                    {
                        g2.close_connection();

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Deleted successfully."), Encoding.UTF8, "application/json");

                        return response;

                        //alldcr.Add(new salereturnrequestdeletes
                        //{
                        //    result = true,
                        //    message = "Deleted successfully",
                        //    servertime = DateTime.Now.ToString(),
                        //    data = null,
                        //});
                        //data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        //response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        //return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Delete only allowed for 'Pending For Approval.'"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}