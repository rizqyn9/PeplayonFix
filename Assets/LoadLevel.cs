using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Loadlevel(int sceneIndex)
    {
        StartCoroutine(loadasyncronously(sceneIndex));
    }

    public void LoadlevelwithDisconnected(int sceneIndex)
    {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        StartCoroutine(loadasyncronously(sceneIndex));
    }

    private IEnumerator loadasyncronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void exitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}