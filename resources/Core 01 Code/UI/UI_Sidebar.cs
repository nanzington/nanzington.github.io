using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives; 

namespace SadTutorial.UI {
    public class UI_Sidebar : InstantUI {
        public int counter = 0;
        public bool switch1 = false;
        public bool switch2 = false;
        public bool clickSwitch = false;

        public UI_Sidebar(int width, int height) : base(width, height, "Sidebar", "") {
            Win.IsVisible = true;
            Win.Position = new Point(0, 0);
            Win.CanDrag = false;
        }

        public override void Update() {
            Con.Clear();
            Con.Print(0, 0, "Hello World!");

            if (Helper.KeyPressed(Keys.Space))
                switch1 = !switch1;

            if (Helper.HotkeyDown(Keys.Space))
                switch2 = !switch2;

            Con.Print(0, 1, "Switch 1 (Spacebar): " + switch1.ToString()); 
            Con.Print(0, 2, "Switch 2 (Spacebar Hotkey): " + switch2.ToString()); 
            Con.PrintClickable(0, 3, "Click Switch: " + clickSwitch.ToString() + " [" + counter + " clicks]", UI_Clicks, "clickedSwitch");
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
