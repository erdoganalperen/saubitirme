using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;

public class LineController : MonoBehaviour
{
    private LineRenderer _lr;
    private List<DotController> _dots;
    private int _index;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = 0;

        _dots = new List<DotController>();
    }

    private DotController _lastAddedDot;

    public void AddPoint(DotController dot)
    {
        if (_lastAddedDot == null)
        {
            _lastAddedDot = dot;
        }
        else
        {
            if (dot == _lastAddedDot)
            {
                Debug.Log("dot can't go itself");
                return;
            }

            _lastAddedDot = dot;
        }

        _lr.positionCount++;
        _dots.Add(dot);
        dot.lineIndex.Add(_index);
        dot.SetActive();

        _index++;
    }

    public void RemovePoint(DotController dot)
    {
        if (dot.lineIndex.Count <= 0)
        {
            return;
        }

        List<DotController> dotsRemovedFromIndex = new List<DotController>();
        var deletedIndex = dot.IndexOfLastConnection();

        int i = 0;
        for (; i < deletedIndex; i++)
        {
            dotsRemovedFromIndex.Add(_dots[i]);
        }

        i++;
        for (; i < _dots.Count; i++)
        {
            _dots[i].DecreaseLineIndexes();
            dotsRemovedFromIndex.Add(_dots[i]);
        }

        _dots = dotsRemovedFromIndex;
        _lr.positionCount = _dots.Count;
        dot.SetActive();
        _index--;
        if (deletedIndex == _lr.positionCount)
        {
            if (_dots.Count > 0)
            {
                _lastAddedDot = _dots[_dots.Count - 1];
            }
        }
    }

    private void LateUpdate()
    {
        if (_dots.Count >= 2)
        {
            for (int i = 0; i < _dots.Count; i++)
            {
                _lr.SetPosition(i, _dots[i].transform.position);
            }
        }
    }

    public void SavePosList(string roomName)
    {
        var unit = DotManager.Instance.unit;
        List<RoomMapPosition> positionList = new List<RoomMapPosition>();
        for (int i = 0; i < _lr.positionCount - 1; i++)
        {
            var diff = _lr.GetPosition(i + 1) - _lr.GetPosition(i);
            var position = new RoomMapPosition(Mathf.Round(diff.x / unit), 0, Mathf.Round(diff.y / unit));
            positionList.Add(position);
        }
        //
        FileHelper.SaveToJson(positionList, $"rooms/{roomName}");
    }
}