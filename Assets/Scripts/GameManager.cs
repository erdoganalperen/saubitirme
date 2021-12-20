using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MainMenu,
    Planner2D,
    RoomSelectScene,
    GameScene
}
public enum States
{
    Nothing,
    Blueprint,
    ItemSelected,
    ItemMoving
}
public class GameManager : GenericSingleton<GameManager>
{
    public Scenes activeScene;
    public States currentState;
    protected override void OnAwake()
    {
        currentState = States.Nothing;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadScene(Scenes.MainMenu);
        }
    }
    public string userId;
    private void Start()
    {
        userId = "";
    }
    private List<RoomMapPosition> _posList;
    public void SetPosList(List<RoomMapPosition> list)
    {
        _posList = list;
    }
    public List<Vector3> GetPosList()
    {
        if (_posList == null)
        {
            return null;
        }
        List<Vector3> vector3PosList = new List<Vector3>();
        foreach (var pos in _posList)
        {
            Vector3 v3 = new Vector3(pos.x, pos.y, pos.z);
            vector3PosList.Add(v3);
        }

        return vector3PosList;
    }
    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
        activeScene = scene;
    }
}