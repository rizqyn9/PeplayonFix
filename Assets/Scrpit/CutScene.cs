using Mirror;
using Peplayon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : NetworkBehaviour
{
    private GameObject PlayerCamera;
    private GameObject Camera1;
    private Camera Camera2;

    public GameObject CountStart;

    public Text txt;

    private CharacterControls cr;
    private UI ui;
    private bool run = true;

    private void Awake()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;

        PlayerCamera.SetActive(false);
    }

    [Client]
    private void Update()
    {
        if (run)
        {
            Camera1 = GameObject.FindGameObjectWithTag("Camera1");
            Camera2 = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>();
            cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
            run = false;
            StartCoroutine(startCutScene());
        }
    }

    private IEnumerator startCutScene()
    {
        LeanTween.move(Camera1, new Vector3(0, 19.8999996f, 253.399994f), 9f);
        yield return new WaitForSeconds(10);

        Camera1.SetActive(false);
        Camera2.enabled = true;

        LeanTween.move(Camera2.gameObject, new Vector3(2.5f, 18, -53.5999985f), 3f);
        yield return new WaitForSeconds(2);

        LeanTween.move(Camera2.gameObject, new Vector3(14.3999996f, 23.8999996f, -94.5f), 1f);

        LeanTween.rotate(Camera2.gameObject, new Vector3(48.2000122f, 180f, 0), 1f);
        yield return new WaitForSeconds(1);
        LeanTween.move(Camera2.gameObject, new Vector3(-11.6999998f, 23.8999996f, -94.5f), 4f);
        yield return new WaitForSeconds(3);
        /* clear = true;*/
        yield return new WaitForSeconds(1);
        Camera2.enabled = false;
        PlayerCamera.SetActive(true);

        CountStart.SetActive(true);
        yield return new WaitForSeconds(1);
        txt.text = "2";
        yield return new WaitForSeconds(1);
        txt.text = "3";
        yield return new WaitForSeconds(1);
        CountStart.SetActive(false);
        playablePlayer();
    }

    private void playablePlayer()
    {
        NetworkManagerPong.haha = true;
    }
}