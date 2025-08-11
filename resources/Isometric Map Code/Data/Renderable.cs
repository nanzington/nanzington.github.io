using SadAdditions;

namespace SadTutorial.Data {
    public class Renderable { 
        public int Glyph;
        public Color Foreground;

        public Dictionary<Point, IsoPixel> Image;

        public Renderable(int g, Color c) { 
            Glyph = g;
            Foreground = c;
            Image = new();
        }

        public ColoredString GetAppearance() {
            return new ColoredString(Glyph.AsString(), Foreground, Color.Black);
        }

        public void SetRow(Point start, ColoredString line) {
            for (int i = 0; i < line.Length; i++) {
                Point where = start + new Point(i, 0);
                if (Image.ContainsKey(where))
                    Image.Remove(where);
                Image.Add(where, new(line[i]));
            }
        }

        public void SetPixel(Point where, ColoredString str) {
            if (Image.ContainsKey(where))
                Image.Remove(where);
            Image.Add(where, new(str[0]));
        }
    }
}
