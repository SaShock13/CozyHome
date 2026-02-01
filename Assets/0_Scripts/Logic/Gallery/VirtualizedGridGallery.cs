using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualizedGridGallery : MonoBehaviour
{


    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private GalleryItemView itemPrefab;

    [SerializeField] private float planchetMinWidth = 1400f; // минимальная ширина экран для опознания планшета
    [SerializeField] private float spacing = 20f;
    [SerializeField] private float planchetPadding = 50f;
    [SerializeField] private float smartphonePadding = 100f;

    [SerializeField] private float aspectRatio = 1f;

    private GalleryDataSource _dataSource;
    private readonly List<GalleryItemView> _views = new();
    private int _columns;
    private float _itemWidth;
    private float _itemHeight;
    private int _lastFirstRow = -1;
    private float _scrollThreshold = 2;  // порог срабатыватия скролла от колебаний
    private Vector2 _lastScrollPos = Vector2.zero;


    float diagonalInches = 0;
    float dpi = 0;

    public void Initialize(GalleryDataSource dataSource)
    {
        _dataSource = dataSource;

        CalculateLayout();
        ResizeContent();
        CreatePool();
        UpdateVisibleItems(true);

        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    private void CalculateLayout()
    {
        bool isTablet = false;
        float viewportWidth = 0;       

        isTablet = DeviceInformationService.IsTablet(out diagonalInches, out dpi); // Планшет или нет?

        if(diagonalInches == 0 ) // Fallback вариант , если по диагонали не сработало.
        {
            viewportWidth = scrollRect.viewport.rect.width;
            isTablet = viewportWidth > planchetMinWidth;
        }

        _columns = isTablet ? 3 : 2; // Определяем количество колонок 
        float padding = _columns == 3 ? planchetPadding : smartphonePadding;

        scrollRect.viewport.offsetMin = new Vector2(60, padding);    //Выставляем паддинги  Left, Bottom
        scrollRect.viewport.offsetMax = new Vector2(-60, -160);    //Выставляем паддинги   -Right, -Top

        viewportWidth = scrollRect.viewport.rect.width -20 ; // пересчитываем ширину с новыми паддингами

        _itemWidth = (viewportWidth - spacing * (_columns - 1)) / _columns;
        _itemHeight = _itemWidth / aspectRatio;
    }

    private void ResizeContent()
    {
        int rows = Mathf.CeilToInt((float)_dataSource.Items.Count / _columns);
        float height = rows * (_itemHeight + spacing) - spacing;
        content.sizeDelta = new Vector2(content.sizeDelta.x, height);
    }

    private void CreatePool()
    {
        float viewportHeight = scrollRect.viewport.rect.height;
        int visibleRows = Mathf.CeilToInt(viewportHeight / (_itemHeight + spacing)) + 2;
        int poolSize = visibleRows * _columns;

        for (int i = 0; i < poolSize; i++)
        {
            var view = Instantiate(itemPrefab, content);
            var rt = view.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(_itemWidth, _itemHeight);
            _views.Add(view);
        }
    }

    private void OnScroll(Vector2 pos)
    {
        //насколько сильно сдвинулся скролл
        float delta = Mathf.Abs(scrollRect.content.anchoredPosition.y - _lastScrollPos.y);
        if (delta < _scrollThreshold)
            return; // игноририм колебания

        _lastScrollPos = scrollRect.content.anchoredPosition;
        UpdateVisibleItems(false);
    }


    private void UpdateVisibleItems(bool force)
    {
        if (_dataSource == null || _dataSource.Items == null) return;

        float scrollY = content.anchoredPosition.y;
        int firstRow = Mathf.FloorToInt(scrollY / (_itemHeight + spacing));
        firstRow = Mathf.Max(0, firstRow);

        if (!force && firstRow == _lastFirstRow) return;

        _lastFirstRow = firstRow;

        for (int i = 0; i < _views.Count; i++)
        {
            var view = _views[i];
            int dataIndex = firstRow * _columns + i;

            if (dataIndex >= _dataSource.Items.Count)
            {
                view.Unbind();
                view.gameObject.SetActive(false);
                continue;
            }

            view.gameObject.SetActive(true);
            int row = dataIndex / _columns;
            int col = dataIndex % _columns;
            view.RectTransform.anchoredPosition = new Vector2(col * (_itemWidth + spacing), -row * (_itemHeight + spacing));

            if (force || view.BoundIndex != dataIndex)
            {
                view.Bind(_dataSource.Items[dataIndex], dataIndex);
            }

        }
    }

    public void Refresh()
    {
        _lastFirstRow = -1;
        scrollRect.StopMovement();
        content.anchoredPosition = Vector2.zero;
        foreach (var view in _views) view.Unbind();
        ResizeContent();
        UpdateVisibleItems(true);
    }
}
