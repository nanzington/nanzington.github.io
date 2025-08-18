using GoRogue;
using GoRogue.MapViews;

namespace SadTutorial.Data {
    public class World {
        public Map CurrentMap;
        public Player Player;

        public FOV PlayerFOV;
        public LambdaMapView<bool> MapView;
        public List<Coord> SeenTiles = new();

        public World() { 
            CurrentMap = new(60, 48);

            Point spawn = CurrentMap.RandomRoomCenter(); 
            Player = new("Player", spawn.X, spawn.Y);
            Player.SetStats(10, "1d6");

            ResetFOV();
            UpdateFOV();
        }

        public void ResetFOV() {
            MapView = new(CurrentMap.Width, CurrentMap.Height, (pos) => {
                Tile? tile = CurrentMap.TileAt(pos.X, pos.Y);

                if (tile != null) {
                    return !tile.BlocksLOS;
                }
                return true;
            });

            PlayerFOV = new(MapView);
            SeenTiles.Clear();
        }

        public void UpdateFOV() {  
            PlayerFOV.Calculate(Player.X, Player.Y, 10);

            foreach (var point in PlayerFOV.NewlyUnseen) {
                SeenTiles.Add(point);
            }
        }
    }
}
