using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DotController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler,
    IPointerExitHandler
{
    public List<int> lineIndex;

    public Action<DotController> ONDragEvent;

    public Action<DotController> ONRightClickEvent;
    public Action<DotController> ONLeftClickEvent;
    public Action ONClickUpEvent;

    public Action<DotController> ONMouseEnterEvent;
    public Action ONMouseExitEvent;

    private void Awake()
    {
        lineIndex = new List<int>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            //right click
            ONRightClickEvent?.Invoke(this);
        }
        else if (eventData.pointerId == -1)
        {
            //left click
            ONLeftClickEvent?.Invoke(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ONClickUpEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (lineIndex.Count <= 0)
        {
            GetComponent<Image>().color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetActive();
    }

    public void OnDrag(PointerEventData eventData)
    {
        ONDragEvent?.Invoke(this);
    }

    public void SetActive()
    {
        if (lineIndex.Count > 0)
        {
            GetComponent<Image>().color = Color.black;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public int IndexOfLastConnection()
    {
        if (lineIndex.Count <= 0)
        {
            return -1;
        }

        int lastLineIndex = lineIndex[lineIndex.Count - 1];
        lineIndex.RemoveAt(lineIndex.Count - 1);
        return lastLineIndex;
    }

    public void DecreaseLineIndexes()
    {
        for (int i = 0; i < lineIndex.Count; i++)
        {
            lineIndex[i] = lineIndex[i] - 1;
        }
    }


}