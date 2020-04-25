using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    public class Team
    {
        /// <summary>
        /// In class "Team" are implemented the properties and the constructor of the team
        /// </summary>
        public Team()
        {
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SportId { get; set; }
        public int? TrainerId { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
