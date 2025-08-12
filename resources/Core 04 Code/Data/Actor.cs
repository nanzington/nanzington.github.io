namespace SadTutorial.Data {
    public class Actor : Entity {
        public int CurrentHP = 0;
        public int MaxHP = 0;

        public string DamageDice = "";

        public Actor(string name, int glyph, Color col, int x, int y) : base(name, glyph, col, x, y) { 
        }


        public void TryMove(int dx, int dy) {
            Tile? dest = GameSettings.World.CurrentMap.TileAt(X + dx, Y + dy);
            Actor? blocker = GameSettings.World.CurrentMap.ActorAt(X + dx, Y + dy);

            if (dest != null && !dest.BlocksMove) {
                if (blocker != null) {
                    blocker.TakeDamage(GoRogue.DiceNotation.Dice.Roll(DamageDice));
                }
                else {
                    X += dx;
                    Y += dy;
                }
            }
        }

        public void SetStats(int hp, string dmg) {
            MaxHP = hp;
            CurrentHP = hp;

            DamageDice = dmg;
        }

        public void TakeDamage(int amt) {
            CurrentHP -= amt;
            if (CurrentHP <= 0) {
                if (this is Player) {

                } else {
                    GameSettings.World.CurrentMap.Monsters.Remove(this);
                }
            }
        }
    }
}
