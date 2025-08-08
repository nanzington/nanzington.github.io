using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using System; 

namespace SadTutorial.UI {
    public class UIManager : ScreenObject {
        public SadConsole.UI.Colors CustomColors;
        public UI_GameArea GameArea;

        public UIManager() {
            IsVisible = true;
            IsFocused = true;
            UseMouse = true;
            Parent = GameHost.Instance.Screen;
        }

        public override void Update(TimeSpan timeElapsed) {
            if (GameArea.Win.IsVisible) {
                GameArea.Update();
                GameArea.Input();
            }


            Helper.ClearKeys();
            base.Update(timeElapsed);
        }

        public void Init() {
            SetupCustomColors(); 
            GameArea = new(GameLoop.GameWidth, GameLoop.GameHeight);
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
