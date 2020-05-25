
using SportsClub.Data;
using SportsClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsClub.Business
{
    public class TeamBusiness
    {
        private SportsClubContext sportsClubContext;
        private string Connection;
        private const string defaultConnection = "Server = .\\SQLEXPRESS; Database= SportsClub; Integrated Security=True;";

        public TeamBusiness(string connection = defaultConnection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Method "GetAll" returns a list of all teams and the information about them
        /// </summary>
        public List<Team> GetAll()
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Teams.ToList();
            }
        }

        /// <summary>
        /// Method "Get" returns the team to which the given id matches 
        /// </summary>
        /// <param name="id"></param>
        public Team Get(int? id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Teams.Find(id);
            }
        }

        /// <summary>
        /// Method "Get" returns a list of all teams to which the given name matches and the information about them
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Team> Get(string name)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                List<Team> teams = sportsClubContext.Teams.ToList();
                List<Team> teamsWithGivenName = new List<Team>();

                foreach (var item in teams)
                {
                    if (item.Name == name)
                    {
                        teamsWithGivenName.Add(item);
                    }
                }

                return teamsWithGivenName;
            }
        }

        /// <summary>
        /// Method "Add" adds a new team to the database
        /// </summary>
        /// <param name="team"></param>
        public void Add(Team team)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                CheckIfSportExists(team.SportId);
                CheckIfTrainerExists(team.TrainerId);
                CheckIfTeamNameIsCorrect(team);
                CheckIfTeamExists(team);
                                                                       
                sportsClubContext.Teams.Add(team);
                sportsClubContext.SaveChanges();                                                        
            }
        }

        /// <summary>
        /// Method "Update" finds an existing team and changes the information about it
        /// </summary>
        /// <param name="team"></param>
        public void Update(Team team)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Teams.Find(team.Id);

                if (item != null)
                {
                    CheckIfSportExists(team.SportId);
                    CheckIfTrainerExists(team.TrainerId);
                    CheckIfTeamNameIsCorrect(team);
                    CheckIfTeamExists(team);

                    sportsClubContext.Entry(item).CurrentValues.SetValues(team);
                    sportsClubContext.SaveChanges();                                   
                }
            }
        }

        /// <summary>
        /// Method "Delete" finds an existing team to which the given id matches and deletes it
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Teams.Find(id);

                if (item != null)
                {
                    foreach (var player in sportsClubContext.Players)
                    {
                        if (player.TeamId == item.Id)
                        {
                            sportsClubContext.Players.Remove(player);
                        }
                    }

                    foreach (var game in sportsClubContext.Games)
                    {
                        if (game.FirstTeamId == item.Id || game.SecondTeamId == item.Id)
                        {
                            sportsClubContext.Games.Remove(game);
                        }
                    }

                    try
                    {
                        sportsClubContext.Teams.Remove(item);
                    }
                    catch
                    {
                        sportsClubContext.Teams.Remove(item);
                    }                    
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "CheckIfTeamExists" checks if the given team matches any of the existing teams
        /// </summary>
        /// <param name="team"></param>
        private void CheckIfTeamExists(Team team)
        {
            bool teamExists = false;

            foreach (var existingTeam in sportsClubContext.Teams)
            {
                if (existingTeam.Name == team.Name &&
                    existingTeam.SportId == team.SportId &&
                    existingTeam.TrainerId == team.TrainerId)
                {
                    teamExists = true;
                }
            }

            if (teamExists)
            {
                throw new ArgumentException("Team already exists");
            }
        }

        /// <summary>
        /// Method "CheckIfSportExists" checks if the given id matches any of the existing sports
        /// </summary>
        /// <param name="sportId"></param>
        private void CheckIfSportExists(int? sportId)
        {
            bool sportIdExists = false;

            foreach (var sport in sportsClubContext.Sports)
            {
                if (sport.Id == sportId)
                {
                    sportIdExists = true;
                }
            }

            if (!sportIdExists)
            {
                throw new ArgumentException("No sport with such id");
            }
        }

        /// <summary>
        /// Method "CheckIfTrainerExists" checks if the given id matches any of the existing trainers
        /// </summary>
        /// <param name="trainerId"></param>
        private void CheckIfTrainerExists(int? trainerId)
        {
            bool trainerIdExists = false;

            foreach (var trainer in sportsClubContext.Trainers)
            {
                if (trainer.Id == trainerId)
                {
                    trainerIdExists = true;
                }
            }

            if (!trainerIdExists)
            {
                throw new ArgumentException("No trainer with such id");
            }
        }

        /// <summary>
        /// Method "CheckIfTeamNameIsCorrect" checks if the given team name is not "" or null
        /// </summary>
        /// <param name="team"></param>
        public void CheckIfTeamNameIsCorrect(Team team)
        {
            if (team.Name == "" || team.Name == null)
            {
                throw new ArgumentException("Team name can't be empty");
            }
        }
    }
}
