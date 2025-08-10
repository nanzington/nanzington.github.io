using SadAdditions;

namespace SadTutorial.Data {
    public class Renderable { 
        public int Glyph;
        public Color Foreground; 

        public Renderable(int g, Color c) { 
            Glyph = g;
            Foreground = c;
        }

        public ColoredString GetAppearance() {
            return new ColoredString(Glyph.AsString(), Foreground, Color.Black);
        }
    }
}
