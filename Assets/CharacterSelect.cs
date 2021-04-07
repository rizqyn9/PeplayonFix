using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wahyu;

public class CharacterSelect : MonoBehaviour
{
    private ManagerMenu manager;

    public void Character_one()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerMenu>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CharacterOne", 1);
        manager.isSpawn = false;
        manager.SpawnCharacter(0);
        Debug.Log("1");
    }

    public void Character_two()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerMenu>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CharacterTwo", 1);
        manager.isSpawn = false;
        manager.SpawnCharacter(1);
        Debug.Log("2");
    }

    public void Character_tree()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerMenu>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CharacterThree", 1);
        manager.isSpawn = false;
        manager.SpawnCharacter(2);
        Debug.Log("3");
    }
}