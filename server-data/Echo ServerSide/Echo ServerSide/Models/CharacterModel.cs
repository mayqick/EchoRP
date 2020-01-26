using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_ServerSide.Models
{
    public class CharacterModel
    {
        public int id { get; set; }
        public string characterName { get; set; }
        public bool isMale { get; set; }

        public float posX { get; set; }
        public float posY { get; set; }
        public float posZ { get; set; }
        public float rotation { get; set; }

        public int money { get; set; }
        public int bank { get; set; }

        public int health { get; set; }
        public int armor { get; set; }

        public int adminRank { get; set; }
        public int age { get; set; }

        public int played { get; set; }
        public int xp { get; set; }
        public int lvl { get; set; }

        public float hunger { get; set; }
        public float thirst { get; set; }

    }
}
