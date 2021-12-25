using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JavascriptHook : MonoBehaviour
{
    public Text userIdText;
    public Image img;
    public Texture2D tex;
    public Sprite sprite;
    private void Awake()
    {
        tex = new Texture2D(2, 2);
        string path, origin;
#if UNITY_WEBGL
        origin = Application.streamingAssetsPath.Substring(0, Application.streamingAssetsPath.Length - 30);
#endif
#if UNITY_EDITOR
        // path = Application.dataPath + "/Sprites/kanepe.jpg";
        path = "http://127.0.0.1:8887/kanepe.jpg";
#endif
        // StartCoroutine(DownloadImage(path));
    }

    public void SetUser(string userId)
    {
        GameManager.Instance.userId = userId;
        userIdText.text = userId;
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(.5f, .5f));
            img.overrideSprite = sprite;
            print(tex);
        }
    }
}
