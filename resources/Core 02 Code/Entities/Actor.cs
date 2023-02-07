using SadConsole.Entities;
using SadRogue.Primitives;

namespace SadTutorial.Entities {
    public class Actor : Entity { 
        public Actor(Color fore, Color back, int glyph) : base(fore, back, glyph, 0) {

        }

        public void TryMove(int dx, int dy) {
            if (GameLoop.World.CurrentMap.IsTileWalkable(Position + new Point(dx, dy))) {
                Position += new Point(dx, dy);
            }
        }
    }
}
