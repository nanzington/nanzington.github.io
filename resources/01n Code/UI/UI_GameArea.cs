using SadAdditions; 
using SadConsole.Input;
using SadTutorial.Data;

namespace SadTutorial.UI {
    public class UI_GameArea : InstantUI {
        int counter = 0;
        bool switch1 = false;
        bool switch2 = false;
        bool clickSwitch = false;
        public bool NetSwitch = false;

        public List<string> MessageLog = new();

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

            Con.PrintClickable(0, 11, "Network Switch: " + NetSwitch.ToString(), () => {
                NetSwitch = !NetSwitch;
                NetMsg msg = new("SetBool", -1);
                msg.MiscBool = NetSwitch;

                GameSettings.SendMessageIfNecessary(msg, -1);
            });

            if (!GameSettings.NetworkManager.IsServerRunning() && !GameSettings.NetworkManager.IsClientConnected()) {
                Con.PrintClickable(50, 0, "Start Server", () => { GameSettings.NetworkManager.StartServer(); });

                Con.PrintClickable(65, 0, "Join Local Server", () => { GameSettings.NetworkManager.JoinServer("localhost", 25565); });
            }

            for (int i = 0; i < 20 && i < MessageLog.Count; i++) {
                Con.Print(50, i, MessageLog[i]);
            }
        }

        public override void Input() {

        } 

        public void ButtonClick() {
            clickSwitch = !clickSwitch;
            counter++;
        }
    }
}
