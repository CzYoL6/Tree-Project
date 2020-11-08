using System.IO;
using UnityEngine;
using Gif.Components;
using System.Drawing;
using System;

public class CamCapture : MonoBehaviour
{
    public static CamCapture Instance { get; private set; }
    public KeyCode screenshotKey;
    private Camera camera
    {
        get
        {
            if (!_camera)
            {
                _camera = GetComponent<Camera>();
            }
            return _camera;
        }
    }
    private Camera _camera;

    string GenerateFileName()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        return "PngCapture-" + timestamp;
    }

    private void LateUpdate()
    {
        camera.transform.position = Camera.main.transform.position;
        camera.orthographicSize = Camera.main.orthographicSize;
        /*if (Input.GetKeyDown(screenshotKey))
        {
            Capture();
        }*/

        
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        /*if (!Directory.Exists(Application.dataPath + "/Capture"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Capture");
        }*/
        //MakeGif(Application.dataPath + "/Captrue/tmp", Application.dataPath + "/Backgrounds/1.gif", true);
    }

    public void Capture(string dir)
    {
        //Debug.Log(Application.dataPath);
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Debug.Log(camera.targetTexture == null);
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        
        Destroy(image);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllBytes(dir + "/" + GenerateFileName() + ".png", bytes);
        //File.WriteAllBytes("C:/Backgrounds/" + fileCounter + ".png", bytes);
        Debug.Log("capture succeed");
    }
}