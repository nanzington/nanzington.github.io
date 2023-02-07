using SadRogue.Primitives; 

namespace SadTutorial {
    public class Map {
        public Tile[] Tiles;
        public int Width;
        public int Height;

        public Map(int w, int h) {
            Width = w;
            Height = h;
            Tiles = new Tile[Width * Height];

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1) {
                        Tiles[new Point(x, y).ToIndex(Width)] = new Tile("Wall", true, true, Color.Gray, Color.Black, '#');
                    } else {
                        Tiles[new Point(x, y).ToIndex(Width)] = new Tile("Floor", false, false, Color.DarkGray, Color.Black, '.');
                    }
                }
            } 
        }


        public Tile? TileAt(Point point) {
            int index = point.ToIndex(Width);
            if (index >= 0 && index <= Tiles.Length) {
                return Tiles[index];
            }
            return null;
        }

        public bool IsTileWalkable(Point point) {
            int index = point.ToIndex(Width);
            if (index >= 0 && index <= Tiles.Length) {
                return !Tiles[index].blockMove;
            }
            return false;
        }

    }
}
