using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatTracker.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        public string GameLocation { get; set; }
        
        [Required]
        public string GameDate { get; set;  }

        public string GameType { get; set; }

        public List<GameDetail> GameDetails;
    }
}
