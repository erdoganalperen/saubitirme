using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUIMove : MonoBehaviour, IPointerClickHandler
{
    ItemController _itemController;
    public void Awake()
    {
        _itemController = GetComponentInParent<ItemController>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _itemController.Move();
    }
}
