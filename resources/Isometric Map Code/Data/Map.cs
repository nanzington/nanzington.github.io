namespace SadTutorial.Data {
    public class Map {
        public string[] Tiles;
        public int Width;
        public int Height;

        public Map(int width, int height) {
            Tiles = new string[width * height];
            Width = width;
            Height = height; 

            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1) {
                        Tiles[x + y * Width] = "stoneWall";
                    } else {
                        Tiles[x + y * Width] = "stoneFloor";
                    }
                }
            }

            for (int i = 0; i < 100; i++) {
                Tiles[GameSettings.rand.Next(Tiles.Length)] = "stoneWall";
            }
        }

        public Tile? TileAt(int x, int y) {
            if (x >= 0 && x < Width && y >= 0 && y < Height) {
                if (GameSettings.World.TileLibrary.ContainsKey(Tiles[x + y * Width]))
                    return GameSettings.World.TileLibrary[Tiles[x + y * Height]];
            }
            return null;
        }
    }
}
