using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofDcrLocalConveyance
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transportid { get; set; }

        [Required]
        public string slno { get; set; }
        

    }

    public class GetDcrLocalConveyanceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFinalLists> data { get; set; }
       
    }

    public class GetFinalLists
    {
        public List<GetDcrLocalConveyanceList> LocalConveyanceList { get; set; }
        public List<GetexeccheckinoutlistdtwisesumList> listdtwisesumList { get; set; }
    }

    public class GetDcrLocalConveyanceList
    {
        public string orgid { get; set; }
        public string orgcat { get; set; }
        public string traveldistance { get; set; }
        public string orgnm { get; set; }
        public string travelduration { get; set; }
        
    }

    public class GetexeccheckinoutlistdtwisesumList
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
        public string TrainBalance { get; set; }
        public string MetroBalance { get; set; }
        public string TollBalance { get; set; }
        public string BusBalance { get; set; }
        public string AutoBalance { get; set; }
        public string RentalBalance { get; set; }
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
        public string isapprove { get; set; }
    }
}