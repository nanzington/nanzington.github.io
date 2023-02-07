using SadConsole;
using SadRogue.Primitives;
using SadTutorial.UI;
using Console = SadConsole.Console;

namespace SadTutorial {
    class GameLoop {
        public const int GameWidth = 147;
        public const int GameHeight = 50;
        public const int MapWidth = 60;
        public const int MapHeight = 35;

        public static SadFont SquareFont;

        public static UIManager UIManager;

        public static World World;

        static void Main(string[] args) {
            // Setup the engine and create the main window. 
            Game.Create(GameWidth, GameHeight, "./fonts/ThinExtended.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init() { SquareFont = (SadFont)GameHost.Instance.LoadFont("./fonts/CheepicusExtended.font"); 
            Game.Instance.MonoGameInstance.Window.Title = "SadTutorial";

            World = new();
             
            UIManager = new();



            UIManager.Init();
        }
    }
}
