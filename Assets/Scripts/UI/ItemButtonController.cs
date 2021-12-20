using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour, IPointerClickHandler
{
    private string _itemName;

    public void InitializeButton(string rn)
    {
        _itemName = rn;
        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = rn;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print(_itemName);
        ItemManager.Instance.CreateItemBlueprint(_itemName);
    }
}
