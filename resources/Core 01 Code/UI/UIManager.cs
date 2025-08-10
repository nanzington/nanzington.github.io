using SadAdditions; 

namespace SadTutorial.UI {
    public class UIManager : ScreenObject { 
        public UI_GameArea GameArea;

        public UIManager() {  }

        public override void Update(TimeSpan timeElapsed) {
            if (GameArea.Win.IsVisible) {
                GameArea.Update();
                GameArea.Input();
            }


            Miscellania.ClearKeys();
            base.Update(timeElapsed);
        }

        public void Init() { 
            GameArea = new(GameSettings.GameWidth, GameSettings.GameHeight);
        } 
    }
}
