using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomButtonController : MonoBehaviour, IPointerClickHandler
{
    private string _roomName;

    public void InitializeButton(string rn)
    {
        _roomName = rn;
        var textComponent = GetComponentInChildren<Text>();
        textComponent.text = rn;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var posList = FileHelper.ReadListFromJson<RoomMapPosition>("rooms/" + _roomName);
        GameManager.Instance.SetPosList(posList);
        GameManager.Instance.LoadScene(Scenes.GameScene);
    }
}