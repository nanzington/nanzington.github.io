namespace SadTutorial.Data {
    public class World {
        public Map CurrentMap;
        public Player Player;

        public World() {
            CurrentMap = new(48, 48);
            Player = new("Player", 5, 5);
        }
    }
}
