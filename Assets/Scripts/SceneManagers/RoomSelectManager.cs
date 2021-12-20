using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject roomButton;
    [SerializeField] private RectTransform roomsParent;

    private void Start()
    {
        CreateRoomListUI();
    }

    void CreateRoomListUI()
    {
        DirectoryInfo dir = new DirectoryInfo(GameConfig.Instance.roomMapsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            var button = Instantiate(roomButton, roomsParent);
            var roomButtonController=button.AddComponent<RoomButtonController>();
            roomButtonController.InitializeButton(f.Name);
        }
        roomsParent.localPosition += new Vector3(0,roomsParent.rect.yMin,0);
    }
}
