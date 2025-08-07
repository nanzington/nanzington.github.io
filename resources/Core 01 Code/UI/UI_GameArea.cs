using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives; 

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
            Con.Clear();
            SquareCon.Clear();
            DoubleSquareCon.Clear();

            Con.Print(0, 0, "Hello World!");  
            SquareCon.Print(0, 1, "Hello World!"); 
            DoubleSquareCon.Print(0, 1, "Hello World!");

            if (Helper.KeyPressed(Keys.Space)) {
                switch1 = !switch1;
            }

            if (Helper.HotkeyDown(Keys.Space)) {
                switch2 = !switch2;
            }

            Con.Print(0, 4, "Switch 1 (Spacebar): " + switch1.ToString());
            Con.Print(0, 5, "Switch 2 (Space hotkey): " + switch2.ToString());
            Con.PrintClickable(0, 6, "Click Switch: " + clickSwitch.ToString() + " [" + counter + " clicks]", UI_Clicks, "clickedSwitch");
            Con.PrintClickable(0, 7, "Lambda Click: " + clickSwitch.ToString() + " [" + counter + " clicks]", () => {
                clickSwitch = !clickSwitch;
                counter++;
            });
        }

        public override void Input() {
            
        }

        public override void UI_Clicks(string ID) { 
            if (ID == "clickedSwitch") {
                clickSwitch = !clickSwitch;
                counter++;
            }
        }
    }
}
