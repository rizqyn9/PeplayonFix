using UnityEngine;
using Mirror;

namespace Peplayon
{
    internal class SpawnItem

    {
        internal static void Spawnitemrandom()
        {
            if (!NetworkServer.active) return;

            for (int a = 0; a < ((NetworkManagerPong)NetworkManager.singleton)._item.Length; a++)
            {
                int b = Random.Range(0, ((NetworkManagerPong)NetworkManager.singleton)._item.Length);
                NetworkServer.Spawn(Object.Instantiate(((NetworkManagerPong)NetworkManager.singleton)._item[b].gameObject, ((NetworkManagerPong)NetworkManager.singleton).PointSpawnItem[a].position, Quaternion.identity));
            }
        }
    }
}