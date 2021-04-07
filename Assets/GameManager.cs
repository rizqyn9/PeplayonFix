using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public Vector3 lastCheckPoint;

    [SerializeField]
    private NetworkIdentity _rplayerPrefab = null;

    [SerializeField]
    private NetworkIdentity _CameraPlayer = null;

    [Command]
    public void RespawnCharacter()
    {
        tttt();
    }

    [ClientRpc]
    public void tttt()
    {
        GameObject respawnplayer = Instantiate(_rplayerPrefab.gameObject, lastCheckPoint, Quaternion.identity);
        GameObject respawncamera = Instantiate(_CameraPlayer.gameObject, lastCheckPoint, Quaternion.identity);
        NetworkServer.Spawn(respawnplayer, base.connectionToClient);
        NetworkServer.Spawn(respawncamera, base.connectionToClient);
    }
}