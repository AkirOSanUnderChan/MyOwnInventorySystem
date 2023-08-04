using System;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public sealed class ItemCell : MonoBehaviour
{
    public event Action<ItemCell, int> OnClicked;

    [SerializeField]
    private string _displayStackFormat = "Stack: {0}";

    [SerializeField]
    private Image _img;

    [SerializeField]
    private Text _txtStackCount;

    [SerializeField]
    private Button _button;

    private int _index;

    private void Awake()
    {
        _button.onClick.AddListener(ClickHandler);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ClickHandler);
    }

    public void UpdateVisual(Item item, int index)
    {
        _index = index;
        _img.sprite = item.itemImage;
        var displayCount = item.stackable ? item.currentStack.ToString() : ""; // ��������� ��������
        _txtStackCount.text = string.Format(_displayStackFormat, displayCount);
    }

    private void ClickHandler()
    {
        OnClicked?.Invoke(this, _index);
    }
}
