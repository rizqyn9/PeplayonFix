using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientInstance : NetworkBehaviour
{
    [SerializeField]
    private NetworkIdentity _playerPrefab = null;

    [SerializeField]
    private NetworkIdentity _cameraprefab = null;

    [SerializeField]
    private NetworkIdentity _DustTrigger = null;

    [SerializeField]
    private NetworkIdentity _indicatoritemSpawn = null;

    [SerializeField]
    private NetworkIdentity _KillZone = null;

    [SerializeField]
    private NetworkIdentity dust = null;

    /*private GameObject currentPlayer;
    private GameObject currentCamera;
    private Transform startPoint;*/

    public Vector3 currenctCheckPoint;
    private GameObject rr;

    private Transform PlayerParent;
    private Transform CameraParent;

    private bool istrue;
    private bool ishave;
    private bool isis = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        /* PlayerParent = GameObject.Find("PlayerParent").transform;
         CameraParent = GameObject.Find("CameraParent").transform;*/
        NetworkSpawnPlayer();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        ChangeTag();
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
    }

    [Client]
    public void hasautorry(NetworkIdentity item, NetworkIdentity player)

    {
        if (!hasAuthority) return;
        changeAuthory(item, player);
        UnityEngine.Debug.Log("wahyu");
    }

    [Command]
    public void changeAuthory(NetworkIdentity itemd, NetworkIdentity played)
    {
        itemd.AssignClientAuthority(played.connectionToClient);
        UnityEngine.Debug.Log("has");
    }

    [Client]
    public void NetworkSpawnPlayer()
    {
        /* Destroy(currentPlayer);*/
        Transform killZoneSpawn1 = GameObject.Find("KillZone0").transform;
        Transform killZoneSpawn2 = GameObject.Find("KillZone1").transform;
        GameObject kz = Instantiate(_KillZone.gameObject, killZoneSpawn1.position, Quaternion.identity);
        GameObject kz2 = Instantiate(_KillZone.gameObject, killZoneSpawn2.position, Quaternion.identity);
        NetworkServer.Spawn(kz, base.connectionToClient);
        NetworkServer.Spawn(kz2, base.connectionToClient);

        GameObject cam = Instantiate(_cameraprefab.gameObject, transform.position, Quaternion.identity);
        rr = Instantiate(_playerPrefab.gameObject, transform.position, Quaternion.identity);

        NetworkServer.Spawn(cam, base.connectionToClient);
        NetworkServer.Spawn(rr, base.connectionToClient);
    }

    /*[ClientCallback]
    public void NetworkreSpawnPlayer()
    {
        GameObject cam = Instantiate(_cameraprefab.gameObject, currenctCheckPoint, Quaternion.identity);
        GameObject rr = Instantiate(_playerPrefab.gameObject, currenctCheckPoint, Quaternion.identity);

        currentCamera = cam.gameObject;
        currentPlayer = rr.gameObject;
        NetworkServer.Spawn(cam, base.connectionToClient);
        NetworkServer.Spawn(rr, base.connectionToClient);
    }*/

    private void Update()
    {
        if (istrue && isis == false && ishave == false)
        {
            if (!hasAuthority) return;
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject FF = GameObject.FindGameObjectWithTag("Player").gameObject;
            NetworkIdentity CV = FF.GetComponent<NetworkIdentity>();
            isis = true;

            spawn(player, CV);
        }
    }

    [Command]
    public void spawn(Transform pp, NetworkIdentity OPOPO)
    {
        Spawnn(pp, OPOPO);
    }

    [ClientRpc]
    public void Spawnn(Transform bb, NetworkIdentity BNM)
    {
        Vector3 pos = new Vector3(bb.position.x, 6.8f, bb.position.z);
        GameObject fuss = Instantiate(dust.gameObject, pos, bb.rotation, bb);

        NetworkIdentity gb = fuss.GetComponent<NetworkIdentity>();

        /*ccccc(gb, BNM);*/
        /* Instantiate(dust.gameObject, pos, bb.rotation, bb);*/

        ishave = true;
    }

    [Command]
    public void ccccc(NetworkIdentity g, NetworkIdentity BM)
    {
        g.AssignClientAuthority(BM.connectionToClient);
    }

    [Command]
    private void ChangeTag()
    {
        change();
    }

    [ClientRpc]
    public void change()
    {
        GameObject[] player = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject ca in player)
        {
            if (ca.CompareTag("GameManager"))
            {
                NetworkIdentity networkIdentity = ca.GetComponent<NetworkIdentity>();
                if (!networkIdentity.hasAuthority)
                {
                    Debug.Log("jj");
                    ca.tag = "MultiplayerGameManager";
                }
            }
        }

        foreach (GameObject ba in player)
        {
            if (ba.CompareTag("Deadzone"))
            {
                NetworkIdentity nn = ba.GetComponent<NetworkIdentity>();
                if (!nn.hasAuthority)
                {
                    ba.tag = "MultiplayerCamera";
                    ba.SetActive(false);
                }
            }
        }
        foreach (GameObject ga in player)
        {
            if (ga.CompareTag("PlayerCamera"))
            {
                NetworkTransform netBev = ga.GetComponent<NetworkTransform>();
                NetworkIdentity nn = ga.GetComponent<NetworkIdentity>();
                if (netBev.hasAuthority && nn.hasAuthority)
                {
                    ga.tag = "PlayerCamera";
                    GameObject cam = ga.gameObject.transform.GetChild(0).GetChild(0).gameObject;
                    cam.tag = "MainCamera";
                }
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    ga.tag = "MultiplayerCamera";
                    ga.SetActive(false);
                    /* GameObject cam = ga.gameObject.transform.GetChild(0).GetChild(0).gameObject;
                     cam.tag = "MultiplayerCamera";*/
                }
            }
        }

        foreach (GameObject go in player)
        {
            if (go.CompareTag("Player"))
            {
                NetworkTransform netBev = go.GetComponent<NetworkTransform>();
                NetworkIdentity nn = go.GetComponent<NetworkIdentity>();
                if (netBev.hasAuthority && nn.hasAuthority)
                {
                    go.tag = "Player";
                    GameObject ind = go.gameObject.transform.GetChild(0).gameObject;
                    ind.tag = "IndicatorItemSpawn";
                }
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    go.tag = "Owner";
                }
            }
        }
        foreach (GameObject fffff in player)
        {
            if (fffff.CompareTag("Owner"))
            {
                NetworkTransform netBev = fffff.GetComponent<NetworkTransform>();
                NetworkIdentity nn = fffff.GetComponent<NetworkIdentity>();
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    Debug.Log("owner");
                    GameObject df = fffff.gameObject.transform.GetChild(6).gameObject;
                    df.tag = "MultiplayerItemSpawn";
                }
            }
        }

        istrue = true;
    }
}