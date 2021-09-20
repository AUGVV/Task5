using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class GameModel
    {
        public int id { get; set; }

        public string GameCreator { get; set; }

        public string GameCompanionId { get; set; }

        public string GameCompanion { get; set; }

        public string Rules { get; set; }

        public string Unique { get; set; }
    }
}
