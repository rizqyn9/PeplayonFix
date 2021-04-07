using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMainMenu : MonoBehaviour
{
    private bool isSpawn;
    private int CharacterSelected1;
    private int CharacterSelected2;
    private int CharacterSelected3;
    private GameObject CurrentCharacter;

    public GameObject[] PlayerPrefab;
    public Transform SpawnPoint;
    private int selectRotate;

    private void Start()
    {
        CharacterSelected1 = PlayerPrefs.GetInt("CharacterOne");
        CharacterSelected2 = PlayerPrefs.GetInt("CharacterTwo");
        CharacterSelected3 = PlayerPrefs.GetInt("CharacterThree");
    }

    private void Update()
    {
        if (CharacterSelected1 == 1)
        {
            SpawnCharacterMainMenu(0);
            selectRotate = 0;
            Debug.Log("111");
        }
        else if (CharacterSelected2 == 1)
        {
            SpawnCharacterMainMenu(1);
            selectRotate = 1;
            Debug.Log("222");
        }
        else if (CharacterSelected3 == 1)
        {
            SpawnCharacterMainMenu(2);
            selectRotate = 2;
            Debug.Log("333");
        }
        else
        {
            SpawnCharacterMainMenu(0);
            selectRotate = 0;
            Debug.Log("def");
        }
    }

    private void SpawnCharacterMainMenu(int select)
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
}