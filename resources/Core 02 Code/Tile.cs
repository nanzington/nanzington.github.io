using SadConsole;
using SadRogue.Primitives; 

namespace SadTutorial {
    public class Tile : ColoredGlyph {
        public string Name;
        public bool blockMove = false;
        public bool blockVision = false;

        public Tile(string name, bool move, bool vis, Color fore, Color back, int glyph) : base(fore, back, glyph) {
            Name = name;
            blockMove = move;
            blockVision = vis;
        }
    }
}
