using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPodium : MonoBehaviour
{
    #region Variable

    private GameObject CurrentCharacter;
    private int CharacterOnSelect1;
    private int CharacterOnSelect2;
    private int CharacterOnSelect3;
    private bool ttt = true;

    public bool isSpawn = false;
    public GameObject alas1;
    public GameObject alas2;
    public GameObject alas3;
    public GameObject[] PlayerPrefab;
    public Transform SpawnPoint;
    public Transform[] spawnFirework;
    public float Speed;
    public GameObject win;
    public GameObject mainmenu;
    public GameObject firework;

    #endregion Variable

    #region MonobehaviourCallBack

    // Start is called before the first frame update
    private void Start()
    {
        CharacterOnSelect1 = PlayerPrefs.GetInt("CharacterOne");
        CharacterOnSelect2 = PlayerPrefs.GetInt("CharacterTwo");
        CharacterOnSelect3 = PlayerPrefs.GetInt("CharacterThree");
    }

    // Update is called once per frame
    private void Update()
    {
        if (CharacterOnSelect1 == 1)
        {
            SpawnCharacterPodium(0);
            if (ttt)
            {
                StartCoroutine(animation());
                ttt = false;
            }
        }
        else if (CharacterOnSelect2 == 1)
        {
            SpawnCharacterPodium(1);

            if (ttt)
            {
                StartCoroutine(animation());
                ttt = false;
            }
        }
        else if (CharacterOnSelect3 == 1)
        {
            SpawnCharacterPodium(2);

            if (ttt)
            {
                StartCoroutine(animation());
                ttt = false;
            }
        }

        Transform getplayer = GameObject.FindGameObjectWithTag("Player").transform;
        getplayer.transform.Rotate(0, Time.deltaTime * Speed, 0);
    }

    #endregion MonobehaviourCallBack

    #region Private Method

    private IEnumerator animation()
    {
        LeanTween.init(1000);
        LeanTween.move(alas1, new Vector3(-8.83499993e-08f, -7.03000021f, 0), 1f);

        yield return new WaitForSeconds(1f);

        LeanTween.move(alas3, new Vector3(1.70000005f, -4.40000105f, -1.79999995f), 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.move(alas2, new Vector3(-3.70000005f, -5.00000048f, 0), 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.move(alas1, new Vector3(-8.83499993e-08f, -5.03000021f, 0), 1.5f);
        yield return new WaitForSeconds(1.5f);
        Instantiate(firework, spawnFirework[0].position, spawnFirework[0].rotation);
        Instantiate(firework, spawnFirework[1].position, spawnFirework[1].rotation);
        LeanTween.move(CurrentCharacter, new Vector3(0, 1, 0), 4f);
        yield return new WaitForSeconds(4f);
        win.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mainmenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SpawnCharacterPodium(int select)
    {
        if (!isSpawn)
        {
            if (!CurrentCharacter)
            {
                Destroy(CurrentCharacter);
            }

            isSpawn = true;

            GameObject spawn = Instantiate(PlayerPrefab[select], SpawnPoint.position, SpawnPoint.rotation);
            CurrentCharacter = spawn;
        }
    }

    #endregion Private Method
}