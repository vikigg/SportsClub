using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Player" are implemented the properties of the player
    /// </summary>
    public partial class Player
    {
        public Player(string name = null, int? age = null, int? teamId = null)
        {
            Name = name;
            Age = age;
            TeamId = teamId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
