using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ItemController : MonoBehaviour
{
    InputManager _inputManager;
    ItemManager _itemManager;
    public string itemName;
    public bool isRaycasting;
    bool isSelected;
    bool isMoving;
    public GameObject itemUI;
    bool isOutline;
    Outline _outline;
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _inputManager = InputManager.Instance;
        _itemManager = ItemManager.Instance;
        _outline.enabled = false;
        tag = "Item";
        gameObject.layer = LayerMask.NameToLayer("Blueprint");
    }
    private void Update()
    {
        RaycastCheck();
    }
    public void Rotate()
    {
        GameManager.Instance.currentState = States.ItemRotating;
        _itemManager.isItemRotating = true;
        _inputManager.lockFlow = true;
        CloseItemUI();
    }
    public void Move()
    {
        GameManager.Instance.currentState = States.ItemMoving;
        _itemManager.isItemMoving = true;
        CloseItemUI();
    }
    public void Place()
    {
        GameManager.Instance.currentState = States.ItemSelected;
        _itemManager.isItemMoving = false;
        _itemManager.isItemRotating = false;
        _inputManager.lockFlow = false;
        OpenItemUI();
    }
    public void Destroy()
    {
        GameManager.Instance.currentState = States.Nothing;
        Destroy(this.gameObject);
    }
    void RaycastCheck()
    {
        if (isRaycasting && !_outline.enabled)
            RaycastOutline();
        if (!isRaycasting && !isSelected)
            CloseRaycastOutline();
        //
        isRaycasting = false;
    }
    void RaycastOutline()
    {
        _outline.OutlineColor = Color.yellow;
        _outline.enabled = true;
    }
    void CloseRaycastOutline()
    {
        _outline.enabled = false;
    }
    public void OpenItemUI()
    {
        itemUI.SetActive(true);
    }
    public void CloseItemUI()
    {
        itemUI.SetActive(false);
    }
    public void Select()
    {
        OpenItemUI();
        _outline.OutlineColor = Color.green;
        isOutline = true;
        isSelected = true;
        _outline.enabled = true;
    }
    public void Deselect()
    {
        CloseItemUI();
        isOutline = false;
        isSelected = false;
        _outline.enabled = false;
    }
}
