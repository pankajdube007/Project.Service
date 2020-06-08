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
    public class CategoryWiseFilterController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCategoryWiseFilter")]
        public HttpResponseMessage GetDetails(ListsofCategoryWiseFilter ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<CategoryWiseFilters> alldcr = new List<CategoryWiseFilters>();
                    List<CategoryWiseFilterdistrict> district = new List<CategoryWiseFilterdistrict>();
                    List<CategoryWiseFilterarea> area = new List<CategoryWiseFilterarea>();
                    List<CategoryWiseFilterbranch> branch = new List<CategoryWiseFilterbranch>();
                    List<CategoryWiseFilterexcutive> executive = new List<CategoryWiseFilterexcutive>();
                    List<CategoryWiseFilterFinal> Final = new List<CategoryWiseFilterFinal>();
                    var dr = g1.return_dr("AppcategorywiseFilter " + ula.ExId + "," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string[] dislist = dr["District"].ToString().Split(',');

                            foreach (var item in dislist)
                            {
                                string[] data = item.Split('-');
                                district.Add(new CategoryWiseFilterdistrict
                                {
                                    districtid = Convert.ToInt32(data[0]),
                                    district = data[1].ToString()
                                });
                            }

                            string[] arealist = dr["area"].ToString().Split(',');

                            foreach (var item in arealist)
                            {
                                string[] data = item.Split('-');
                                area.Add(new CategoryWiseFilterarea
                                {
                                    areaid = Convert.ToInt32(data[0]),
                                    area = data[1].ToString()
                                });
                            }

                            string[] branchlist = dr["branch"].ToString().Split(',');

                            foreach (var item in branchlist)
                            {
                                string[] data = item.Split('-');
                                branch.Add(new CategoryWiseFilterbranch
                                {
                                    branchid = Convert.ToInt32(data[0]),
                                    branch = data[1].ToString()
                                });
                            }

                            string[] executivelist = dr["executive"].ToString().Split(',');

                            foreach (var item in executivelist)
                            {
                                string[] data = item.Split('-');
                                executive.Add(new CategoryWiseFilterexcutive
                                {
                                    exid = Convert.ToInt32(data[0]),
                                    executive = data[1].ToString()
                                });
                            }

                            Final.Add(new CategoryWiseFilterFinal
                            {
                                districts = district,
                                areas = area,
                                branches = branch,
                                executives = executive
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new CategoryWiseFilters
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Final,
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