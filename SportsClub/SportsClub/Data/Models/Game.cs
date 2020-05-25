using System;
using System.Collections.Generic;

namespace SportsClub.Data.Models
{
    /// <summary>
    /// In class "Game" are implemented the properties of the game
    /// </summary>
    public partial class Game
    {
        public Game(int? firstTeamId = null, int? secondTeamId = null)
        {
            FirstTeamId = firstTeamId;
            SecondTeamId = secondTeamId;
        }

        public int Id { get; set; }
        public int? FirstTeamId { get; set; }
        public int? SecondTeamId { get; set; }

        public virtual Team FirstTeam { get; set; }
        public virtual Team SecondTeam { get; set; }
    }
}
