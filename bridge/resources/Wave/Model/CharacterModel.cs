namespace Wave.Model
{
    public class CharacterModel
    {
        public string   accountName { get; set; }
        public string   characterName { get; set; }
        public int      id         { get; set; }

        public float    posX       { get; set; }
        public float    posY       { get; set; }
        public float    posZ       { get; set; }
        public float    rotation   { get; set; }

        public int      money      { get; set; }
        public int      bank       { get; set; }

        public int      health     { get; set; }
        public int      armor      { get; set; }

        public int      adminRank  { get; set; }
        public int      age        { get; set; }
        public int      sex        { get; set; }

        public int      played     { get; set; }
        public int      xp         { get; set; }
        public int      lvl        { get; set; }

        public float hunger { get; set; }
        public float thirst { get; set; }
    }
}