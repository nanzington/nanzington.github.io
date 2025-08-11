namespace SadTutorial.Data {
    public class Entity : Renderable{
        public string Name = "";  
        public int X, Y;

        public Entity(string n, int g, Color c, int x, int y) : base(g, c) {
            Name = n; 
            X = x;
            Y = y;
        } 
    }
}
