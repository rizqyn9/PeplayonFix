using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject dd;

    private void Start()
    {
        dd = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        Canvas tt = dd.GetComponent<Canvas>();
        tt.enabled = false;
    }
}