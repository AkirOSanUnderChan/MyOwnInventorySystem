using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> inventoryItems = new List<Item>();
    public Transform itemsParent;
    public ItemCell itemCellPrefab;
    public Button dropButton; // Public змінна для посилання на кнопку "Викинути предмет"

    private List<ItemCell> _itemCells = new();

    private int _selectedCellIndex = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Item item)
    {
        if (item.stackable)
        {
            foreach (Item existingItem in inventoryItems)
            {
                if (existingItem.itemName == item.itemName && existingItem.currentStack < existingItem.maxStack)
                {
                    int remainingStack = existingItem.maxStack - existingItem.currentStack;
                    int amountToAdd = Mathf.Min(item.currentStack, remainingStack);

                    existingItem.currentStack += amountToAdd;
                    item.currentStack -= amountToAdd;
                }
            }
        }

        if (item.currentStack > 0)
        {
            inventoryItems.Add(item);
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < inventoryItems.Count)
        {
            inventoryItems.RemoveAt(index);
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        // Очищуємо попередні клітинки айтемів на канвасі
        itemsParent.DestroyChildrens();

        // Створюємо нові клітинки айтемів для всіх предметів у списку inventoryItems
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            CreateItemCell(inventoryItems[i], i);
        }
    }

    private void CreateItemCell(Item item, int index)
    {
        var newItemCell = Instantiate(itemCellPrefab, itemsParent);
        newItemCell.UpdateVisual(item, index);
        _itemCells.Add(newItemCell);
        newItemCell.OnClicked += ItemCellOnClicked;
    }

    private void ItemCellOnClicked(ItemCell cell, int index)
    {
        //RemoveItem(index);
        _selectedCellIndex = index;
    }

    public void RemoveSelecteditem()
    {
        if (_selectedCellIndex != -1)
        {
            RemoveItem(_selectedCellIndex);
            _selectedCellIndex = -1;
        }
    }
}
