using SadConsole.Input;
using SadConsole.UI; 

namespace SadTutorial.UI {
    public abstract class InstantUI {
        public ScreenSurface Con;
        public ScreenSurface SquareCon;
        public ScreenSurface DoubleSquareCon;
        public Window Win;

        public InstantUI(int width, int height, string windowTitle = "") {
            Win = new(width, height);
            Win.CanDrag = true;
            Win.Position = new((GameSettings.GameWidth - width) / 2, (GameSettings.GameHeight - height) / 2);

            Con = new(width - 2, height - 2);
            Con.Position = new(1, 1);
            Win.Title = windowTitle.Align(HorizontalAlignment.Center, width - 2, (char)196);

            SquareCon = new(new CellSurface(width, height), GameSettings.SquareFont);
            SquareCon.UsePixelPositioning = true;
            SquareCon.Position = new(7, 12);

            DoubleSquareCon = new(new CellSurface(width / 2, height / 2), GameSettings.SquareFont, new Point(24, 24));
            DoubleSquareCon.UsePixelPositioning = true;
            DoubleSquareCon.Position = new Point(7, 12);

            Win.Children.Add(Con);
            Win.Children.Add(SquareCon);
            Win.Children.Add(DoubleSquareCon);
            GameSettings.UIManager.Children.Add(Win);

            Win.Show();
            Win.IsVisible = false;
        }


        public virtual void Update() {
            Con.Clear();
        }

        public virtual void Input() {
            Point mousePos = new MouseScreenObjectState(Con, GameHost.Instance.Mouse).CellPosition;
        } 
    }
}
