using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> inventoryItems = new List<Item>();
    public Transform itemsParent;
    public GameObject itemCellPrefab;

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
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            CreateItemCell(inventoryItems[i], i);
        }
    }

    private void CreateItemCell(Item item, int index)
    {
        GameObject newItemCell = Instantiate(itemCellPrefab, itemsParent);

        Image itemImage = newItemCell.transform.Find("ItemIMG").GetComponent<Image>();
        itemImage.sprite = item.itemImage;

        TextMeshProUGUI itemStackText = newItemCell.GetComponentInChildren<TextMeshProUGUI>();
        itemStackText.text = item.stackable ? item.currentStack.ToString() : "";

        Button dropButton = newItemCell.transform.Find("Drop").GetComponent<Button>();
        dropButton.gameObject.SetActive(false);

        // Підписуємося на події onMouseEnterAction та onMouseExitAction айтема
        ItemPickup itemPickup = newItemCell.GetComponent<ItemPickup>();
        itemPickup.onMouseEnterAction += () =>
        {
            dropButton.gameObject.SetActive(true);
        };

        itemPickup.onMouseExitAction += () =>
        {
            dropButton.gameObject.SetActive(false);
        };

        dropButton.onClick.AddListener(() => OnDropButtonClick(index));
    }

    private void OnDropButtonClick(int index)
    {
        RemoveItem(index);
    }
}
