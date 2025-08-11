using SadAdditions;

namespace SadTutorial.Data {
    public class IsoPixel {
        public int Glyph = 0;
        public Color Foreground = Color.White;

        public IsoPixel(int glyph, Color fore) {
            Glyph = glyph;  
            Foreground = fore;
        }

        public IsoPixel(ColoredGlyph col) {
            Glyph = col.Glyph;
            Foreground = col.Foreground;
        }

        public ColoredString GetAppearance() {
            return new ColoredString(Glyph.AsString(), Foreground, Color.Black);
        }
    }
}
