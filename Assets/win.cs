using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class win : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ClientInstance cl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
            NetworkIdentity player = other.GetComponent<NetworkIdentity>();
            NetworkIdentity item = GetComponent<NetworkIdentity>();
            cl.hasautorry(item, player);

            SetWin();
        }
    }

    [Client]
    public void SetWin()
    {
        if (!hasAuthority) return;
        GameObject localplayer = ClientScene.localPlayer.gameObject;
        NetworkIdentity get = localplayer.GetComponent<NetworkIdentity>();
        string nm = get.netId.ToString();
        Debug.Log(nm);

        Wiin(nm);
    }

    [Command]
    public void Wiin(string kl)
    {
        setWint(kl);
    }

    [ClientRpc]
    public void setWint(string hj)
    {
        GameObject klkl = ClientScene.localPlayer.gameObject;
        NetworkIdentity ff = klkl.GetComponent<NetworkIdentity>();
        string hh = ff.netId.ToString();
        Debug.Log(hh);

        if (hh == hj)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("kalah");
        }
    }
}