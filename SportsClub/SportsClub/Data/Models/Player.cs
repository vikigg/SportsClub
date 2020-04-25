using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Player" are implemented the properties of the player
    /// </summary>
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
