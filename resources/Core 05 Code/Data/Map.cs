namespace SadTutorial.Data {
    public class Map {
        public Tile[] Tiles;
        public int Width;
        public int Height;

        public List<Actor> Monsters = new();

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

        public Actor? ActorAt(int x, int y) {
            foreach (var mon in Monsters) {
                if (mon.X == x && mon.Y == y) return mon;
            }
             
            if (GameSettings.World.Player.X == x && GameSettings.World.Player.Y == y) return GameSettings.World.Player; 

            return null;
        }

        public void PlaceMonsters() {
            for (int i = 0; i < 30; i++) {
                int x = GameSettings.rand.Next(Width);
                int y = GameSettings.rand.Next(Height);

                while (TileAt(x, y) == null || TileAt(x, y).BlocksMove || ActorAt(x, y) != null) {
                    x = GameSettings.rand.Next(Width);
                    y = GameSettings.rand.Next(Height);
                }

                Actor goblin = new("Goblin", 'g', Color.LimeGreen, x, y);
                goblin.SetStats(5, "1d2-1");

                Monsters.Add(goblin);
            }
        }
    }
}
