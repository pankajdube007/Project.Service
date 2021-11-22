using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListMatchSummaryExpertOpenion
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class ListMatchSummaryExpertOpenionLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListMatchSummaryExpertOpenionList> data { get; set; }
    }

    public class ListMatchSummaryExpertOpenionList
    {
        public int Matchsummaryid { get; set; }
        public string Event { get; set; }
        public int Team1id { get; set; }
        public int Team2id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public decimal team1point { get; set; }
        public decimal team2point { get; set; }
        public bool winnerlock { get; set; }
        public string winnerteam { get; set; }
        public bool othetplay { get; set; }
        public string othetplayteam { get; set; }
        public decimal team1per { get; set; }
        public decimal team2per { get; set; }
        public string venue { get; set; }
        public int othetplayteamid { get; set; }
        public int winnerlockteamid { get; set; }
        public int selectedteam { get; set; }
        public bool team1selected { get; set; }
        public bool team2selected { get; set; }
        public bool prediction { get; set; }
        public string result { get; set; }

        public string slab1four { get; set; }
        public string slab1fourslno { get; set; }
        public string slab1fourpt { get; set; }
        public string slab2four { get; set; }
        public string slab2fourslno { get; set; }
        public string slab2fourpt { get; set; }
        public string slab3four { get; set; }
        public string slab3fourslno { get; set; }
        public string slab3fourpt { get; set; }
        public string slab4four { get; set; }
        public string slab4fourslno { get; set; }
        public string slab4fourpt { get; set; }
        public string slab1six { get; set; }
        public string slab1sixslno { get; set; }
        public string slab1sixpt { get; set; }
        public string slab2six { get; set; }
        public string slab2sixslno { get; set; }
        public string slab2sixpt { get; set; }
        public string slab3six { get; set; }
        public string slab3sixslno { get; set; }
        public string slab3sixpt { get; set; }
        public string slab4six { get; set; }
        public string slab4sixslno { get; set; }
        public string slab4sixpt { get; set; }
        public string slab1scr { get; set; }
        public string slab1scrslno { get; set; }
        public string slab1scrpt { get; set; }
        public string slab2scr { get; set; }
        public string slab2scrslno { get; set; }
        public string slab2scrpt { get; set; }
        public string slab3scr { get; set; }
        public string slab3scrslno { get; set; }
        public string slab3scrpt { get; set; }
        public string slab4scr { get; set; }
        public string slab4scrslno { get; set; }
        public string slab4scrpt { get; set; }


        public string TossPrediction { get; set; }
        public string TossPredictiontteamid { get; set; }
        public string BatFirstPrediction { get; set; }
        public string BatFirstPredictionteamid { get; set; }
        public string selected4slabid { get; set; }
        public string selected6slabid { get; set; }
        public string selectedscoreslabid { get; set; }
        public string selected4slab { get; set; }
        public string selected6slab { get; set; }
        public string selectedscoreslab { get; set; }
        public string selected4slabpoint { get; set; }
        public string selected6slabpoint { get; set; }
        public string selectedscoreslabpoint { get; set; }




        public string resulttosswinteam { get; set; }
        public string resulttosswinteamid { get; set; }
        public string resultbatswinteam { get; set; }
        public string resultbatswinteamid { get; set; }

        public string resultfourslab { get; set; }
        public string resultsixslab { get; set; }
        public string resultscoreslab { get; set; }
        public string resultfourslabslno { get; set; }
        public string resultsixslabslno { get; set; }
        public string resultscoreslabslno { get; set; }


        public bool PredictionResultToss { get; set; }
        public bool PredictionResultBat { get; set; }
        public bool PredictionResultFour { get; set; }
        public bool PredictionResultSix { get; set; }
        public bool PredictionResultScor { get; set; }
        public string PredictionResultFourPoint { get; set; }
        public string PredictionResultsixPoint { get; set; }
        public string PredictionResultscorPoint { get; set; }
        public string PredictionResultTossPoint { get; set; }
        public string PredictionResultBatPoint { get; set; }
        public string IsAbandoned { get; set; }
        public string IsOpen { get; set; }



    }
}