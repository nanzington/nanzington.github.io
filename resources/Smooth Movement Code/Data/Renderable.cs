using SadAdditions;

namespace SadTutorial.Data {
    public class Renderable : ScreenSurface { 
        public int Glyph;
        public Color Foreground; 

        public Renderable(int g, Color c) : base(1, 1) { 
            Glyph = g;
            Foreground = c;
        }

        public ColoredString GetAppearance() {
            return new ColoredString(Glyph.AsString(), Foreground, Color.Black);
        }
    }
}
