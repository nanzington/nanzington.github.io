using SadTutorial.Data;

namespace SadTutorial {
    public static class ItemManager {
        public static bool TryUseItem(Item item, Actor user) {
            if (item.UseID == "Heal") {
                int healAmount = GoRogue.DiceNotation.Dice.Roll(item.UseDice);
                user.CurrentHP = Math.Clamp(user.CurrentHP + healAmount, 0, user.MaxHP);

                GameSettings.UIManager.GameArea.AddMessage(user.Name + " used " + item.Name + " and healed " + healAmount + "!");

                user.Inventory.Remove(item);
                return true;
            }


            return false;
        }
    }
}
