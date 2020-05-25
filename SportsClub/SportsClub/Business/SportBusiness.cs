using SportsClub.Data;
using SportsClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsClub.Business
{
    public class SportBusiness
    {
        private SportsClubContext sportsClubContext;
        private string Connection;
        private const string defaultConnection = "Server = .\\SQLEXPRESS; Database= SportsClub; Integrated Security=True;";

        public SportBusiness(string connection = defaultConnection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Method "GetAll" returns a list of all sports and the information about them
        /// </summary>
        public List<Sport> GetAll()
        {
            using(sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Sports.ToList();
            }
        }

        /// <summary>
        /// Method "Get" returns the sport to which the given id matches 
        /// </summary>
        /// <param name="id"></param>
        public Sport Get(int? id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                return sportsClubContext.Sports.Find(id);
            }
        }

        /// <summary>
        /// Method "Get" returns the sport to which the given name matches and the information about them
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Sport Get(string name)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                List<Sport> sports = sportsClubContext.Sports.ToList();

                foreach (var item in sports)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Method "Add" adds a new sport to the database
        /// </summary>
        /// <param name="sport"></param>
        public void Add(Sport sport)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                CheckIfSportExists(sport);
                CheckIfSportNameIsCorrect(sport);

                sportsClubContext.Sports.Add(sport);
                sportsClubContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method "Update" finds an existing sport and changes the information about it
        /// </summary>
        /// <param name="sport"></param>
        public void Update(Sport sport)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Sports.Find(sport.Id);

                if (item != null)
                {
                    CheckIfSportExists(sport);
                    CheckIfSportNameIsCorrect(sport);

                    sportsClubContext.Entry(item).CurrentValues.SetValues(sport);
                    sportsClubContext.SaveChanges();
                }                
            }
        }

        /// <summary>
        /// Method "Delete" finds an existing sport to which the given id matches and deletes it
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (sportsClubContext = new SportsClubContext(Connection))
            {
                var item = sportsClubContext.Sports.Find(id);

                if (item != null)
                {
                    foreach (var team in sportsClubContext.Teams)
                    {
                        if (team.SportId == item.Id)
                        {
                            TeamBusiness teamBusiness = new TeamBusiness();
                            teamBusiness.Delete(team.Id);
                        }
                    }

                    try
                    {
                        sportsClubContext.Sports.Remove(item);
                    }
                    catch
                    {
                        sportsClubContext.Sports.Remove(item);
                    }                    
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "CheckIfSportExists" checks if the given name matches any of the existing sports
        /// </summary>
        /// <param name="sport"></param>
        private void CheckIfSportExists(Sport sport)
        {
            foreach (var existentSport in sportsClubContext.Sports)
            {
                if (sport.Name == existentSport.Name)
                {
                    throw new ArgumentException("Sport already exists");
                }
            }
        }

        /// <summary>
        /// Method "CheckIfSportNameIsCorrect" checks if the given sport name is not "" or null
        /// </summary>
        /// <param name="sport"></param>
        public void CheckIfSportNameIsCorrect(Sport sport)
        {
            if (sport.Name == "" || sport.Name == null)
            {
                throw new ArgumentException("Sport name can't be empty");
            }
        }
    }
}
