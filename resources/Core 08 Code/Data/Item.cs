namespace SadTutorial.Data {
    public class Item : Entity {
        public string UseID = "";
        public string UseDice = "";

        public bool Consumable = false;
        public bool Targeting = false;

        public Item(string n, int g, Color c, int x, int y) : base(n, g, c, x, y) {
        }
    }
}
