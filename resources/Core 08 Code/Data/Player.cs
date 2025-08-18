namespace SadTutorial.Data {
    public class Player : Actor {
        public Item? targetingWith;

        public Player(string name, int x, int y) : base(name, '@', Color.White, x, y) {
        } 
    }
}
