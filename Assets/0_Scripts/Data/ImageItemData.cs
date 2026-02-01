using System;
using UnityEngine;

[Serializable]
public class ImageItemData
{
    public int Id { get; private set; }
    public string ImageUrl { get; private set; }
    public string LocalName { get; private set; }
    public bool IsPremium { get; private set; }
    public Texture Texture { get; set; } = null;

    public ImageItemData(int id, string url, string localName = "0")
    {
        Id = id;
        ImageUrl = url;
        IsPremium = id % 4 == 0;
        LocalName = localName;
    }
}
