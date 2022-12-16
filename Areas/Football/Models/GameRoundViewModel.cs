namespace Sportiada.Web.Areas.Football.Models
{
    using Services.Football.Models.Game;
    using Services.Football.Models.Round;
    using Services.Football.Models.Team;
    using System.Collections.Generic;
    using System.Linq;
    public class GameRoundViewModel
    {
        public IEnumerable<GameModel> games;
        public IEnumerable<GameModel> gamesForRanking;

        public IEnumerable<RoundBaseModel> rounds;
        public IEnumerable<TeamTableModel> teamsTable => getTeamsTable();

        private IEnumerable<TeamTableModel> getTeamsTable()
        {
            List<TeamTableModel> teamsTable = new List<TeamTableModel>();

            foreach (var game in gamesForRanking)
            {
                string hostSideName = game.Statistics.Where(s => s.Type.id == 1).First().Squad.Name;
                string hostTeamName = getTeamName(hostSideName);
                string guestSideName = game.Statistics.Where(s => s.Type.id == 2).First().Squad.Name;
                string guestTeamName = getTeamName(guestSideName);
                bool isHostWin = game.HostGoals > game.GuestGoals;
                bool isGuestWin = game.HostGoals < game.GuestGoals;
                bool isDraw = game.HostGoals == game.GuestGoals;

                AddTeam(hostTeamName, teamsTable, isHostWin, isGuestWin, isDraw, game, true);
                AddTeam(guestTeamName, teamsTable, isHostWin, isGuestWin, isDraw, game, false);
            }

            var teams = teamsTable
                .OrderByDescending(t => t.OverallPoints)
                .ThenByDescending(t => t.OveralGoalDiff)
                .ThenByDescending(t => t.OverallScoredGoals)
                .ToList();

            SetTeamsRank(teams);
            return teams;
        }

        private void fillHostStatistic(TeamTableModel team, GameModel game, bool isHostWin, bool isGuestWin, bool isDraw)
        {
            if (isHostWin)
            {
                team.HostPoints += 3;
                team.OverallPoints += 3;
                team.HostWins ++;
                team.OverallWins++;

            }
            else if (isGuestWin)
            {
                team.HostLoses ++;
                team.OverallLoses ++;
            }
            else if (isDraw)
            {
                team.HostPoints ++;
                team.OverallPoints ++;
                team.OverallDraws++;
                team.HostDraws ++;
            }

            team.HostScoredGoals += game.HostGoals;
            team.HostAllowedGoals += game.GuestGoals;
            team.OverallScoredGoals += game.HostGoals;
            team.OverallAllowedGoals += game.GuestGoals;
            team.HostMathes ++;
            team.OverallMathes++;
        }

        private void fillGuestStatistic(TeamTableModel team, GameModel game, bool isHostWin, bool isGuestWin, bool isDraw)
        {
            if(isHostWin)
            {
                team.GuestLoses ++;
                team.OverallLoses ++;
            }
            else if (isGuestWin)
            {
                team.GuestPoints += 3;
                team.OverallPoints += 3;
                team.GuestWins ++;
                team.OverallWins ++;
            }
            else if (isDraw)
            {
                team.GuestPoints ++;
                team.OverallPoints ++;
                team.OverallDraws ++;
                team.GuestDraws ++;
            }

            team.GuestScoredGoals += game.GuestGoals;
            team.GuestAllowedGoals += game.HostGoals;
            team.OverallScoredGoals += game.GuestGoals;
            team.OverallAllowedGoals += game.HostGoals;
            team.GuestMathes++;
            team.OverallMathes++;
        }

        private string getTeamName(string sideName)
        {
            int index = sideName.IndexOf(' ');
            return sideName.Substring(0, index);
        }

        private void AddTeam(string teamName, List<TeamTableModel> teamsTable, bool isHostWin, bool isGuestWin, bool isDraw, GameModel game, bool isTeamHost)
        {
            if (teamsTable.Where(t => t.TeamName == teamName).Any())
            {
                TeamTableModel team = teamsTable.Find(t => t.TeamName == teamName);
                if (isTeamHost)
                {
                    fillHostStatistic(team, game, isHostWin, isGuestWin, isDraw);
                }
                else
                {
                    fillGuestStatistic(team, game, isHostWin, isGuestWin, isDraw);
                }
                
            }
            else
            {
                TeamTableModel team = new TeamTableModel() { TeamName = teamName };
                if (isTeamHost)
                {
                    fillHostStatistic(team, game, isHostWin, isGuestWin, isDraw);
                }
                else
                {
                    fillGuestStatistic(team, game, isHostWin, isGuestWin, isDraw);
                }
                teamsTable.Add(team);
            }
        }

        private void SetTeamsRank(List<TeamTableModel> teams)
        {
            int teamsCount = teams.Count();
            for (int i = 0; i < teamsCount; i++)
            {
                if (i == 0)
                {
                    teams[i].Rank = i + 1;
                }
                else
                {
                    if (teams[i].OverallPoints == teams[i - 1].OverallPoints
                        && teams[i].OveralGoalDiff == teams[i - 1].OveralGoalDiff
                        && teams[i].OverallScoredGoals == teams[i - 1].OverallScoredGoals)
                    {
                        teams[i].Rank = teams[i - 1].Rank;
                    }
                    else
                    {
                        teams[i].Rank = i + 1;
                    }
                }
            }
        }
    }
}
