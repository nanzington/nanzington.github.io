using SadConsole.Configuration;
using SadTutorial.UI;
using SadTutorial;

Settings.WindowTitle = "SadTutorial";

Builder startup = new Builder()
    .SetScreenSize(GameSettings.GameWidth, GameSettings.GameHeight)
    .SetStartingScreen((host) => {
        GameSettings.SquareFont = Game.Instance.Fonts["CheepicusExtended"];
        GameSettings.UIManager = new UIManager();
        GameSettings.NetworkManager = new NetworkManager();

        GameSettings.UIManager.Init();

        return GameSettings.UIManager;
    })
    .IsStartingScreenFocused(true)
    .ConfigureFonts("./fonts/ThinExtended.font", new[] { "./fonts/CheepicusExtended.font" })
    .AddFrameUpdateEvent(GameHost_FrameUpdate)
    ;

Game.Create(startup);
Game.Instance.Run();
Game.Instance.Dispose();

static void GameHost_FrameUpdate(object sender, GameHost e) {
    GameSettings.NetworkManager.Update();
}