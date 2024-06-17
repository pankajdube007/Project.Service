using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.Management;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class GetProductListCategorywiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetProductListCatwise")]
        public HttpResponseMessage GetDetails(GetProductListCategorywiseList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetProductListCategorywises> alldcr = new List<GetProductListCategorywises>();
                    List<GetProductListCategorywise> alldcr1 = new List<GetProductListCategorywise>();
                    var dr = g1.return_dr($"usp_GetProductListCategorywise_API  {ula.UserCategoryID}, {ula.ProductCategoryID} , {ula.index} , {ula.Count} ,  '{ula.Category}' ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetProductListCategorywise
                            {
                                ProductID = Convert.ToString(dr["ProductID"]),
                                Name = Convert.ToString(dr["Name"]),
                                Points = Convert.ToString(dr["Points"]),
                                ProductCategoryId = Convert.ToString(dr["ProductCategoryId"]),
                                ProductCategory = Convert.ToString(dr["ProductCategory"]),
                                ProductSubCategory = Convert.ToString(dr["ProductSubCategory"]),
                                ProductSubCategoryID = Convert.ToString(dr["ProductSubCategoryID"]),
                                ShortDescription = Convert.ToString(dr["ShortDescription"]),
                                ProductPic = Convert.ToString(dr["ProductPic"]),
                                TotalCount = Convert.ToString(dr["TotalCount"]),
                                UserCategoryID = Convert.ToString(dr["UserCategoryID"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetProductListCategorywises
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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