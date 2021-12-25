using System.IO;
using UnityEngine;

public class RoomSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject roomButton;
    [SerializeField] private RectTransform roomsParent;
    [SerializeField] private GameObject loadingPanel;
    private void Start()
    {
        CreateRoomListUI();
    }

    void CreateRoomListUI()
    {
        if (GameManager.Instance.userId == "")
        {
            return;
        }
        DirectoryInfo dir = new DirectoryInfo(GameConfig.Instance.roomMapsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            var button = Instantiate(roomButton, roomsParent);
            var roomButtonController = button.AddComponent<RoomButtonController>();
            roomButtonController.InitializeButton(f.Name);
        }
        roomsParent.localPosition += new Vector3(0, roomsParent.rect.yMin, 0);
    }
    public void BackButton()
    {
        GameManager.Instance.LoadScene(Scenes.MainMenu);
    }
    public void CreateNewRoom()
    {
        GameManager.Instance.LoadScene(Scenes.Planner2D);
    }
}
