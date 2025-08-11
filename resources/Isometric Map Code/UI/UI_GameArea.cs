using SadAdditions; 
using SadConsole.Input;
using SadTutorial.Data;

namespace SadTutorial.UI {
    public class UI_GameArea : InstantUI {  
        public UI_GameArea(int width, int height) : base(width, height, "") {
            Win.IsVisible = true;
            Win.Position = new Point(0, 0);
            Win.CanDrag = false;
        }

        public override void Update() {
            Win.Clear();
            Extensions.DrawBox(Win, 0, 0, Win.Width - 2, Win.Height - 2);

            Con.Clear();
            SquareCon.Clear();
            DoubleSquareCon.Clear(); 

            Con.DrawLine(new Point(33, 0), new Point(33, 48), 179);

            Tile? ground;
            Point tile = new Point(GameSettings.World.Player.X - 3, GameSettings.World.Player.Y - 2);

            for (int y = 8; y > -6; y--) {
                for (int x = 9; x > -8; x--) {
                    tile = new Point(GameSettings.World.Player.X - x, GameSettings.World.Player.Y - y);

                    ground = GameSettings.World.CurrentMap.TileAt(tile.X, tile.Y);

                    if (ground != null) {
                        SquareCon.Print((50 - 7 * x) + 7 * y, (20 - 4 * y) - 3 * x, ground.GetAppearance());


                        SquareCon.Print((48 - 7 * x) + 7 * y, (21 - 4 * y) - 3 * x, ground.GetAppearance().Repeat(5));
                        SquareCon.Print((47 - 7 * x) + 7 * y, (22 - 4 * y) - 3 * x, ground.GetAppearance().Repeat(9));
                        SquareCon.Print((45 - 7 * x) + 7 * y, (23 - 4 * y) - 3 * x, ground.GetAppearance().Repeat(10));
                        SquareCon.Print((46 - 7 * x) + 7 * y, (24 - 4 * y) - 3 * x, ground.GetAppearance().Repeat(7));
                        SquareCon.Print((49 - 7 * x) + 7 * y, (25 - 4 * y) - 3 * x, ground.GetAppearance().Repeat(3));

                        foreach (var kv in ground.Image) {
                            Point thisSpot = kv.Key + new Point((45 - 7 * x) + 7 * y, (23 - 4 * y) - 3 * x);

                            if (thisSpot.X < 92)
                                SquareCon.Print(thisSpot.X, thisSpot.Y, kv.Value.GetAppearance());
                        }
                    } 

                    if (y == 0 && x == 0) {
                        SquareCon.Print(49, 16, "/" + 196.AsString() + "\\", Color.White);
                        SquareCon.Print(48, 17, " " + 179.AsString() + " " + 179.AsString() + "", Color.White);
                        SquareCon.Print(48, 18, " \\_/ ", Color.White);
                        SquareCon.Print(48, 19, " /" + 179.AsString() + "\\ ", Color.White);
                        SquareCon.Print(48, 20, "/ " + 179.AsString() + " \\", Color.White);
                        SquareCon.Print(48, 21, "  " + 179.AsString() + " ", Color.White);
                        SquareCon.Print(49, 22, "/ \\", Color.White);
                        SquareCon.Print(49, 23, "" + 179.AsString() + " " + 179.AsString() + "", Color.White);
                    }
                }
            }

            SquareCon.Fill(new Rectangle(0, 0, 20, 80), Color.White, Color.Black, ' ');

        }

        public override void Input() {
            int dx = 0;
            int dy = 0;
            if (Shorthands.KeyPressed(Keys.W)) { dy = -1; }
            if (Shorthands.KeyPressed(Keys.S)) { dy = 1;  }
            if (Shorthands.KeyPressed(Keys.A)) { dx = -1; }
            if (Shorthands.KeyPressed(Keys.D)) { dx = 1; }

            if (dx != 0 || dy != 0) {
                Tile? dest = GameSettings.World.CurrentMap.TileAt(GameSettings.World.Player.X + dx, GameSettings.World.Player.Y + dy);

                if (dest != null && !dest.BlocksMove) {
                    GameSettings.World.Player.X += dx;
                    GameSettings.World.Player.Y += dy;
                }
            }
        }  
    }
}
