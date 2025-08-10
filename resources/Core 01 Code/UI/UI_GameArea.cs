using SadAdditions; 
using SadConsole.Input; 

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

            Con.Print(0, 0, "Hello World!");
            SquareCon.Print(0, 1, "Hello World!");
            DoubleSquareCon.Print(0, 1, "Hello World!");

            if (Shorthands.KeyPressed(Keys.Space)) {
                switch1 = !switch1;
            }

            if (Miscellania.HotkeyDown(Keys.Space)) {
                switch2 = !switch2;
            }

            Con.Print(0, 5, "Switch 1 (Spacebar): " + switch1.ToString());
            Con.Print(0, 6, "Switch 2 (Space hotkey): " + switch2.ToString()); 
            Con.PrintClickable(0, 8, "Lambda Click: " + clickSwitch.ToString() + " [" + counter + " clicks]", () => {
                clickSwitch = !clickSwitch;
                counter++;
            });

            Con.PrintClickable(0, 9, "Non-lambda Click: " + clickSwitch.ToString() + " [" + counter + " clicks]", ButtonClick);
        }

        public override void Input() {

        } 

        public void ButtonClick() {
            clickSwitch = !clickSwitch;
            counter++;
        }
    }
}
