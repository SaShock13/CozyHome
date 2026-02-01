using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoaderService : MonoBehaviour
{
    // Асинхронная загрузка по URL
    public static IEnumerator Load(string url, Action<Texture> onLoaded)
    {
        using var req = UnityWebRequestTexture.GetTexture(url);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
            onLoaded?.Invoke(DownloadHandlerTexture.GetContent(req));
        else
            onLoaded?.Invoke(null);
    }

    // Синхронно
    public static Texture LoadResource(string name)
    {
        var tex = Resources.Load<Texture>(name);

        if (tex == null)
        {
            Debug.LogError($"Texture not found: '{name}'");
        }

        return tex;
    }

    // Асинхронная версия с колбэком
    public static IEnumerator LoadResourceAsync(string name, Action<Texture> onLoaded)
    {
        Debug.Log($"Try async load: '{name}'");
               

        // Асинхронная загрузка
        ResourceRequest request = Resources.LoadAsync<Texture>(name);
        yield return request;

        if (request.asset != null)
        {
            Texture texture = request.asset as Texture;
            Debug.Log($"Success: '{texture.name}'");
            onLoaded?.Invoke(texture);
        }
        else
        {
            Debug.LogError($"Texture not found: '{name}'");
            onLoaded?.Invoke(null);
        }
    }
}
