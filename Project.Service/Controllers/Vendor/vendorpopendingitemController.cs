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
    public class vendorpopendingitemController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorpopendingitem")]
        public HttpResponseMessage GetDetails(Listofvendorpopendingitem ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<vendorpopendingitems> alldcr = new List<vendorpopendingitems>();
                    List<vendorpopendingitem> alldcr1 = new List<vendorpopendingitem>();
                    var dr = g1.return_dr("spVendorProcpopendingitemEditedapp '" + ula.branchIDs + "','" + ula.distdlid + "','" + ula.asondate + "','" + ula.divisionid1 + "','" + ula.pocat + "','" + ula.PartyID + "','" + ula.TypeCat + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorpopendingitem
                            {
                         
                                materialissuefrom = Convert.ToString(dr["materialissuefrom"].ToString()),
                                increaselimit = Convert.ToString(dr["increaselimit"].ToString()),
                                tolarencelimit1 = Convert.ToString(dr["tolarencelimit1"].ToString()),
                                Urgent = Convert.ToString(dr["Urgent"].ToString()),
                                ApproveQty = Convert.ToString(dr["ApproveQty"].ToString()),
                                DispatchQty = Convert.ToString(dr["DispatchQty"].ToString()),
                                addline1 = Convert.ToString(dr["addline1"].ToString()),
                                pending = Convert.ToString(dr["pending"].ToString()),
                                SchemeQty = Convert.ToString(dr["SchemeQty"].ToString()),
                                BaseCode = Convert.ToString(dr["BaseCode"].ToString()),
                                itemnm = Convert.ToString(dr["itemnm"].ToString()),
                                colornm = Convert.ToString(dr["colornm"].ToString()),
                                rangenm = Convert.ToString(dr["rangenm"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                unitnm = Convert.ToString(dr["unitnm"].ToString()),
                                slno = Convert.ToString(dr["slno"].ToString()),
                                PCategory = Convert.ToString(dr["PCategory"].ToString()),
                                PartyName = Convert.ToString(dr["PartyName"].ToString()),
                                MaterialIssueBranch = Convert.ToString(dr["MaterialIssueBranch"].ToString()),
                                areanm = Convert.ToString(dr["areanm"].ToString()),
                                city = Convert.ToString(dr["city"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                salesexname = Convert.ToString(dr["salesexname"].ToString()),
                                PartyType = Convert.ToString(dr["PartyType"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                mobile = Convert.ToString(dr["mobile"].ToString()),
                                SchemePer = Convert.ToString(dr["SchemePer"].ToString()),
                                cartoonbox = Convert.ToString(dr["cartoonbox"].ToString()),
                                boxqty = Convert.ToString(dr["boxqty"].ToString()),
                                lose = Convert.ToString(dr["lose"].ToString()),
                                Stockqty = Convert.ToString(dr["Stockqty"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorpopendingitems
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