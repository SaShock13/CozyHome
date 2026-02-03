using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public  class ImageLoaderService : MonoBehaviour
{

    static string testUrl = "https://muzrock.com/wp-content/uploads/2018/02/metal.png";

    // Асинхронно по url

    //public static IEnumerator LoadFromNet(string url, Action<Texture> onLoaded)
    //{

    //    //Debug.Log($"Try to Load from Url {url}");
    //    using var req = UnityWebRequestTexture.GetTexture(testUrl);
    //    yield return req.SendWebRequest();

    //    Debug.Log($"Request is  {req.result}");
    //    if (req.result == UnityWebRequest.Result.Success)
    //        onLoaded?.Invoke(DownloadHandlerTexture.GetContent(req));
    //    else
    //        onLoaded?.Invoke(null);
    //}


    public static IEnumerator LoadFromNet(string url, Action<Texture> onLoaded)
    {

        Debug.Log($"Try to Load from Url {url}");
        using var req = UnityWebRequestTexture.GetTexture(url);
        yield return req.SendWebRequest();

        Debug.Log($"Request is  {req.result}");
        if (req.result == UnityWebRequest.Result.Success)
            onLoaded?.Invoke(DownloadHandlerTexture.GetContent(req));
        else
            onLoaded?.Invoke(null);
    }

    // Синхронно
    public Texture LoadResource(string name)
    {
        var tex = Resources.Load<Texture>(name);

        if (tex == null)
        {
            Debug.LogError($"Texture not found: '{name}'");
        }

        return tex;
    }

    // Асинхронно
    public IEnumerator LoadResourceAsync(string name, Action<Texture> onLoaded)
    {
        // Асинхронная загрузка
        ResourceRequest request = Resources.LoadAsync<Texture>(name);
        yield return request;

        if (request.asset != null)
        {
            Texture texture = request.asset as Texture;
            onLoaded?.Invoke(texture);
        }
        else
        {
            Debug.LogError($"Texture not found: '{name}'");
            onLoaded?.Invoke(null);
        }
    }
}
