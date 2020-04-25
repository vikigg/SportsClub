using SportsClub.Data;
using SportsClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsClub.Business
{
    public class TrainerBusiness
    {
        private SportsClubContext sportsClubContext;

        /// <summary>
        /// Method "GetAll" returns a list of all trainers and the information about them
        /// </summary>
        public List<Trainer> GetAll()
        {
            using (sportsClubContext = new SportsClubContext())
            {
                return sportsClubContext.Trainers.ToList();
            }
        }

        /// <summary>
        /// Method "Get" returns the trainer to which the given id matches 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Trainer Get(int? id)
        {
            using (sportsClubContext = new SportsClubContext())
            {
                return sportsClubContext.Trainers.Find(id);
            }
        }

        /// <summary>
        /// Method "Add" adds a new trainer to the database
        /// </summary>
        /// <param name="trainer"></param>
        public void Add(Trainer trainer)
        {
            using (sportsClubContext = new SportsClubContext())
            {
                CheckIfTrainerNameIsCorrect(trainer);

                sportsClubContext.Trainers.Add(trainer);
                sportsClubContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method "Update" finds an existing trainer and changes the information about him
        /// </summary>
        /// <param name="trainer"></param>
        public void Update(Trainer trainer)
        {
            using (sportsClubContext = new SportsClubContext())
            {
                var item = sportsClubContext.Trainers.Find(trainer.Id);

                if (item != null)
                {
                    CheckIfTrainerNameIsCorrect(trainer);

                    sportsClubContext.Entry(item).CurrentValues.SetValues(trainer);
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "Delete" finds an existing trainer to which the given id matches and deletes him
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (sportsClubContext = new SportsClubContext())
            {
                var item = sportsClubContext.Trainers.Find(id);

                if (item != null)
                {
                    foreach (var team in sportsClubContext.Teams)
                    {
                        if(team.TrainerId==item.Id)
                        {
                            TeamBusiness teamBusiness = new TeamBusiness();
                            teamBusiness.Delete(team.Id);
                        }
                    }

                    try
                    {
                        sportsClubContext.Trainers.Remove(item);
                    }
                    catch
                    {
                        sportsClubContext.Trainers.Remove(item);
                    }                    
                    sportsClubContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method "CheckIfTrainerNameIsCorrect" checks if the given trainer name is not "" or null
        /// </summary>
        /// <param name="trainer"></param>
        public void CheckIfTrainerNameIsCorrect(Trainer trainer)
        {
            if (trainer.Name == "" || trainer.Name == null)
            {
                throw new ArgumentException("Trainer name can't be empty");
            }
        }
    }
}
