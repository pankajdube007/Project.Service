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
    public class ItemProblemVendorMappingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ItemProblemVendorMapping")]
        public HttpResponseMessage GetDetails(ItemProblemVendorMapping ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;


                List<ItemProblemVendors> alldcr = new List<ItemProblemVendors>();
                List<ItemProblemVendor> alldcr1 = new List<ItemProblemVendor>();
                var dr = g2.return_dr("ItemProblemVendorMappingUpdate '" + ula.QrCode.ToString() + "','" + ula.VendorID + "'");
      
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new ItemProblemVendor
                        {
                            output = "Data Sucessfully Updated",
                            TotalRecord = Convert.ToInt32(dr["TotalRecord"].ToString()),
                            UpdatedRecord = Convert.ToInt32(dr["UpdatedRecord"].ToString()),
                        });
                    }
                    g2.close_connection();
                    alldcr.Add(new ItemProblemVendors
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

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