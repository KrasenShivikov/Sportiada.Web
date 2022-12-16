namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Sportiada.Services.Football.Interfaces;
    using Sportiada.Services.Football.Models.Round;
    using Sportiada.Services.Football.Models.Season;
    using Sportiada.Web.Areas.Football.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Area("Football")]
    public class GameController : Controller
    {
        public const int PageSize = 20;
        private readonly IGameService games;
        private readonly ISeasonService seasons;
        private readonly IRoundService rounds;

        public GameController(IGameService games, ISeasonService seasons, IRoundService rounds)
        {
            this.games = games;
            this.seasons = seasons;
            this.rounds = rounds;
        }

        [Route("football/game/last20")]
        public IActionResult LastTwentyGames()
            => View(this.games.LastTwentyGames());

        [Route("football/game/{id}")]
        public IActionResult ById(int id)
        {
            var game = games.ById(id);
            return View (game);
        }
        

        [Route("football/game/tournament/{tournamentId}")]
        public IActionResult ByTournament(int tournamentId, int page = 1)
        {
            var model = new GameWithSeasonViewModel
            {
                Games = this.games.ByTournament(page, PageSize, tournamentId).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.games.CountByTournament(tournamentId) / (double)PageSize),
                Seasons = this.seasons
                    .All()
                    .ToList()
                    .Select(s => new SeasonModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).OrderByDescending(x => x.Name)
                    .ToList()
            };

            return View(model);
        }

        [Route("football/game/tournament/season/firstRound/{tournamentId}/{seasonId}")]
        public IActionResult ByTournamentBySeasonByFirstRound(int tournamentId, int seasonId)
        {
            RoundBaseModel firstRound = this.rounds.FirstRoundByTournamentBySeason(tournamentId, seasonId);

            return View(new GameRoundViewModel
            {
                rounds = this.rounds.ByTournamentBySeason(tournamentId, seasonId),
                games = this.games.ByRound(firstRound.Id),
                gamesForRanking = this.games.ByRounds(new List<int> { firstRound.Id })
            });
        }

        [Route("football/game/round/{tournamentId}/{seasonId}/{roundId}")]
        public IActionResult ByRound(int tournamentId, int seasonId, int roundId)
        {
            IEnumerable<RoundBaseModel> rounds = this.rounds.ByTournamentBySeason(tournamentId, seasonId);
            IEnumerable<int> roundIds = GetRoundIds(rounds, roundId);
            return View(new GameRoundViewModel
            {
                rounds = this.rounds.ByTournamentBySeason(tournamentId, seasonId),
                games = this.games.ByRound(roundId),
                gamesForRanking = this.games.ByRounds(roundIds)
            });
        }

        private IEnumerable<int> GetRoundIds(IEnumerable<RoundBaseModel> rounds, int roundId)
           => rounds
            .Where(r => r.Id <= roundId)
            .Select(r => r.Id);
    }
}
