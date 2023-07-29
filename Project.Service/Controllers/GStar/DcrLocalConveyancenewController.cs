using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class DcrLocalConveyancenewController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDcrLocalConveyancenew")]
        public HttpResponseMessage GetDetails(ListofDcrLocalConveyancenew ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetDcrLocalConveyanceListsnew> alldcr = new List<GetDcrLocalConveyanceListsnew>();
                    List<GetDcrLocalConveyanceListnew> alldcr1 = new List<GetDcrLocalConveyanceListnew>();

                    List<GetexeccheckinoutlistdtwisesumListnew> alldcr2 = new List<GetexeccheckinoutlistdtwisesumListnew>();

                    List<GetFinalListsnew> Final = new List<GetFinalListsnew>();

                    var dr = g1.return_dr("dbo.execcheckinoutlistdtwisenew '" + ula.ExId + "','" + ula.date + "','" + ula.EmpType + "'");
                    var dr1 = g1.return_dr("dbo.execcheckinoutlistdtwisesumnew '" + ula.ExId + "','" + ula.date + "','" + ula.transportid + "', '" + ula.slno + "','" + ula.EmpType + "' ");


                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetDcrLocalConveyanceListnew
                            {

                                orgid = Convert.ToString(dr["orgid"].ToString()),
                                orgcat = Convert.ToString(dr["orgcat"].ToString()),
                                traveldistance = Convert.ToString(dr["traveldistance"].ToString()),
                                orgnm = Convert.ToString(dr["orgnm"].ToString()),
                                travelduration = Convert.ToString(dr["timediff"].ToString()),
                                transport = Convert.ToString(dr["transport"].ToString()),
                            });
                        }

                    }

                    if (dr1.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr1.Read())
                        {
                            alldcr2.Add(new GetexeccheckinoutlistdtwisesumListnew
                            {

                                grossdistance = Convert.ToString(dr1["grossdistance"].ToString()),
                                claimablediastance = Convert.ToString(dr1["claimablediastance"].ToString()),
                                odomtrkm = Convert.ToString(dr1["odomtrkm"].ToString()),
                                trainlimit = Convert.ToString(dr1["trainlimit"].ToString()),
                                MetroLimit = Convert.ToString(dr1["MetroLimit"].ToString()),
                                TollLimit = Convert.ToString(dr1["TollLimit"].ToString()),
                                Buslimit = Convert.ToString(dr1["Buslimit"].ToString()),
                                AutoLimit = Convert.ToString(dr1["AutoLimit"].ToString()),
                                RentalLimit = Convert.ToString(dr1["RentalLimit"].ToString()),
                                TrvlLimit = Convert.ToString(dr1["TrvlLimit"].ToString()),
                                CarRate = Convert.ToString(dr1["CarRate"].ToString()),
                                BikeRate = Convert.ToString(dr1["BikeRate"].ToString()),
                                SameDaykm = Convert.ToString(dr1["SameDaykm"].ToString()),
                                SameDayAmount = Convert.ToString(dr1["SameDayAmount"].ToString()),
                                TrainUsed = Convert.ToString(dr1["TrainUsed"].ToString()),
                                MetroUsed = Convert.ToString(dr1["MetroUsed"].ToString()),
                                TollUsed = Convert.ToString(dr1["TollUsed"].ToString()),
                                BusUsed = Convert.ToString(dr1["BusUsed"].ToString()),
                                AutoUsed = Convert.ToString(dr1["AutoUsed"].ToString()),
                                RentalUsed = Convert.ToString(dr1["RentalUsed"].ToString()),
                                trvlUsed = Convert.ToString(dr1["trvlUsed"].ToString()),
                                TrainBalance = Convert.ToString(dr1["TrainBalance"].ToString()),
                                MetroBalance = Convert.ToString(dr1["MetroBalance"].ToString()),
                                TollBalance = Convert.ToString(dr1["TollBalance"].ToString()),
                                BusBalance = Convert.ToString(dr1["BusBalance"].ToString()),
                                AutoBalance = Convert.ToString(dr1["AutoBalance"].ToString()),
                                RentalBalance = Convert.ToString(dr1["RentalBalance"].ToString()),
                                balancekm = Convert.ToString(dr1["balancekm"].ToString()),
                                slno = Convert.ToString(dr1["slno"].ToString()),


                                InsertedOdoMtr = Convert.ToString(dr1["InsertedOdoMtr"].ToString()),
                                InsertedSelf = Convert.ToString(dr1["InsertedSelf"].ToString()),
                                InsertedtrvlMode = Convert.ToString(dr1["InsertedtrvlMode"].ToString()),
                                Insertedtrain = Convert.ToString(dr1["Insertedtrain"].ToString()),
                                Insertedmetro = Convert.ToString(dr1["Insertedmetro"].ToString()),
                                Insertedrentalcar = Convert.ToString(dr1["Insertedrentalcar"].ToString()),
                                Insertedbus = Convert.ToString(dr1["Insertedbus"].ToString()),
                                Insertedauto = Convert.ToString(dr1["Insertedauto"].ToString()),
                                Insertedtollparking = Convert.ToString(dr1["Insertedtollparking"].ToString()),
                                insertedRemark = Convert.ToString(dr1["insertedRemark"].ToString()),


                                isapprove = Convert.ToString(dr1["isapprove"].ToString()),
                                isfix = Convert.ToString(dr1["isfix"].ToString()),
                                fixamt = Convert.ToString(dr1["fixamt"].ToString()),
                                localfood = Convert.ToString(dr1["foodamount"].ToString()),
                                outstationlimit = Convert.ToString(dr1["outstationlimit"].ToString()),
                                outstationused = Convert.ToString(dr1["outstationused"].ToString()),
                                outstationbalance = Convert.ToString(dr1["OutStationBalance"].ToString()),
                                isbtnshow = Convert.ToString(dr1["isbtnshow"].ToString()),
                                Insertedfood = Convert.ToString(dr1["insertedfood"].ToString()),
                                Insertedoutstation = Convert.ToString(dr1["InsertedOutStation"].ToString()),
                                Insertedfixamt = Convert.ToString(dr1["Insertedfixamt"].ToString()),

                                insertedtrainimg = Convert.ToString(dr1["insertedtrainimg"].ToString()),
                                insertedmetroimg = Convert.ToString(dr1["insertedmetroimg"].ToString()),
                                insertedrentalcarimg = Convert.ToString(dr1["insertedrentalcarimg"].ToString()),
                                insertedtollparkingimg = Convert.ToString(dr1["insertedtollparkingimg"].ToString()),
                                insertedautoimg = Convert.ToString(dr1["insertedautoimg"].ToString()),
                                insertedbusimg = Convert.ToString(dr1["insertedbusimg"].ToString()),
                                insertedoutstationimg = Convert.ToString(dr1["insertedoutstationimg"].ToString()),
                                Foodimg = Convert.ToString(dr1["Foodimg"].ToString())
                                //insertedtrainimg=string.IsNullOrEmpty(dr1["insertedtrainimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedtrainimg"]).ToString().TrimEnd(',')),
                                //insertedmetroimg = string.IsNullOrEmpty(dr1["insertedmetroimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedmetroimg"]).ToString().TrimEnd(',')),
                                //insertedrentalcarimg = string.IsNullOrEmpty(dr1["insertedrentalcarimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedrentalcarimg"]).ToString().TrimEnd(',')),
                                //insertedtollparkingimg = string.IsNullOrEmpty(dr1["insertedtollparkingimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedtollparkingimg"]).ToString().TrimEnd(',')),
                                //insertedautoimg = string.IsNullOrEmpty(dr1["insertedautoimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedautoimg"]).ToString().TrimEnd(',')),
                                //insertedbusimg = string.IsNullOrEmpty(dr1["insertedbusimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedbusimg"]).ToString().TrimEnd(',')),
                                //insertedoutstationimg = string.IsNullOrEmpty(dr1["insertedoutstationimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["insertedoutstationimg"]).ToString().TrimEnd(',')),
                                //Foodimg = string.IsNullOrEmpty(dr1["Foodimg"].ToString().TrimEnd(',')) ? string.Empty : (baseurl + "inspectionimg/" + Convert.ToString(dr1["Foodimg"]).ToString().TrimEnd(','))

                            });
                        }

                    }

                    Final.Add(new GetFinalListsnew
                    {

                        LocalConveyanceList = alldcr1,
                        listdtwisesumList = alldcr2

                        //listdtwisesumList = alldcr2,
                        //LocalConveyanceList= alldcr1
                    });


                    g1.close_connection();
                    alldcr.Add(new GetDcrLocalConveyanceListsnew
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
                    //}
                    //else
                    //{
                    //    g1.close_connection();
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                    //    return response;
                    //}
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