using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatTracker.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string PlayerName { get; set; }

        public int PlayerHeightCm { get; set; }

        public List<GameDetail> GameDetails { get; set; }

        // Store players best game
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
