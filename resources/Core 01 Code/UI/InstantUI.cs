using SadConsole;
using SadConsole.Input;
using SadConsole.UI;
using SadRogue.Primitives; 

namespace SadTutorial.UI {
    public abstract class InstantUI {
        public Console Con;
        public Console SquareCon;
        public Console DoubleSquareCon;
        public Window Win; 

        public InstantUI(int width, int height, string windowTitle = "") {
            Win = new(width, height);
            Win.CanDrag = true;
            Win.Position = new((GameLoop.GameWidth - width)/2, (GameLoop.GameHeight - height)/2);

            Con = new(width - 2, height - 2);
            Con.Position = new(1, 1);
            Win.Title = windowTitle.Align(HorizontalAlignment.Center, width - 2, (char)196);

            SquareCon = new Console(new CellSurface(width, height), GameLoop.SquareFont);
            SquareCon.UsePixelPositioning = true;
            SquareCon.Position = new(7, 12);

            DoubleSquareCon = new Console(new CellSurface(width / 2, height / 2), GameLoop.SquareFont, new Point(24, 24));
            DoubleSquareCon.UsePixelPositioning = true;
            DoubleSquareCon.Position = new Point(7, 12);

            Win.Children.Add(Con);
            Win.Children.Add(SquareCon);
            Win.Children.Add(DoubleSquareCon);
            GameLoop.UIManager.Children.Add(Win);

            Win.Show();
            Win.IsVisible = false; 
        }


        public virtual void Update() { 
            Con.Clear();
        }

        public virtual void Input() {
            Point mousePos = new MouseScreenObjectState(Con, GameHost.Instance.Mouse).CellPosition;
        }

        public virtual void UI_Clicks(string ID) {

        }
    }
}
