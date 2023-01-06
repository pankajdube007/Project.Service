using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Service.Models.Vendor;
using Newtonsoft.Json.Serialization;
using System.IO;


namespace Project.Service.Controllers.Vendor
{
    public class vendorinspectionreqitemappController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addvendorinspectionreqitemapp")]
        public HttpResponseMessage GetDetails(ListAddvendorinspectionreqitemapp ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;

            if (ula.cin != "")
            {

                string data1;

                List<AddvendorinspectionreqitemappLists> alldcr = new List<AddvendorinspectionreqitemappLists>();
                List<Addvendorinspectionreqitemapp> alldcr1 = new List<Addvendorinspectionreqitemapp>();


                var dr = g2.return_dr("vendoriteminspectionreqapp '" + ula.vendorid + "','" + ula.Category + "','"+ ula.insepectiondate + "','" + ula.remark + "','" + ula.itemids + "'");

                if (dr.HasRows)
                {
                    alldcr1.Add(new Addvendorinspectionreqitemapp
                    {
                        output = "Data Sucessfully inserted"
                    });

                    g2.close_connection();
                    alldcr.Add(new AddvendorinspectionreqitemappLists
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
                    response.Content = new StringContent(cm.StatusTime(false, "Add Trip Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }


            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}