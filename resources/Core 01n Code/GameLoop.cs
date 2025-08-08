using SadConsole;
using SadRogue.Primitives;
using SadTutorial.Data;
using SadTutorial.UI;
using System;
using Console = SadConsole.Console;

namespace SadTutorial {
    class GameLoop {
        public const int GameWidth = 160;
        public const int GameHeight = 50;
        public const int MapWidth = 70;
        public const int MapHeight = 43;

        public static SadFont SquareFont;

        public static UIManager UIManager;
        public static NetworkManager NetworkManager;

        static void Main(string[] args) {
            // Setup the engine and create the main window. 
            Game.Create(GameWidth, GameHeight, "./fonts/ThinExtended.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            Game.Instance.FrameUpdate += Update;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }
        
        private static void Init() {
            // Load up our square font (CheepicusExtended) and save it for later - our game map will use it, while the rest defaults to our thin font.
            SquareFont = (SadFont)GameHost.Instance.LoadFont("./fonts/CheepicusExtended.font");
            // Set the window title to whatever you like, probably the name of your project.
            Game.Instance.MonoGameInstance.Window.Title = "SadTutorial";

            // Initialize our UIManager
            UIManager = new();

            // Initialize the interfaces within UIManager, plus anything else we need to set up.
            UIManager.Init();

            NetworkManager = new();
        }

        private static void Update(object sender, GameHost e) {
            NetworkManager.Update();
        }

        public static void SendMessageIfNecessary(NetMsg msg, int recipient) {
            if (NetworkManager.IsServerRunning()) {
                msg.SenderID = -1;
                NetworkManager.ServerSend(msg, recipient);
            }

            if (NetworkManager.IsClientConnected()) {
                NetworkManager.ClientSend(msg);
            }
        }
    }
}
