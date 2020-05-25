using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Trainer" are implemented the properties and the constructor of the trainer
    /// </summary>
    public partial class Trainer
    {
        public Trainer(string name = null)
        {
            Team = new HashSet<Team>();
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
