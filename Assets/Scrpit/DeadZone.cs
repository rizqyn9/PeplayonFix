using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : NetworkBehaviour
{
    private DetectChild dt;
    private CharacterControls cr;
    private ClientInstance client;
    private ItemPickup tp;

    private void Start()
    {
        dt = GameObject.FindGameObjectWithTag("IndicatorItemSpawn").GetComponent<DetectChild>();
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();
        client = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
        tp = GameObject.FindGameObjectWithTag("Item").GetComponent<ItemPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAuthority) return;
        if (other.CompareTag("Player"))
        {
            other.transform.position = client.currenctCheckPoint;
            tp.StopAllCoroutines();
            dt.Child();
            cr.SetDefaultValue();
            tp.DestroyTransculent();
            tp.de();
        }
    }
}