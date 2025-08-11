namespace SadTutorial.Data {
    public class Tile : Renderable {
        public string Name = "";
        public bool BlocksMove = false;
        public bool BlocksLOS = false; 

        public Tile(string n, int g, Color c, bool blockMove, bool blockLOS ) : base(g, c) {
            Name = n;
            BlocksMove = blockMove;
            BlocksLOS = blockLOS; 
        } 
    }
}
