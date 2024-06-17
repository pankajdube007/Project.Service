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
    public class GetProductDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetProductDetails")]
        public HttpResponseMessage GetDetails(GetProductDetailsList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetProductDetailsLists> alldcr = new List<GetProductDetailsLists>();
                    List<GetProductDetails> alldcr1 = new List<GetProductDetails>();
                    var dr = g1.return_dr($"usp_GetProductDetails_API  {ula.ProductId}, {ula.UserCategoryID} , '{ula.Category}' ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetProductDetails
                            {
                                ProductID = Convert.ToString(dr["ProductID"]),
                                Name = Convert.ToString(dr["Name"]),
                                Sku = Convert.ToString(dr["Sku"]),
                                ManufaturerPartNumber = Convert.ToString(dr["ManufaturerPartNumber"]),
                                HSN = Convert.ToString(dr["HSN"]),
                                ProductCategory = Convert.ToString(dr["ProductCategory"]),
                                ProductSubCategory = Convert.ToString(dr["ProductSubCategory"]),
                                ProductCategoryID = Convert.ToString(dr["ProductCategoryID"]),
                                ProductSubCategoryID = Convert.ToString(dr["ProductSubCategoryID"]),
                                ShortDescription = Convert.ToString(dr["ShortDescription"]),
                                ProductPic = Convert.ToString(dr["ProductPic"]),
                                Points = Convert.ToString(dr["Points"]),
                                UserCategoryID = Convert.ToString(dr["UserCategoryID"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetProductDetailsLists
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