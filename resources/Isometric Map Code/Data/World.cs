namespace SadTutorial.Data {
    public class World {
        public Map CurrentMap;
        public Player Player;
        public Dictionary<string, Tile> TileLibrary = new();

        public World() {
            CurrentMap = new(48, 48);
            Player = new("Player", 6, 6);

            TileLibrary.Add("stoneFloor", new Tile("Stone Floor", '.', Color.DarkGray, false, false));

            Tile stoneWall = new Tile("Stone Wall", '#', Color.Gray, true, true);
            stoneWall.SetRow(new Point(5, -10), new ColoredString("#", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(3, -9), new ColoredString("## ##", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(2, -8), new ColoredString("#     ###", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -7), new ColoredString("###     # #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -6), new ColoredString("#  ## ##  #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -5), new ColoredString("#    #    #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -4), new ColoredString("#    #    #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -3), new ColoredString("#    #    #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -2), new ColoredString("#    #    #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, -1), new ColoredString("#    #    #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(0, 0), new ColoredString("##   #   #", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(1, 1), new ColoredString("### # ##", Color.Gray, Color.Black));
            stoneWall.SetRow(new Point(4, 2), new ColoredString("###", Color.Gray, Color.Black));
             
            TileLibrary.Add("stoneWall", stoneWall);
        }
    }
}
