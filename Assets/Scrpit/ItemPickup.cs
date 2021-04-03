using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ItemPickup : NetworkBehaviour
{
    private CharacterControls characterControls;
    public int indexItem;
    public float jumpHeightplus;
    public float speedplus;
    public float speedandjumpStun;
    public float speedmin;
    public float Countdown;
    public GameObject EffectPrefab;
    public MeshRenderer mes;
    public Collider coll;
    private GameObject ssignAuthorityObj;
    private bool iya = false;
    private GameObject ohteer;

    private UI ui;
    private DetectChild detect;
    private GameObject Effect;

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
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("JUMP");
                    Jump();
                }
                else if (indexItem == 2)
                {
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("RUN");
                    Run();
                    setTransparentBox();
                }
                else if (indexItem == 3)
                {
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("SLOW");
                    Slow();
                    setTransparentBox();
                }
                else if (indexItem == 4)
                {
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("TRANSCULENT");
                    Transculent();
                    setTransparentBox();
                }
                else if (indexItem == 5)
                {
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("FLASHBACK");
                    Flashback(ohteer.gameObject);
                    setTransparentBox();
                }
                else if (indexItem == 6)
                {
                    ui.setCommandInd(0, ohteer);
                    ff();
                    Debug.Log("STUNT");
                    Stunt();
                    setTransparentBox();
                }

                mes.enabled = false;
                coll.enabled = false;
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
            NetworkIdentity playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkIdentity>();
            NetworkIdentity itemm = GetComponent<NetworkIdentity>();
            ClientInstance cl = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
            cl.hasautorry(itemm, playerr);
        }
    }

    [Command]
    private void ff()
    {
        setTransparentBox();
    }

    [ClientRpc]
    public void setTransparentBox()
    {
        Debug.Log("SENT ALL TO RPC");
        mes.enabled = false;
        coll.enabled = false;
    }

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

    public void yyy(GameObject pp)
    {
        if (!hasAuthority) return;

        effect(pp);
    }

    public void de()
    {
        if (!hasAuthority) return;

        destroyEffect();
    }

    [Command]
    public void effect(GameObject jj)
    {
        setEffect(jj);
    }

    [Command]
    public void destroyEffect()
    {
        setDestroyEffect();
    }

    [ClientRpc]
    public void setEffect(GameObject aa)
    {
        Effect = Instantiate(EffectPrefab, aa.transform.position, aa.transform.rotation, aa.transform) as GameObject;
    }

    [ClientRpc]
    public void setDestroyEffect()
    {
        Debug.Log("Destroy effect");
        Destroy(Effect);
    }

    private IEnumerator setJump()
    {
        ui.UIeffect.SetActive(true);
        ui.effecthighjump();
        characterControls.jumpHeight = jumpHeightplus;

        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
        de();
        characterControls.jumpHeight = 5f;

        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setRun()
    {
        ui.UIeffect.SetActive(true);
        ui.effectfaster();
        characterControls.speed = speedplus;
        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
        de();
        characterControls.speed = 10f;
        detect.Child();
        ui.UIeffect.SetActive(false);
    }

    private IEnumerator setSlow()
    {
        ui.UIeffect.SetActive(true);
        ui.effectslower();
        characterControls.speed = speedmin;
        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
        de();
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
        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
        de();
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
        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
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
        yyy(ohteer);
        yield return new WaitForSeconds(Countdown);
        de();
        characterControls.jumpHeight = 5f;
        characterControls.speed = 10f;

        detect.Child();
        ui.UIeffect.SetActive(false);
    }
}