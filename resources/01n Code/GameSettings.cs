using SadTutorial.Data;
using SadTutorial.UI;

namespace SadTutorial {
    public static class GameSettings {
        public const int GameWidth = 160;
        public const int GameHeight = 50;

        public static IFont SquareFont;

        public static UIManager UIManager;
        public static NetworkManager NetworkManager;

        public static void SendMessageIfNecessary(NetMsg msg, int recipient) {
            if (NetworkManager.IsServerRunning()) {
                msg.SenderID = -1;
                NetworkManager.ServerSend(msg, recipient);
            }

            if (NetworkManager.IsClientConnected()) {
                NetworkManager.ClientSend(msg);
            }
        }
    }
}