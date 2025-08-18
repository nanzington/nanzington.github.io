using SadTutorial.Data;
using SadTutorial.UI;

public static class GameSettings {
    public const int GameWidth = 160;
    public const int GameHeight = 50;

    public static IFont SquareFont;

    public static UIManager UIManager;

    public static Random rand = new Random();
    public static World World = new World();
}
