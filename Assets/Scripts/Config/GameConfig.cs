using System.IO;
using UnityEngine;

public class GameConfig : GenericSingleton<GameConfig>
{
    public string savePath;
    public string roomMapsPath;
    public string itemsPath;
    public float screenWidth;
    public float screenHeight;

    protected override void OnAwake()
    {
        savePath = Application.persistentDataPath;
        roomMapsPath = savePath + "/rooms";
        itemsPath = "items";
        if (!Directory.Exists(roomMapsPath))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(roomMapsPath);
        }
        //
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }
}