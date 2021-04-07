using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : NetworkBehaviour
{
    [Client]
    private void OnTriggerEnter(Collider other)
    {
        ClientInstance manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ClientInstance>();
        if (other.CompareTag("Player"))
        {
            manager.currenctCheckPoint = transform.position;
        }
    }
}