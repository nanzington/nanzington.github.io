namespace SadTutorial.Data {
    public class Map {
        public Tile[] Tiles;
        public int Width;
        public int Height; 

        public Map(int width, int height) {
            Tiles = new Tile[width * height];
            Width = width;
            Height = height;

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1) {
                        Tiles[x + y * Width] = new Tile("Stone Wall", '#', Color.DimGray, true, true); 
                    } else {
                        Tiles[x + y * Width] = new Tile("Stone Floor", '.', Color.DarkGray, false, false);
                    }
                }
            }

            for (int i = 0; i < 100; i++) {
                Tiles[GameSettings.rand.Next(Tiles.Length)] = new Tile("Stone Wall", '#', Color.DimGray, true, true);
            }
        }

        public Tile? TileAt(int x, int y) {
            if (x >= 0 && x < Width && y >= 0 && y < Height) {
                return Tiles[x + y * Width];
            }
            return null;
        }
    }
}
