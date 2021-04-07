using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ItemPickup : NetworkBehaviour
{
    public int indexItem;
    public float jumpHeightplus;
    public float speedplus;
    public float speedandjumpStun;
    public float speedmin;
    public float Countdown;
    public GameObject EffectPrefab;
    public MeshRenderer mes;
    public Collider coll;

    private CharacterControls characterControls;
    private GameObject ssignAuthorityObj;
    private bool iya = false;
    private GameObject ohteer;
    private UI ui;
    private DetectChild detect;
    private GameObject Effect;

    #region networkbehaviour

    private void Update()
    {
        if (iya)
        {
            if (hasAuthority)
            {
                iya = false;
                ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
                detect = GameObject.FindGameObjectWithTag("IndicatorItemSpawn").GetComponent<DetectChild>();
                Debug.Log("PICKUP");
                if (indexItem == 1)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("JUMP");
                    Jump();
                    ClientRPCsetTransparentBox();
                }
                else if (indexItem == 2)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("RUN");
                    Run();
                    ClientRPCsetTransparentBox();
                }
                else if (indexItem == 3)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("SLOW");
                    Slow();
                    ClientRPCsetTransparentBox();
                }
                else if (indexItem == 4)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("TRANSCULENT");
                    Transculent();
                    ClientRPCsetTransparentBox();
                }
                else if (indexItem == 5)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("FLASHBACK");
                    Flashback(ohteer.gameObject);
                    ClientRPCsetTransparentBox();
                }
                else if (indexItem == 6)
                {
                    ui.ClientSetIndItem(0, ohteer);

                    Debug.Log("STUNT");
                    Stunt();
                    ClientRPCsetTransparentBox();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        characterControls = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();

        if (other.CompareTag("Player"))
        {
            iya = true;
            ohteer = other.gameObject;
            NetworkIdentity player = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkIdentity>();
            NetworkIdentity item = GetComponent<NetworkIdentity>();
            ClientInstance cl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
            cl.getauthority(item, player);
        }
    }

    #endregion networkbehaviour

    #region settransparentbox

    [Command]
    private void CMDsetTransparentBox()
    {
        ClientRPCsetTransparentBox();
    }

    [ClientRpc]
    public void ClientRPCsetTransparentBox()
    {
        Debug.Log("SENT ALL TO RPC");
        mes.enabled = false;
        coll.enabled = false;
    }

    #endregion settransparentbox

    #region Coroutine item

    public void Jump()
    {
        StartCoroutine(setJump());
    }

    public void Run()
    {
        StartCoroutine(setRun());
    }

    public void Slow()
    {
        StartCoroutine(setSlow());
    }

    public void Transculent()
    {
        StartCoroutine(setTransculent());
    }

    public void Flashback(GameObject Player)
    {
        StartCoroutine(setFlashback(Player));
    }

    public void Stunt()
    {
        StartCoroutine(setStun());
    }

    public void DestroyTransculent()
    {
        Collider[] AllObject = FindObjectsOfType<Collider>();

        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = false;
            }
        }
    }

    #region Set effect and  Destory

    [Client]
    public void setEffect(GameObject pp)
    {
        if (!hasAuthority) return;

        CMDseteffect(pp);
    }

    [Client]
    public void destroyEffect()
    {
        if (!hasAuthority) return;

        CMDdestroyEffect();
    }

    [Command]
    public void CMDseteffect(GameObject jj)
    {
        ClientRPCsetEffect(jj);
    }

    [Command]
    public void CMDdestroyEffect()
    {
        ClientRPCsetDestroyEffect();
    }

    [ClientRpc]
    public void ClientRPCsetEffect(GameObject aa)
    {
        Effect = Instantiate(EffectPrefab, aa.transform.position, aa.transform.rotation, aa.transform) as GameObject;
    }

    [ClientRpc]
    public void ClientRPCsetDestroyEffect()
    {
        Debug.Log("Destroy effect");
        Destroy(Effect);
    }

    #endregion Set effect and  Destory

    private IEnumerator setJump()
    {
        ui.UIeffect.SetActive(true);
        ui.effecthighjump();
        characterControls.jumpHeight = jumpHeightplus;

        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.jumpHeight = 5f;

        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setRun()
    {
        ui.UIeffect.SetActive(true);
        ui.effectfaster();
        characterControls.speed = speedplus;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.speed = 10f;
        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setSlow()
    {
        ui.UIeffect.SetActive(true);
        ui.effectslower();
        characterControls.speed = speedmin;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.speed = 10f;
        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setTransculent()
    {
        ui.UIeffect.SetActive(true);
        ui.effecttransculent();
        Collider[] AllObject = FindObjectsOfType<Collider>();

        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = true;
            }
        }
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        foreach (Collider co in AllObject)
        {
            if (co.CompareTag("Object"))
            {
                co.isTrigger = false;
            }
        }
        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setFlashback(GameObject player)
    {
        ui.UIeffect.SetActive(true);
        ui.effectrespawn();
        ClientInstance clientInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        ui.UIeffect.SetActive(false);
        player.transform.position = clientInstance.currenctCheckPoint;
        detect.Child();
    }

    private IEnumerator setStun()
    {
        ui.UIeffect.SetActive(true);
        ui.EFFECTSTUNT();
        characterControls.speed = speedandjumpStun;
        characterControls.jumpHeight = speedandjumpStun;
        setEffect(ohteer);
        yield return new WaitForSeconds(Countdown);
        destroyEffect();
        characterControls.jumpHeight = 5f;
        characterControls.speed = 10f;

        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    #endregion Coroutine item
}