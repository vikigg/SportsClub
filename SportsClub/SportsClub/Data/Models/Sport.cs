using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Sport" are implemented the properties and consctructor of the sport
    /// </summary>
    public class Sport
    {
        public Sport()
        {
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
