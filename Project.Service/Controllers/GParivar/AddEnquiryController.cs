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
    public class AddEnquiryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddEnquiryDetails")]
        public HttpResponseMessage GetDetails(ListAddEnquiryDetails ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddEnquiryDetails> alldcr = new List<AddEnquiryDetails>();
                    List<AddEnquiryDetail> alldcr1 = new List<AddEnquiryDetail>();
                    var dr = g2.return_dr("dbo.InsertEnquiryDetils '" + ula.ExId + "','" + ula.enquirytype + "','" + ula.onsitepersonname + "','" + ula.contactno + "','" + ula.pincoide + "','" + ula.cityid + "','" + ula.Addressline1 + "','" + ula.Addressline2 + "','" + ula.comment + "','"+ ula.visitorid + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddEnquiryDetail
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddEnquiryDetails
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Enquiry Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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