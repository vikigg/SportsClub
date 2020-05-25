using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Team" are implemented the properties and the constructor of the team
    /// </summary>
    public partial class Team
    {
        public Team(string name = null, int? sportId = null, int? trainerId = null)
        {
            GameFirstTeam = new HashSet<Game>();
            GameSecondTeam = new HashSet<Game>();
            Player = new HashSet<Player>();
            Name = name;
            SportId = sportId;
            TrainerId = trainerId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SportId { get; set; }
        public int? TrainerId { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<Game> GameFirstTeam { get; set; }
        public virtual ICollection<Game> GameSecondTeam { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
