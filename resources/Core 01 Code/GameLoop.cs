using SadConsole;
using SadRogue.Primitives;
using SadTutorial.UI;
using Console = SadConsole.Console;

namespace SadTutorial {
    class GameLoop {
        public const int GameWidth = 160;
        public const int GameHeight = 50;
        public const int MapWidth = 70;
        public const int MapHeight = 43;

        public static SadFont SquareFont;

        public static UIManager UIManager;

        static void Main(string[] args) {
            // Setup the engine and create the main window. 
            Game.Create(GameWidth, GameHeight, "./fonts/ThinExtended.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

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



            UIManager.Init();
        }
    }
}
