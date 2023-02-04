using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives; 

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


            Helper.ClearKeys();
            base.Update(timeElapsed);
        }

        public void Init() {
            SetupCustomColors();

            UI_Sidebar Sidebar = new(40, 50);
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
