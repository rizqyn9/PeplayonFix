using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    #region Variable

    public List<TabButton> tabButtons;
    public Sprite hover;
    public Sprite active;
    public Sprite idle;
    public TabButton selectedTab;
    public List<GameObject> selectswap;

    #endregion Variable

    #region Public Method

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        resetTab();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = hover;
        }
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        resetTab();
        button.background.sprite = active;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < selectswap.Count; i++)
        {
            if (i == index)
            {
                selectswap[i].SetActive(true);
            }
            else
            {
                selectswap[i].SetActive(false);
            }
        }
    }

    public void OnTabExit(TabButton button)
    {
        resetTab();
    }

    public void resetTab()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = idle;
        }
    }

    #endregion Public Method
}