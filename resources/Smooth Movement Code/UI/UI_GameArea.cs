using SadAdditions; 
using SadConsole.Input;
using SadTutorial.Data;

namespace SadTutorial.UI {
    public class UI_GameArea : InstantUI { 
        int moveDX = 0;
        int moveDY = 0;

        int moveFinalX = 0;
        int moveFinalY = 0;

        public UI_GameArea(int width, int height) : base(width, height, "") {
            Win.IsVisible = true;
            Win.Position = new Point(0, 0);
            Win.CanDrag = false;

            SquareCon.Children.Add(GameSettings.World.Player);
            GameSettings.World.Player.Font = GameSettings.SquareFont;
            GameSettings.World.Player.Print(0, 0, GameSettings.World.Player.GetAppearance());
            GameSettings.World.Player.UsePixelPositioning = true;
        }

        public override void Update() {
            Win.Clear();
            Extensions.DrawBox(Win, 0, 0, Win.Width - 2, Win.Height - 2);

            if (moveFinalX != 0 || moveFinalY != 0) {
                int dx = Math.Sign(moveFinalX) * 2;
                int dy = Math.Sign(moveFinalY) * 2;

                moveDX += dx;
                moveDY += dy;

                if (moveDX == moveFinalX && moveDY == moveFinalY) {
                    GameSettings.World.Player.X += Math.Sign(moveFinalX);
                    GameSettings.World.Player.Y += Math.Sign(moveFinalY);

                    moveFinalX = 0;
                    moveFinalY = 0;
                    moveDX = 0;
                    moveDY = 0;
                }
            }
            Point tinymove = new Point(moveDX, moveDY);

            GameSettings.World.Player.Position = new Point((44 + GameSettings.World.Player.X) * 12, GameSettings.World.Player.Y * 12) + tinymove;


            Con.Clear();
            SquareCon.Clear();
            DoubleSquareCon.Clear(); 

            Con.DrawLine(new Point(74, 0), new Point(74, 48), 179);

            for (int x = 0; x < GameSettings.World.CurrentMap.Width; x++) {
                for (int y = 0; y < GameSettings.World.CurrentMap.Height; y++) {
                    Tile tile = GameSettings.World.CurrentMap.TileAt(x, y);

                    if (tile != null) {
                        SquareCon.Print(44 + x, y, tile.GetAppearance());
                    }
                }
            }

            //SquareCon.Print(44 + GameSettings.World.Player.X, GameSettings.World.Player.Y, GameSettings.World.Player.GetAppearance());
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
                    moveFinalX = dx * 12;
                    moveFinalY = dy * 12;

                    moveDX = 0;
                    moveDY = 0;
                    //GameSettings.World.Player.X += dx;
                    //GameSettings.World.Player.Y += dy;
                }
            }
        } 
    }
}
