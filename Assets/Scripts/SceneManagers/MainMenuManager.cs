using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartButton()
    {
        GameManager.Instance.LoadScene(Scenes.RoomSelectScene);
    }
    public void DrawRoomButton()
    {
        GameManager.Instance.LoadScene(Scenes.Planner2D);
    }

}
