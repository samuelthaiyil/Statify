using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatTracker.Models
{
    public class GameDetail
    {
        public int GameDetailId { get; set; }

        public Game GameId;

        public Player PlayerId;
    }
}
