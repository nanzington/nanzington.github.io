using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives; 
using Key = SadConsole.Input.Keys;

namespace SadTutorial.UI {
    public class UIManager : ScreenObject {
        public SadConsole.UI.Colors CustomColors;
        public Dictionary<string, InstantUI> Interfaces = new();

        public UIManager() {
            IsVisible = true;
            IsFocused = true;
            UseMouse = true;
            Parent = GameHost.Instance.Screen;
        }

        public override void Update(TimeSpan timeElapsed) {
            foreach(KeyValuePair<string, InstantUI> kv in Interfaces) {
                if (kv.Value.Win.IsVisible) {
                    kv.Value.Update();
                    kv.Value.Input();
                }
            }

            if (Helper.KeyPressed(Key.W)) { GameLoop.World.Player.TryMove(0, -1); }
            if (Helper.KeyPressed(Key.S)) { GameLoop.World.Player.TryMove(0, 1); }
            if (Helper.KeyPressed(Key.A)) { GameLoop.World.Player.TryMove(-1, 0); }
            if (Helper.KeyPressed(Key.D)) { GameLoop.World.Player.TryMove(1, 0); }


            Helper.ClearKeys();
            base.Update(timeElapsed);
        }

        public void Init() {
            SetupCustomColors();

            UI_Sidebar Sidebar = new(40, 50);
            UI_Map Map = new(GameLoop.MapWidth + 2, GameLoop.MapHeight + 2);
        }


        private void SetupCustomColors() {
            CustomColors = SadConsole.UI.Colors.CreateAnsi();
            CustomColors.ControlHostBackground = new AdjustableColor(Color.Black, "Black");
            CustomColors.Lines = new AdjustableColor(Color.White, "White");
            CustomColors.Title = new AdjustableColor(Color.White, "White");

            CustomColors.RebuildAppearances();
            SadConsole.UI.Themes.Library.Default.Colors = CustomColors;
        }
    }
}
