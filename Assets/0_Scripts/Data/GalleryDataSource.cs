using System.Collections.Generic;
using System.Linq;

public class GalleryDataSource
{
    public List<ImageItemData> Items { get; private set; }
    private List<ImageItemData> _allItems;

    public GalleryDataSource(List<ImageItemData> items)
    {
        _allItems = items;
        Items = new List<ImageItemData>(_allItems);
    }

    public void ApplyFilter(GalleryFilterType filter)
    {
        switch (filter)
        {
            case GalleryFilterType.All:
                Items = new List<ImageItemData>(_allItems);
                break;
            case GalleryFilterType.Odd:
                Items = _allItems.Where(i => i.Id % 2 == 1).ToList();
                break;
            case GalleryFilterType.Even:
                Items = _allItems.Where(i => i.Id % 2 == 0).ToList();
                break;
        }
    }
}
