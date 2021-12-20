using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemManager : GenericSingleton<ItemManager>
{
    private Object[] _items;
    GameObject selectedBlueprint;
    GameObject selectedItem;
    public bool isItemMoving;
    public GameObject itemUI;
    private InputManager _inputManager;
    GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _items = Resources.LoadAll(GameConfig.Instance.itemsPath, typeof(GameObject));
        GameSceneUIManager.Instance.CreateItemButtons(_items);
    }
    public void CreateItemBlueprint(string name)
    {
        if (selectedBlueprint != null)
        {
            return;
        }
        _gameManager.currentState = States.Blueprint;
        var item = _items.FirstOrDefault(x => x.name == name) as GameObject;
        selectedBlueprint = Instantiate(item, Vector3.zero, Quaternion.identity);
        var itemController = selectedBlueprint.GetComponent<ItemController>();//.itemName = name;
        itemController.itemName = name;
        itemController.itemUI = Instantiate(itemUI, selectedBlueprint.transform);
        itemController.CloseItemUI();
    }
    public void BuildItem()
    {
        if (selectedBlueprint == null)
            return;
        _gameManager.currentState = States.Nothing;
        selectedBlueprint.layer = LayerMask.NameToLayer("Item");
        selectedBlueprint = null;
    }
    void MoveBlueprint()
    {
        if (selectedBlueprint != null)
        {
            selectedBlueprint.transform.position = _inputManager.CameraRaycastHitPosition;
        }
    }
    void MoveItem()
    {
        if (selectedItem != null && isItemMoving)
        {
            selectedItem.transform.position = _inputManager.CameraRaycastHitPosition;
        }
    }
    public void PlaceItem()
    {
        selectedItem.GetComponent<ItemController>().Place();
    }
    public void SelectItem()
    {
        DeselectItem();
        if (_inputManager.hitItem == null)
        {
            return;
        }
        _gameManager.currentState = States.ItemSelected;
        selectedItem = _inputManager.hitItem;
        selectedItem.GetComponent<ItemController>().Select();
    }
    public void DeselectItem()
    {
        if (selectedItem != null)
        {
            _gameManager.currentState = States.Nothing;
            selectedItem.GetComponent<ItemController>().Deselect();
            selectedItem = null;
        }
    }
    ItemController raycastTargetItemController;
    void OutlineRaycast()
    {
        if (_inputManager.hitItem == null)
            return;
        raycastTargetItemController = _inputManager.hitItem.GetComponent<ItemController>();
        if (raycastTargetItemController == null)
            return;
        raycastTargetItemController.isRaycasting = true;
    }
    private void Update()
    {
        MoveItem();
        OutlineRaycast();
        MoveBlueprint();
    }
}