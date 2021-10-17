using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatTracker.Models
{
    public class GameDetail
    {
        public int GameDetailId { get; set; }

        public int PlayerId { get; set; }

        public int GameId { get; set; }
        
        public int Points { get; set; }

        public int Rebounds { get; set; }

        public int Assists { get; set; }

        public Player Player { get; set; }

        public Game Game { get; set; }


    }
}
