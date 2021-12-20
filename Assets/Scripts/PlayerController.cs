using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager _inputManager;
    GameManager _gameManager;
    ItemManager _itemManager;
    void Start()
    {
        _inputManager = InputManager.Instance;
        _gameManager = GameManager.Instance;
        _itemManager = ItemManager.Instance;
    }
    void Update()
    {
        if (_inputManager.LeftClick)
        {
            if (_gameManager.currentState == States.Blueprint)
            {
                _itemManager.BuildItem();
            }
            else if (_gameManager.currentState == States.ItemMoving)
            {
                _itemManager.PlaceItem();
            }
            else
            {
                _itemManager.SelectItem();
            }
        }
    }
}
