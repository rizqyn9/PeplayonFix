using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectChild : MonoBehaviour
{
    public void Child()
    {
        UI ui = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UI>();
        foreach (Transform child in transform)
        {
            if (transform)
            {
                ui.ClientsetDestroyIndItem(child);
                Destroy(child.gameObject);
            }
        }
    }
}