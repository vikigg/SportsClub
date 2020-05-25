using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Sport" are implemented the properties and the consctructor of the sport
    /// </summary>
    public partial class Sport
    {
        public Sport(string name = null)
        {
            Team = new HashSet<Team>();
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
