using SportsClub.Data;
using SportsClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsClub.Business
{
    public class GameBusiness
    {
        private SportsClubContext sportsClubContext;
        private string Connection;
        private const string defaultConnection = "Server = .\\SQLEXPRESS; Database= SportsClub; Integrated Security=True;";

        public GameBusiness(string connection = defaultConnection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Method "GetAll" returns a list of all games and the information about them
        /// </summary>
        public List<Game> GetAll()
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Games.ToList();
            }
        }

        /// <summary>
        /// Method "Get" returns the game to which the given id matches 
        /// </summary>
        /// <param name="id"></param>
        public Game Get(int? id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Games.Find(id);
            }
        }

        /// <summary>
        /// Method "Add" adds a new game to the database
        /// </summary>
        /// <param name="game"></param>
        public void Add(Game game)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                CheckIfTeamExists(game.FirstTeamId);
                CheckIfTeamExists(game.SecondTeamId);

                sportsClubContext.Games.Add(game);
                sportsClubContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method "Update" finds an existing game and changes the information about it
        /// </summary>
        /// <param name="game"></param>
        public void Update(Game game)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Games.Find(game.Id);

                if (item != null)
                {
                    CheckIfTeamExists(game.FirstTeamId);
                    CheckIfTeamExists(game.SecondTeamId);

                    sportsClubContext.Entry(item).CurrentValues.SetValues(game);
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "Delete" finds an existing game to which the given id matches and deletes it
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Games.Find(id);

                if (item != null)
                {
                    try
                    {
                        sportsClubContext.Games.Remove(item);
                    }
                    catch
                    {
                        sportsClubContext.Games.Remove(item);
                    }
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "CheckIfTeamExists" checks if the given id matches any of the existing teams
        /// </summary>
        /// <param name="teamId"></param>
        private void CheckIfTeamExists(int? teamId)
        {
            bool teamIdExists = false;

            foreach (var team in sportsClubContext.Teams)
            {
                if (team.Id == teamId)
                {
                    teamIdExists = true;
                }
            }
            if (!teamIdExists)
            {
                throw new ArgumentException("No team with such id");
            }
        }
    }
}
