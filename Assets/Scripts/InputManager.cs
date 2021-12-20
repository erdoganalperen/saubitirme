using UnityEngine;
using UnityEngine.EventSystems;
public class InputManager : GenericSingleton<InputManager>
{
    [Header("Read Values")]
    [SerializeField] private LayerMask BlueprintLayerMask; //<-LAYERS BE IGNORED
    [ReadOnlyInInspector] [SerializeField] private bool isKeyPressed;
    [ReadOnlyInInspector] [SerializeField] private bool _isMouseOverUI;
    [ReadOnlyInInspector] [SerializeField] private bool _leftClick;
    [ReadOnlyInInspector] [SerializeField] private bool _rightClick;
    [ReadOnlyInInspector] [SerializeField] private KeyCode pressedKey;
    [ReadOnlyInInspector] [SerializeField] private Vector2 _mousePosition;
    [ReadOnlyInInspector] [SerializeField] private float _mouseWheel;
    [ReadOnlyInInspector] [SerializeField] private Vector2 _mousePositionDelta;
    [ReadOnlyInInspector] [SerializeField] private Vector3 _mouseRayHitPosition;
    // Properties
    public Vector3 CameraRaycastHitPosition => _mouseRayHitPosition;
    public bool IsMouseOverUI => _isMouseOverUI;
    public bool LeftClick => _leftClick;
    public bool RightClick => _rightClick;
    public Vector2 MousePosition => _mousePosition;
    public float MouseScrollDeltaY => _mouseWheel;
    public Vector2 MousePositionDelta => _mousePositionDelta;
    //
    private Vector2 _previousMousePosition;
    public bool lockFlow = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            lockFlow = !lockFlow;
        GetMouseClick();
        GetMousePosition();
        DetectMouseScroll();
        DetectKeyPress();
        RaycastHit();
        GetIsMouseOverUI();
    }
    void GetIsMouseOverUI()
    {
        _isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }
    void GetMouseClick()
    {
        if (IsMouseOverUI)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            _leftClick = true;
        }
        else
        {
            _leftClick = false;
        }
        if (Input.GetMouseButton(1))
        {
            _rightClick = true;
        }
        else
        {
            _rightClick = false;
        }
    }
    void DetectKeyPress()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                isKeyPressed = true;
                pressedKey = key;
            }
        }
        if (!Input.GetKey(pressedKey))
        {
            isKeyPressed = false;
        }
    }
    void DetectMouseScroll()
    {
        _mouseWheel = Input.mouseScrollDelta.y;

    }
    void GetMousePosition()
    {
        _mousePosition = Input.mousePosition;
        _mousePositionDelta = _mousePosition - _previousMousePosition;
        _previousMousePosition = _mousePosition;
    }

    public GameObject hitItem;
    void RaycastHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, BlueprintLayerMask))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                _mouseRayHitPosition = hit.point;
                hitItem = null;
            }
            if (hit.collider.CompareTag("Item"))
            {
                hitItem = hit.collider.gameObject;
            }
        }
    }
}