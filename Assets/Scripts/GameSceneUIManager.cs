using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : GenericSingleton<GameSceneUIManager>
{
    [SerializeField] private GameObject itemButton;
    [SerializeField] private RectTransform itemsParent;

    public void CreateItemButtons(Object[] list)
    {
        foreach (var item in list)
        {
            var btn = Instantiate(itemButton, itemsParent);
            var component = btn.AddComponent<ItemButtonController>();
            component.InitializeButton(item.name);
        }
    }
}