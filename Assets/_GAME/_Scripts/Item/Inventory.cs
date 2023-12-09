// InventoryManager.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<ItemScriptableObject> inventory = new List<ItemScriptableObject>();
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;
    public ItemDescription itemDescription;

    void Start()
    {
        // Initialize inventory and UI
        UpdateInventoryUI();
    }

    public void AddToInventory(ItemScriptableObject item)
    {
        if (!inventory.Contains(item))
        {
            // Add the item to the inventory
            inventory.Add(item);

        }

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        // Clear existing UI
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Create UI for each inventory item
        foreach (ItemScriptableObject item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            inventorySlotPrefab.GetComponent<InventoryItem>().itemDescription = itemDescription;
            Image iconImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
            slot.GetComponent<InventoryItem>().item = item;
            iconImage.sprite = item.sprite;
        }
    }
}