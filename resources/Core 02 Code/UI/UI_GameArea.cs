using SadAdditions; 
using SadConsole.Input;
using SadTutorial.Data;

namespace SadTutorial.UI {
    public class UI_GameArea : InstantUI {
        int counter = 0;
        bool switch1 = false;
        bool switch2 = false;
        bool clickSwitch = false; 

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

            Con.DrawLine(new Point(74, 0), new Point(74, 48), 179);

            for (int x = 0; x < GameSettings.World.CurrentMap.Width; x++) {
                for (int y = 0; y < GameSettings.World.CurrentMap.Height; y++) {
                    Tile tile = GameSettings.World.CurrentMap.TileAt(x, y);

                    if (tile != null) {
                        SquareCon.Print(44 + x, y, tile.GetAppearance());
                    }
                }
            }

            SquareCon.Print(44 + GameSettings.World.Player.X, GameSettings.World.Player.Y, GameSettings.World.Player.GetAppearance());
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

        public void ButtonClick() {
            clickSwitch = !clickSwitch;
            counter++;
        }
    }
}
