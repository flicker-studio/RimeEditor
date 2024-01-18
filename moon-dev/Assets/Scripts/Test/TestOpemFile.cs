using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

public class TestOpemFile : MonoBehaviour
{
    string path;
    public RawImage image;

    public void Start()
    {
        OpenExplorer(); 
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        GetImage();
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
        image.texture = DownloadHandlerTexture.GetContent(uwr);
    }
}