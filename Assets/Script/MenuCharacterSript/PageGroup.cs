using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wahyu;

public class PageGroup : MonoBehaviour
{
    #region Variable

    public List<CharacterList> characterLists;
    public List<GameObject> objectswap;
    public CharacterList selectedCharacter;

    public Sprite idle;
    public Sprite enter;
    public Sprite selected;

    #endregion Variable

    #region public method

    public void Subscribe(CharacterList character)
    {
        if (characterLists == null)
        {
            characterLists = new List<CharacterList>();
        }
        characterLists.Add(character);
    }

    public void OnListEnter(CharacterList character)
    {
        resetTab();

        if (selectedCharacter == null || character != selectedCharacter)
        {
            character.image.sprite = enter;
        }
    }

    public void OnListSelected(CharacterList character)
    {
        selectedCharacter = character;
        resetTab();
        character.image.sprite = selected;
        /* int index = character.transform.GetSiblingIndex();

         for (int i = 0; i < objectswap.Count; i++)
         {
             if (i == index)
             {
                 objectswap[i].SetActive(true);
             }
             else
             {
                 objectswap[i].SetActive(false);
             }
         }*/
    }

    public void OnListExit(CharacterList character)
    {
        resetTab();
    }

    public void resetTab()
    {
        foreach (CharacterList character in characterLists)
        {
            if (selectedCharacter != null && character == selectedCharacter)
            {
                continue;
            }

            character.image.sprite = idle;
        }
    }

    #endregion public method
}