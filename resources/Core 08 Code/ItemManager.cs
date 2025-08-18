using SadTutorial.Data;

namespace SadTutorial {
    public static class ItemManager {
        public static bool TryUseItem(Item item, Actor user, Actor target = null) {
            bool used = false;

            if (item.Targeting && target == null) {
                if (user is Player p) {
                    p.targetingWith = item; 
                    GameSettings.UIManager.GameArea.AddMessage("Use " + item.Name + " on what?");
                    return false;
                } else {
                    target = GameSettings.World.Player;
                }
            }

            if (item.UseID == "Heal") {
                int healAmount = GoRogue.DiceNotation.Dice.Roll(item.UseDice);
                user.CurrentHP = Math.Clamp(user.CurrentHP + healAmount, 0, user.MaxHP);

                GameSettings.UIManager.GameArea.AddMessage(user.Name + " used " + item.Name + " and healed " + healAmount + "!");
                 
                used = true;
            }

            if (item.UseID == "MagicMissile") {
                int damage = GoRogue.DiceNotation.Dice.Roll(item.UseDice);
                if (target != null) {
                    GameSettings.UIManager.GameArea.AddMessage(user.Name + " used " + item.Name + " on " + target.Name + "!");

                    target.TakeDamage(damage, user);
                    used = true;
                }
            }


            if (used) {
                if (item.Consumable) {
                    user.Inventory.Remove(item);
                }
            }

            return used;
        }
    }
}
