namespace SadTutorial.Data {
    public class Map {
        public Tile[] Tiles;
        public int Width;
        public int Height;

        public List<Actor> Monsters = new();
        public List<Rectangle> Rooms = new();

        public Map(int width, int height) {
            Tiles = new Tile[width * height];
            Width = width;
            Height = height; 

            for (int i = 0; i < Tiles.Length; i++) {
                Tiles[i] = new Tile("Stone Wall", '#', Color.DimGray, true, true);
            }

            for (int i = 0; i < 10; i++) {
                int roomWidth = GameSettings.rand.Next(10) + 3;
                int roomHeight = GameSettings.rand.Next(10) + 3;

                int roomX = GameSettings.rand.Next(Width - roomWidth - 2) + 1;
                int roomY = GameSettings.rand.Next(Height - roomHeight - 2) + 1;

                Rectangle newRoom = new Rectangle(roomX, roomY, roomWidth, roomHeight);
                 
                while (true) {
                    bool intersects = false;

                    foreach (Rectangle room in Rooms) {
                        if (room.Intersects(newRoom)) {
                            intersects = true;
                        }
                    }

                    if (!intersects)
                        break; 

                    roomX = GameSettings.rand.Next(Width - roomWidth - 2) + 1;
                    roomY = GameSettings.rand.Next(Height - roomHeight - 2) + 1;

                    newRoom = new Rectangle(roomX, roomY, roomWidth, roomHeight);
                } 

                Rooms.Add(newRoom);
                ApplyRoom(newRoom);
            }
            
            for (int i = 0; i < Rooms.Count - 1; i++) {
                ApplyLine(Lines.GetLine(Rooms[i].Center, new Point(Rooms[i].Center.X, Rooms[i+1].Center.Y)).ToList());
                ApplyLine(Lines.GetLine(Rooms[i+1].Center, new Point(Rooms[i].Center.X, Rooms[i+1].Center.Y)).ToList());
            }
        }

        public void ApplyLine(List<Point> positions) {
            for (int i = 0; i < positions.Count(); i++) {
                Point pos = positions[i];

                if (pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height) {
                    Tiles[pos.X + pos.Y * Width] = new Tile("Stone Floor", '.', Color.Gray, false, false);
                }
            }
        }

        public void ApplyRoom(Rectangle rect) {
            for (int x = rect.X; x < rect.X + rect.Width; x++) {
                for (int y = rect.Y; y < rect.Y + rect.Height; y++) {
                    if (x >= 0 && x < Width && y >= 0 && y < Height) {
                        Tiles[x + y * Width] = new Tile("Stone Floor", '.', Color.Gray, false, false);
                    }
                }
            }
        }

        public Point RandomRoomCenter() {
            return Rooms[GameSettings.rand.Next(Rooms.Count)].Center; 
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
