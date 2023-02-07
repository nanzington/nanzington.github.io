using SadRogue.Primitives;
using SadTutorial.Entities; 

namespace SadTutorial {
    public class World {
        public Map CurrentMap;
        public Actor Player;

        public World() {
            CurrentMap = new(GameLoop.MapWidth, GameLoop.MapHeight);
            Player = new(Color.HotPink, Color.Black, '@');
            Player.Position = new Point(5, 5);
        }
    }
}
