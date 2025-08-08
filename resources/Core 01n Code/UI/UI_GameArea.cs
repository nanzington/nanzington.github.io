using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using SadTutorial.Data;
using System.Collections.Generic;

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
            Con.Clear();
            SquareCon.Clear();
            DoubleSquareCon.Clear();

            Con.Print(0, 0, "Hello World!");  
            SquareCon.Print(0, 1, "Hello World!");  

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

            Con.PrintClickable(0, 8, "Network Switch: " + NetSwitch.ToString(), () => {
                NetSwitch = !NetSwitch;
                NetMsg msg = new("SetBool", -1);
                msg.MiscBool = NetSwitch;

                GameLoop.SendMessageIfNecessary(msg, -1);
            }); 

            if (!GameLoop.NetworkManager.IsServerRunning() && !GameLoop.NetworkManager.IsClientConnected()) {
                Con.PrintClickable(50, 0, "Start Server", () => { GameLoop.NetworkManager.StartServer(); });
                 
                Con.PrintClickable(65, 0, "Join Local Server", () => { GameLoop.NetworkManager.JoinServer("localhost", 25565); });
            }

            for (int i = 0; i < 20 && i < MessageLog.Count; i++) {
                Con.Print(50, i, MessageLog[i]);
            }


            
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
