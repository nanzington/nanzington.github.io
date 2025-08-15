namespace SadTutorial.Data {
    public class Actor : Entity {
        public int CurrentHP = 0;
        public int MaxHP = 0;

        public string DamageDice = "";
        public List<Item> Inventory = new();

        public Actor(string name, int glyph, Color col, int x, int y) : base(name, glyph, col, x, y) { 
        }


        public void TryMove(int dx, int dy) {
            Tile? dest = GameSettings.World.CurrentMap.TileAt(X + dx, Y + dy);
            Actor? blocker = GameSettings.World.CurrentMap.ActorAt(X + dx, Y + dy);

            if (dest != null && !dest.BlocksMove) {
                if (blocker != null) {
                    blocker.TakeDamage(GoRogue.DiceNotation.Dice.Roll(DamageDice), this);
                }
                else {
                    X += dx;
                    Y += dy;
                }
            }
        }

        public void TryPickup() {
            Item? item = GameSettings.World.CurrentMap.ItemAt(X, Y);

            if (item != null && Inventory.Count < 20) {
                Inventory.Add(item);
                GameSettings.World.CurrentMap.Items.Remove(item);
                GameSettings.UIManager.GameArea.AddMessage(Name + " picked up " + item.Name + ".");
            }
            else if (Inventory.Count == 20 && this is Player) {
                GameSettings.UIManager.GameArea.AddMessage(new ColoredString("Your inventory is too full to pick that up.", Color.Crimson, Color.Black));
            }
        }

        public void SetStats(int hp, string dmg) {
            MaxHP = hp;
            CurrentHP = hp;

            DamageDice = dmg;
        }

        public void TakeDamage(int amt, Actor attacker) {
            CurrentHP -= amt;

            GameSettings.UIManager.GameArea.AddMessage(attacker.Name + " hit " + Name + " for " + amt + " damage.");

            if (CurrentHP <= 0) {
                if (this is Player) {

                } else {
                    GameSettings.World.CurrentMap.Monsters.Remove(this);
                    GameSettings.UIManager.GameArea.AddMessage(new ColoredString(Name + " died!", Color.Crimson, Color.Black));
                }
            }
        }
    }
}
