namespace Sportiada.Web.Areas.Football.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Football.Models.Game;
    using Services.Football.Models.Round;
    using Services.Football.Models.Season;   
    using System.Collections.Generic;

    public class GameWithSeasonViewModel
    {
        public List<GameModel> Games { get; set; }

        public List<SeasonModel> Seasons { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviuosPage => CurrentPage == 1 ? CurrentPage : CurrentPage - 1;

        public int NextPage => CurrentPage == 1 ? CurrentPage : CurrentPage + 1;
    }
}
