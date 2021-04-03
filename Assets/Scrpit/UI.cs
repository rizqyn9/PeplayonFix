using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : NetworkBehaviour
{
    public GameObject[] IndicatorItem;
    private Transform IndicatorPointParent;
    private bool Call;
    private GameObject tt;

    public GameObject dd;

    [SerializeField]
    private GameObject dust;

    public GameObject UIeffect;
    public Text texteffect;
    private Transform gk;

    #region Public Method

    public void effectfaster()
    {
        texteffect.text = "FASTER";
    }

    public void effectslower()
    {
        texteffect.text = "SLOWER";
    }

    public void effectbigger()
    {
        texteffect.text = "BIGGER";
    }

    public void effecttransculent()
    {
        texteffect.text = "TRANSCULENT";
    }

    public void effecthighjump()
    {
        texteffect.text = "HIGH JUMP";
    }

    public void EFFECTSTUNT()
    {
        texteffect.text = "STUNT";
    }

    public void effectrespawn()
    {
        texteffect.text = "RESPAWN";
    }

    #endregion Public Method

    [ClientRpc]
    public void startCutcsene()
    {
        Debug.Log("sadadffff");
        dd.SetActive(true);
        MovableObs.ready = true;
    }

    [Command]
    public void jjj()
    {
        nm();
    }

    [ClientRpc]
    public void nm()
    {
        CharacterControls.cutsceneawal = false;
    }

    private void LateUpdate()
    {
        if (Call)
        {
            /*  Transform Camera = GameObject.FindGameObjectWithTag("Tes").transform;
              IndicatorPointParent.LookAt(IndicatorPointParent.position, Camera.forward);*/
        }
    }

    [Client]
    public void Dust(Transform dddd)
    {
        gk = dddd;
        if (!hasAuthority) return;
        callSetDust(dddd);
    }

    [Command]
    public void callSetDust(Transform vvv)
    {
        setDust(vvv);
    }

    [ClientRpc]
    public void setDust(Transform cccc)
    {
        Transform DD = GameObject.FindGameObjectWithTag("Dust").transform;
        Instantiate(dust, DD.position, DD.rotation, gk);
        Debug.Log("Ddfdf");
    }

    public void setCommandInd(int b, GameObject hit)
    {
        if (!hasAuthority) return;
        setIndItem(b, hit);
        Debug.Log("indicatoritem");
    }

    public void setCommanddes(Transform v)
    {
        if (!hasAuthority) return;
        setDestroyIndItem(v);
        Debug.Log("indicatoritem");
    }

    [Command]
    public void setIndItem(int a, GameObject hit)
    {
        SetIndicatorItem(a, hit);
    }

    [Command]
    public void setDestroyIndItem(Transform z)
    {
        destroy(z);
    }

    [ClientRpc]
    public void SetIndicatorItem(int index, GameObject player)
    {
        GameObject indicatorSpawn = player.transform.GetChild(6).gameObject;

        IndicatorPointParent = indicatorSpawn.transform;
        tt = Instantiate(IndicatorItem[index], IndicatorPointParent.position, Quaternion.identity, IndicatorPointParent);

        /* if (!hasAuthority)
         {
             private Transform IndicatorItemPoint = GameObject.FindGameObjectWithTag("MultiplayerItemSpawn").transform;

     IndicatorPointParent = IndicatorItemPoint.transform;

             tt = private Instantiate(IndicatorItem[index], IndicatorPointParent.position, Quaternion.identity, IndicatorPointParent);
 }*/
    }

    /*[ClientRpc]
    public void SetIndicatorItemmm(int index)
    {
        if (isLocalPlayer) return;

        Call = true;
    }*/

    [ClientRpc]
    public void destroy(Transform dd)
    {
        Destroy(tt);
    }
}