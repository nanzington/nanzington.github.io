using SadConsole;
using SadConsole.Entities;
using SadRogue.Primitives; 

namespace SadTutorial.UI {
    public class UI_Map : InstantUI {
        public Renderer entityRenderer;


        public UI_Map(int width, int height) : base(width, height, "Map", "") {
            Con = new SadConsole.Console(new CellSurface(GameLoop.MapWidth, GameLoop.MapHeight, GameLoop.World.CurrentMap.Tiles), GameLoop.SquareFont);

            Win = new(new CellSurface(width, height), GameLoop.SquareFont);

            Con.UsePixelPositioning = true;
            Con.Position = new Point(12, 12);

            Win.IsVisible = true;
            Win.UsePixelPositioning = true;
            Win.Position = new Point(277, 0);
            Win.CanDrag = false;
            Win.Title = "";

            entityRenderer = new();
            Con.SadComponents.Add(entityRenderer);
            entityRenderer.Add(GameLoop.World.Player);


            Win.Children.Add(Con);
            GameLoop.UIManager.Children.Add(Win);
        }

        public override void Update() {
            
        }
    }
}
