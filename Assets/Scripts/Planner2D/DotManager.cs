using UnityEngine;
using UnityEngine.UI;

public class DotManager : GenericSingleton<DotManager>
{
    private Camera _cam;
    [Header("Line")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform lineParent;
    private LineController _currentLine;
    [Header("Dot")]
    public Transform dotParent;
    public GameObject dotPrefab;
    [Tooltip("Size of height")]
    public int dotMapSize;
    [HideInInspector]public float unit;
    public InputField roomNameText;
    private void Start()
    {
        _cam = Camera.main;
        //
        CreateDotMap();
    }
    
    void CreateDotMap()
    {
        float height = 2f * _cam.orthographicSize;
        float width = height * _cam.aspect;

        float heightWithEdgeOffset = height * 6 / 10;
        float widthWithEdgeOffset = width * 6 / 10;

        float heightUnit = heightWithEdgeOffset / (dotMapSize-1);
        float widthUnit = heightUnit;
        unit = heightUnit;
        var startPoint = new Vector3(-widthWithEdgeOffset / 2, -heightWithEdgeOffset / 2,0);
        for (int i = 0; i < dotMapSize; i++)
        {
            for (int j = 0; j < widthWithEdgeOffset/widthUnit; j++)
            {
                DotController dot = Instantiate(dotPrefab, startPoint, Quaternion.identity, dotParent)
                    .GetComponent<DotController>();
                
                dot.ONLeftClickEvent += AddPoint;
                dot.ONRightClickEvent += RemovePoint;
                startPoint.x += widthUnit;
            }

            startPoint.x = -widthWithEdgeOffset / 2;
            startPoint.y += heightUnit;
        }
    }
    
    private void AddPoint(DotController dot)
    {
        if (_currentLine==null)
        {
            _currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
        }
        _currentLine.AddPoint(dot);
    }

    void RemovePoint(DotController dot)
    {
        _currentLine.RemovePoint(dot);
    }

    public void SavePosList()
    {
        _currentLine.GetComponent<LineController>().SavePosList(roomNameText.text);
    }

    public void BackButton()
    {
        GameManager.Instance.LoadScene(Scenes.MainMenu);
    }
}