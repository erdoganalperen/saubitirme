using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainMenuManager : MonoBehaviour
{
    bool isLoaded;
    public void StartButton()
    {
        GameManager.Instance.LoadScene(Scenes.RoomSelectScene);
    }
    public void DrawRoomButton()
    {
        GameManager.Instance.LoadScene(Scenes.Planner2D);
    }

    IEnumerator RequestCoroutine(string url)
    {
        isLoaded = false;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {

        }
        isLoaded = true;
    }
}
