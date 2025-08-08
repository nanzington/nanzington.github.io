using SadTutorial.Data;
using System; 
using Telepathy; 

namespace SadTutorial {
    public class NetworkManager {
        Server server;
        Client client;

        public NetworkManager() {
            server = new Server(4096);
            server.OnConnected = OnConnected;
            server.OnData = OnData;
            server.OnDisconnected = OnDisconnected;


            client = new Client(4096);
            client.OnConnected = () => {
                GameLoop.UIManager.GameArea.MessageLog.Insert(0, "Connected to server.");
            };

            client.OnDisconnected = () => {
                GameLoop.UIManager.GameArea.MessageLog.Insert(0, "Disconnected from server.");
            };

            client.OnData = ClientOnData;
        }

        // BOTH: Call the Tick function to send and receive any waiting messages, whether hosting or connecting.
        public void Update() {
            if (server != null && server.Active) {
                server.Tick(100);
            }

            if (client != null && client.Connected) {
                client.Tick(100);
            }
        }

        // SERVER: Either broadcast a message to all clients or send a message to a specific client as requested.
        public void ServerSend(NetMsg msg, int toId = -1) {
            if (toId == -1) { 
                server.Broadcast(msg.ToByteArray());
            } else {
                server.Send(toId, msg.ToByteArray());
            }
        }

        // CLIENT: Send a message to the server host
        public void ClientSend(NetMsg msg) {
            client.Send(msg.ToByteArray());
        }



        // CLIENT: Try to connect to a server at the given IP and Port (requires the server to Port Forward in most cases)
        public void JoinServer(string IP, int port) {
            client.Connect(IP, port);
        }

        // SERVER: Start the server listener
        public void StartServer() {
            server.Start(25565);
            GameLoop.UIManager.GameArea.MessageLog.Insert(0, "Server started.");
        }

        // SERVER: Disconnect all clients and stop the server.
        public void StopServer() {
            server.Stop();
            GameLoop.UIManager.GameArea.MessageLog.Insert(0, "Server stopped.");
        }

        // SERVER: Returns true if hosting a server currently
        public bool IsServerRunning() {
            if (server != null)
                return server.Active;
            return false;
        }

        // CLIENT: Returns true if acting as a client and connected to a server.
        public bool IsClientConnected() {
            if (client != null)
                return client.Connected;
            return false;
        }

        // SERVER: What to do when a client connects, for now simply log it.
        public void OnConnected(int connectionId, string test) {
            GameLoop.UIManager.GameArea.MessageLog.Insert(0, connectionId + " connected.");
        }

        // SERVER: What to do when a client disconnects, for now simply log it.
        public void OnDisconnected(int connectionId) { 
            GameLoop.UIManager.GameArea.MessageLog.Insert(0, connectionId + " disconnected.");
        }

        // CLIENT: Receive the data, pass it to the main processor with our special ID for the server (-1)
        public void ClientOnData(ArraySegment<byte> data) {
            OnData(-1, data);
        }


        // BOTH: Process a message into something actionable
        public void OnData(int connectionId, ArraySegment<byte> data) {
            NetMsg msg = data.Array.FromByteArray<NetMsg>();

            if (msg.Message == "SetBool") {
                GameLoop.UIManager.GameArea.NetSwitch = msg.MiscBool;
                GameLoop.UIManager.GameArea.MessageLog.Insert(0, connectionId + " set the switch to " + msg.MiscBool.ToString() + ".");
            }
        }

        // CLIENT: Disconnect from Server
        public void DisconnectFromServer() {
            client.Disconnect();
        }
    }
}
