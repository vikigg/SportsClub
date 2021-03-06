﻿using SportsClub.Data;
using SportsClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsClub.Business
{
    public class PlayerBusiness
    {
        private SportsClubContext sportsClubContext;
        private string Connection;
        private const string defaultConnection = "Server = .\\SQLEXPRESS; Database= SportsClub; Integrated Security=True;";

        public PlayerBusiness(string connection = defaultConnection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Method "GetAll" returns a list of all players and the information about them
        /// </summary>
        public List<Player> GetAll()
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Players.ToList();
            }
        }

        /// <summary>
        /// Method "Get" returns the player to which the given id matches 
        /// </summary>
        /// <param name="id"></param>
        public Player Get(int? id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Players.Find(id);
            }
        }

        /// <summary>
        /// Method "Get" returns a list of all players to which the given name matches and the information about them
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Player> Get(string name)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                List<Player> players = sportsClubContext.Players.ToList();
                List<Player> playersWithGivenName = new List<Player>();

                foreach (var item in players)
                {
                    if (item.Name == name)
                    {
                        playersWithGivenName.Add(item);
                    }
                }

                return playersWithGivenName;
            }
        }

        /// <summary>
        /// Method "Add" adds a new player to the database
        /// </summary>
        /// <param name="player"></param>
        public void Add(Player player)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                CheckIfTeamExists(player.TeamId);
                CheckIfPlayerNameIsCorrect(player);
                
                sportsClubContext.Players.Add(player);
                sportsClubContext.SaveChanges();                
            }
        }

        /// <summary>
        /// Method "Update" finds an existing player and changes the information about him
        /// </summary>
        /// <param name="player"></param>
        public void Update(Player player)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Players.Find(player.Id);

                if (item != null)
                {
                    CheckIfTeamExists(player.TeamId);
                    CheckIfPlayerNameIsCorrect(player);

                    sportsClubContext.Entry(item).CurrentValues.SetValues(player);
                    sportsClubContext.SaveChanges();                  
                }
            }
        }

        /// <summary>
        /// Method "Delete" finds an existing player to which the given id matches and deletes him
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Players.Find(id);

                if (item != null)
                {
                    try
                    {
                        sportsClubContext.Players.Remove(item);
                    }
                    catch
                    {
                        sportsClubContext.Players.Remove(item);
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

        /// <summary>
        /// Method "CheckIfPlayerNameIsCorrect" checks if the given player name is not "" or null
        /// </summary>
        /// <param name="player"></param>
        public void CheckIfPlayerNameIsCorrect(Player player)
        {
            if (player.Name == "" || player.Name == null)
            {
                throw new ArgumentException("Player name can't be empty");
            }
        }
    }
}
