using GoRogue.MapViews;
using SadAdditions; 
using SadConsole.Input;
using SadTutorial.Data;
using System.Security.Cryptography;
using System.Text;

namespace SadTutorial.UI {
    public class UI_GameArea : InstantUI {
        public List<ColoredString> MessageLog = new();

        public Point Targeting = new();

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

            // Sidebar
            Con.Print(1, 0, "Health: " + GameSettings.World.Player.CurrentHP + "/" + GameSettings.World.Player.MaxHP, Color.Crimson);

            Con.DrawLine(new Point(0, 5), new Point(53, 5), 196);
            Con.Print(0, 6, "Inventory");

            for (int i = 0; i < 20; i++) {
                Con.DrawLine(new Point(0, 7 + i), new Point(53, 7 + i), '-', Color.DarkSlateGray);
                if (i < GameSettings.World.Player.Inventory.Count) {
                    Item item = GameSettings.World.Player.Inventory[i];
                    Con.Print(0, 7 + i, new ColoredString("| ") + new ColoredString(item.Name, item.GetAppearance().First().Foreground, Color.Black));

                    SquareCon.Print(0, 7 + i, item.GetAppearance());

                    bool countChanged = false;
                    if (item.UseID != "") {
                        Con.PrintClickable(46, 7 + i, "USE", () => {
                            if (ItemManager.TryUseItem(item, GameSettings.World.Player)) {
                                countChanged = true;
                            }
                        });
                    }

                    Con.PrintClickable(50, 7 + i, "DROP", () => {
                        if (GameSettings.World.CurrentMap.ItemAt(GameSettings.World.Player.X, GameSettings.World.Player.Y) == null) {
                            item.X = GameSettings.World.Player.X;
                            item.Y = GameSettings.World.Player.Y;
                            GameSettings.World.CurrentMap.Items.Add(item);
                            GameSettings.World.Player.Inventory.Remove(item);
                            countChanged = true;
                        } else {
                            AddMessage("There's already an item here!");
                        }
                    });

                    if (countChanged)
                        break;
                }
            }


            Con.DrawLine(new Point(0, 27), new Point(53, 27), 196);


            Con.DrawLine(new Point(0, 35), new Point(53, 35), 196);

            for (int i = 0; i < MessageLog.Count && i < 15; i++) {
                Con.Print(0, 36 + i, MessageLog[i]);
            }

            // End Sidebar

            Con.DrawLine(new Point(54, 0), new Point(54, 48), 179);

            for (int x = 0; x < GameSettings.World.CurrentMap.Width; x++) {
                for (int y = 0; y < GameSettings.World.CurrentMap.Height; y++) {
                    Tile tile = GameSettings.World.CurrentMap.TileAt(x, y);

                    if (tile != null) {
                        if (GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(x, y)))
                            SquareCon.Print(32 + x, y, tile.GetAppearance());
                        else if (GameSettings.World.SeenTiles.Contains(new GoRogue.Coord(x, y)))
                            SquareCon.Print(32 + x, y, tile.GetAppearance().GetDarker().GetDarker()); 
                    }
                }
            }

            foreach (var item in GameSettings.World.CurrentMap.Items) {
                if (GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(item.X, item.Y))) {
                    SquareCon.Print(32 + item.X, item.Y, item.GetAppearance());
                }
            }

            foreach (var mon in GameSettings.World.CurrentMap.Monsters) {
                if (GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(mon.X, mon.Y))) {
                    SquareCon.Print(32 + mon.X, mon.Y, mon.GetAppearance());
                }
            } 

            SquareCon.Print(32 + GameSettings.World.Player.X, GameSettings.World.Player.Y, GameSettings.World.Player.GetAppearance());
            
            if (GameSettings.World.Player.targetingWith != null) {
                if (Shorthands.Time() % 500 < 250) {
                    SquareCon.Print(32 + GameSettings.World.Player.X + Targeting.X, GameSettings.World.Player.Y + Targeting.Y, "X", Color.Crimson);
                }

                Point where = new Point(GameSettings.World.Player.X + Targeting.X, GameSettings.World.Player.Y + Targeting.Y);

                Actor? cursor = GameSettings.World.CurrentMap.ActorAt(where.X, where.Y);

                if (cursor != null && GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(where.X, where.Y))) { 
                    Win.Print(56, 0, "Use " + GameSettings.World.Player.targetingWith.Name + " on " + cursor.Name + "?");
                } else {
                    Win.Print(56, 0, "Use " + GameSettings.World.Player.targetingWith.Name + " on what?");
                }
            } 
        }

        public override void Input() {
            int dx = 0;
            int dy = 0;
            if (Shorthands.KeyPressed(Keys.W)) { dy = -1; }
            if (Shorthands.KeyPressed(Keys.S)) { dy = 1;  }
            if (Shorthands.KeyPressed(Keys.A)) { dx = -1; }
            if (Shorthands.KeyPressed(Keys.D)) { dx = 1; }

            if (dx != 0 || dy != 0) {
                if (GameSettings.World.Player.targetingWith == null) {
                    GameSettings.World.Player.TryMove(dx, dy);

                    DoTurn();
                } else {
                    Targeting += new Point(dx, dy);
                }
            }

            if (Shorthands.KeyPressed(Keys.G)) { GameSettings.World.Player.TryPickup(); }

            if (Shorthands.KeyPressed(Keys.Escape)) {
                GameSettings.World.Player.targetingWith = null;
                Targeting = new Point(0, 0);
            }

            if (Shorthands.KeyPressed(Keys.Enter)) {
                Point where = new Point(GameSettings.World.Player.X + Targeting.X, GameSettings.World.Player.Y + Targeting.Y);
                Actor? cursor = GameSettings.World.CurrentMap.ActorAt(where.X, where.Y);

                if (cursor != null && GameSettings.World.Player.targetingWith != null && GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(where.X, where.Y))) {
                    if (ItemManager.TryUseItem(GameSettings.World.Player.targetingWith, GameSettings.World.Player, cursor)) {
                        Targeting = new Point(0, 0);
                        GameSettings.World.Player.targetingWith = null;
                    }
                }
            }
        }  

        public void DoTurn() {
            Point step;
            int monCount = GameSettings.World.CurrentMap.Monsters.Count;
            foreach (var mon in GameSettings.World.CurrentMap.Monsters) {
                if (GameSettings.World.PlayerFOV.CurrentFOV.Contains(new GoRogue.Coord(mon.X, mon.Y))) {
                    step = Lines.GetLine(new Point(mon.X, mon.Y), new Point(GameSettings.World.Player.X, GameSettings.World.Player.Y)).ToList()[1];
                    mon.TryMove(step.X - mon.X, step.Y - mon.Y);
                }

                if (monCount != GameSettings.World.CurrentMap.Monsters.Count)
                    break;
            }

            GameSettings.World.UpdateFOV();
        }

        public void AddMessage(string str) {
            AddMessage(new ColoredString(str));
        }

        public void AddMessage(ColoredString str) {
            MessageLog.Insert(0, str);
        }
    }
}
