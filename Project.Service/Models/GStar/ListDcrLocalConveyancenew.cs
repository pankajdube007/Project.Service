using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofDcrLocalConveyancenew
    {
        [Required]
        public int ExId { get; set; }

        public int EmpType { get; set; }
        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transportid { get; set; }

        [Required]
        public string slno { get; set; }


    }

    public class GetDcrLocalConveyanceListsnew
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFinalListsnew> data { get; set; }

    }

    public class GetFinalListsnew
    {
        public List<GetDcrLocalConveyanceListnew> LocalConveyanceList { get; set; }
        public List<GetexeccheckinoutlistdtwisesumListnew> listdtwisesumList { get; set; }
    }

    public class GetDcrLocalConveyanceListnew
    {
        public string orgid { get; set; }
        public string orgcat { get; set; }
        public string traveldistance { get; set; }
        public string orgnm { get; set; }
        public string travelduration { get; set; }
        public string transport { get; set; }

    }

    public class GetexeccheckinoutlistdtwisesumListnew
    {
        public string grossdistance { get; set; }
        public string claimablediastance { get; set; }
        public string odomtrkm { get; set; }
        public string trainlimit { get; set; }
        public string MetroLimit { get; set; }
        public string TollLimit { get; set; }
        public string Buslimit { get; set; }
        public string AutoLimit { get; set; }
        public string RentalLimit { get; set; }
        public string TrvlLimit { get; set; }
        public string OtherLimit { get; set; }
        public string CarRate { get; set; }
        public string BikeRate { get; set; }
        public string SameDaykm { get; set; }
        public string SameDayAmount { get; set; }
        public string TrainUsed { get; set; }
        public string MetroUsed { get; set; }
        public string TollUsed { get; set; }
        public string BusUsed { get; set; }
        public string AutoUsed { get; set; }
        public string RentalUsed { get; set; }
        public string trvlUsed { get; set; }

        public string OtherUsed { get; set; }
        public string TrainBalance { get; set; }
        public string MetroBalance { get; set; }
        public string TollBalance { get; set; }
        public string BusBalance { get; set; }
        public string AutoBalance { get; set; }
        public string RentalBalance { get; set; }

        public string OtherBalance { get; set; }
        public string balancekm { get; set; }
        public string slno { get; set; }
        public string InsertedOdoMtr { get; set; }
        public string InsertedSelf { get; set; }
        public string InsertedtrvlMode { get; set; }
        public string Insertedtrain { get; set; }
        public string Insertedmetro { get; set; }
        public string Insertedrentalcar { get; set; }
        public string Insertedbus { get; set; }
        public string Insertedauto { get; set; }
        public string Insertedtollparking { get; set; }
        public string insertedRemark { get; set; }
        public string insertedOther { get; set; }


        public string isapprove { get; set; }

        public string localfood { get; set; }
        public string fixamt { get; set; }
        public string isfix { get; set; }
        public string outstationlimit { get; set; }
        public string outstationused { get; set; }
        public string outstationbalance { get; set; }
        public string isbtnshow { get; set; }
        public string Insertedfood { get; set; }
        public string Insertedoutstation { get; set; }
        public string Insertedfixamt { get; set; }
        public string insertedtrainimg { get; set; }
        public string insertedmetroimg { get; set; }
        public string insertedrentalcarimg { get; set; }
        public string insertedtollparkingimg { get; set; }
        public string insertedautoimg { get; set; }
        public string insertedbusimg { get; set; }
        public string insertedoutstationimg { get; set; }
        public string insertedotherimg { get; set; }
        public string Foodimg { get; set; }
    }
}