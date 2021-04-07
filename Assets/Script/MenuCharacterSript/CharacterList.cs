using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using wahyu;

[RequireComponent(typeof(Image))]
public class CharacterList : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public PageGroup pageGroup;

    public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        pageGroup.OnListSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pageGroup.OnListEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pageGroup.OnListExit(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        pageGroup.Subscribe(this);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}