using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListTodList
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class TodLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodList> data { get; set; }
    }

    public class TodList
    {
        public string GroupName { get; set; }
        public string GroupId { get; set; }
        public string YearlyTargetAmt { get; set; }
        public string YearlySalesAmt { get; set; }

        public string q1amt { get; set; }
        public string q2amt { get; set; }
        public string q3amt { get; set; }
        public string q4amt { get; set; }
        public string apramt { get; set; }
        public string mayamt { get; set; }
        public string junamt { get; set; }
        public string julamt { get; set; }
        public string augamt { get; set; }
        public string sepamt { get; set; }
        public string octamt { get; set; }
        public string novamt { get; set; }
        public string decamt { get; set; }
        public string janamt { get; set; }
        public string febamt { get; set; }
        public string maramt { get; set; }

        public string q1amts { get; set; }
        public string q2amts { get; set; }
        public string q3amts { get; set; }
        public string q4amts { get; set; }
        public string apramts { get; set; }
        public string mayamts { get; set; }
        public string junamts { get; set; }
        public string julamts { get; set; }
        public string augamts { get; set; }
        public string sepamts { get; set; }
        public string octamts { get; set; }
        public string novamts { get; set; }
        public string decamts { get; set; }
        public string janamts { get; set; }
        public string febamts { get; set; }
        public string maramts { get; set; }

        public string YearlytradeSale { get; set; }
        public string q1tradesale { get; set; }
        public string q2tradesale { get; set; }
        public string q3tradesale { get; set; }
        public string q4tradesale { get; set; }
        public string aprtradesale{ get; set; }
        public string maytradesale{ get; set; }
        public string juntradesale{ get; set; }
        public string jultradesale{ get; set; }
        public string augtradesale{ get; set; }
        public string septradesale{ get; set; }
        public string octtradesale{ get; set; }
        public string novtradesale{ get; set; }
        public string dectradesale{ get; set; }
        public string jantradesale{ get; set; }
        public string febtradesale{ get; set; }
        public string martradesale{ get; set; }


        public string YearlyprojectSale { get; set; }
        public string q1projectsale { get; set; }
        public string q2projectsale { get; set; }
        public string q3projectsale { get; set; }
        public string q4projectsale { get; set; }
        public string aprprojectsale { get; set; }
        public string mayprojectsale { get; set; }
        public string junprojectsale { get; set; }
        public string julprojectsale { get; set; }
        public string augprojectsale { get; set; }
        public string sepprojectsale { get; set; }
        public string octprojectsale { get; set; }
        public string novprojectsale { get; set; }
        public string decprojectsale { get; set; }
        public string janprojectsale { get; set; }
        public string febprojectsale { get; set; }
        public string marprojectsale { get; set; }

        public string YearlyEarnedAmt { get; set; }
        public string q1earnedamt { get; set; }
        public string q2earnedamt { get; set; }
        public string q3earnedamt { get; set; }
        public string q4earnedamt { get; set; }
        public string aprearnedamt { get; set; }
        public string mayearnedamt { get; set; }
        public string junearnedamt { get; set; }
        public string julearnedamt { get; set; }
        public string augearnedamt { get; set; }
        public string sepearnedamt { get; set; }
        public string octearnedamt { get; set; }
        public string novearnedamt { get; set; }
        public string decearnedamt { get; set; }
        public string janearnedamt { get; set; }
        public string febearnedamt { get; set; }
        public string marearnedamt { get; set; }


        public bool isAccept { get; set; }
        public bool termAccept { get; set; }
        public bool showTerm { get; set; }
        public string terms { get; set; }
        
    }
}