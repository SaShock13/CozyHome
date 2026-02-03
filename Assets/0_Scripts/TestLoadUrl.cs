using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestLoadUrl : MonoBehaviour
{
    [SerializeField] ImageLoaderService imageLoaderService;
    [SerializeField] RawImage image;
    private IEnumerator Start()
    {
        string url = "http://data.ikppbb.com/test-task-unity-data/pics/33.jpg";
        string testUrl = "https://muzrock.com/wp-content/uploads/2018/02/metal.png";


        Debug.Log($"Try to Load from Url {url}");

        //using var req = UnityWebRequestTexture.GetTexture(url);
        //yield return req.SendWebRequest();


        using var req = UnityWebRequestTexture.GetTexture(url);
        req.timeout = 10; // СЕКУНДЫ

        yield return req.SendWebRequest();

        Debug.Log($"Request is {req.result}");
        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Loaded");
            Texture texture = DownloadHandlerTexture.GetContent(req);
            SetTexture(texture);
        }
        else
        {
            Debug.Log(req.error);
        }
    }



    private void SetTexture(Texture texture)
    {
        image.texture = texture;
    }

}
