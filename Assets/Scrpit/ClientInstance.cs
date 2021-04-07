using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientInstance : NetworkBehaviour
{
    [SerializeField]
    private NetworkIdentity[] _playerPrefab = null;

    [SerializeField]
    private NetworkIdentity _cameraprefab = null;

    [SerializeField]
    private NetworkIdentity _indicatoritemSpawn = null;

    [SerializeField]
    private NetworkIdentity _KillZone = null;

    [SerializeField]
    private NetworkIdentity dust = null;

    public Vector3 currenctCheckPoint;

    private GameObject rr;
    private bool istrue;
    private bool ishave;
    private bool isis = false;
    private int one;
    private int two;
    private int tree;

    #region NetworkBehaviour

    private void Start()
    {
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkSpawnPlayer();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        CMDChangeTag();
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
    }

    private void Update()
    {
        if (istrue && isis == false && ishave == false)
        {
            if (!hasAuthority) return;
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject FF = GameObject.FindGameObjectWithTag("Player").gameObject;
            NetworkIdentity CV = FF.GetComponent<NetworkIdentity>();
            isis = true;

            CMDspawnDust(player, CV);
        }
    }

    #endregion NetworkBehaviour

    #region Get Authority

    [Client]
    public void getauthority(NetworkIdentity item, NetworkIdentity player)

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

    #endregion Get Authority

    #region Spawn Player ,Dust Trigger, Killzone

    [Client]
    public void NetworkSpawnPlayer()
    {
        one = PlayerPrefs.GetInt("CharacterOne");
        two = PlayerPrefs.GetInt("CharacterTwo");
        tree = PlayerPrefs.GetInt("CharacterThree");

        Transform killZoneSpawn1 = GameObject.Find("KillZone0").transform;
        Transform killZoneSpawn2 = GameObject.Find("KillZone1").transform;
        GameObject kz = Instantiate(_KillZone.gameObject, killZoneSpawn1.position, Quaternion.identity);
        GameObject kz2 = Instantiate(_KillZone.gameObject, killZoneSpawn2.position, Quaternion.identity);
        NetworkServer.Spawn(kz, base.connectionToClient);
        NetworkServer.Spawn(kz2, base.connectionToClient);

        if (one == 1)
        {
            GameObject cam = Instantiate(_cameraprefab.gameObject, transform.position, Quaternion.identity);
            rr = Instantiate(_playerPrefab[0].gameObject, transform.position, Quaternion.identity);

            NetworkServer.Spawn(cam, base.connectionToClient);
            NetworkServer.Spawn(rr, base.connectionToClient);
        }
        else if (two == 1)
        {
            GameObject cam = Instantiate(_cameraprefab.gameObject, transform.position, Quaternion.identity);
            rr = Instantiate(_playerPrefab[1].gameObject, transform.position, Quaternion.identity);

            NetworkServer.Spawn(cam, base.connectionToClient);
            NetworkServer.Spawn(rr, base.connectionToClient);
        }
        else if (tree == 1)
        {
            GameObject cam = Instantiate(_cameraprefab.gameObject, transform.position, Quaternion.identity);
            rr = Instantiate(_playerPrefab[2].gameObject, transform.position, Quaternion.identity);

            NetworkServer.Spawn(cam, base.connectionToClient);
            NetworkServer.Spawn(rr, base.connectionToClient);
        }
        else if (tree == 0 && one == 0 && two == 0)
        {
            GameObject cam = Instantiate(_cameraprefab.gameObject, transform.position, Quaternion.identity);
            rr = Instantiate(_playerPrefab[1].gameObject, transform.position, Quaternion.identity);

            NetworkServer.Spawn(cam, base.connectionToClient);
            NetworkServer.Spawn(rr, base.connectionToClient);
        }
    }

    [Command]
    public void CMDspawnDust(Transform pp, NetworkIdentity OPOPO)
    {
        ClienRPCSpawnDust(pp, OPOPO);
    }

    [ClientRpc]
    public void ClienRPCSpawnDust(Transform bb, NetworkIdentity BNM)
    {
        Vector3 pos = new Vector3(bb.position.x, 2.6f, bb.position.z);
        GameObject fuss = Instantiate(dust.gameObject, pos, bb.rotation, bb);

        NetworkIdentity gb = fuss.GetComponent<NetworkIdentity>();

        ishave = true;
    }

    #endregion Spawn Player ,Dust Trigger, Killzone

    #region Change Tag Player

    [Command]
    private void CMDChangeTag()
    {
        ClientRPCchange();
    }

    [ClientRpc]
    public void ClientRPCchange()
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

    #endregion Change Tag Player
}