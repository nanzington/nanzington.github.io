namespace SadTutorial.Data {
    public class NetMsg {
        public int SenderID = -1;
        public string Message = "";

        public int MiscInt = 0;
        public bool MiscBool = false;
        public string MiscString = "";


        public NetMsg(string msg, int ownID) {
            Message = msg;
            SenderID = ownID;
        }
    }
}
