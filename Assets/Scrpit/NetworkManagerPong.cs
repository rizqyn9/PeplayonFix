using UnityEngine;
using Mirror;
using UnityEngine.Playables;

namespace Peplayon
{
    [AddComponentMenu("")]
    public class NetworkManagerPong : NetworkManager

    {
        public static bool haha;
        public NetworkIdentity[] _item = null;

        public Transform[] PointSpawnItem;

        public Transform spawnplayer;

        public Transform ItemParent;

        public override void OnStartServer()
        {
            base.OnStartServer();
            SpawnItem.Spawnitemrandom();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab, spawnplayer.position, spawnplayer.rotation);

            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
        }

        private void Update()
        {
            if (NetworkServer.connections.Count >= 1)
            {
                checkPlayer();
            }

            if (haha)
            {
                Playable();
            }
        }

        public void checkPlayer()
        {
            Debug.Log("gaga");
            UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();

            ui.startCutcsene();
        }

        public void Playable()
        {
            UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
            ui.jjj();
        }
    }
}