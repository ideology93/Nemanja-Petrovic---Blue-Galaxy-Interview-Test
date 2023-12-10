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
    public void RemoveFromInventory(ItemScriptableObject item)
    {
        if (inventory.Contains(item))
        {
            // Remove the item from the inventory
            itemDescription.ClearDescription();
            inventory.Remove(item);
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
        //Regenerate UI
        foreach (ItemScriptableObject item in inventory)
        {

            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            InventoryItem inventoryItem = slot.GetComponent<InventoryItem>();
            inventoryItem.ownerInventory = this;
            inventoryItem.item = item;
            inventoryItem.itemDescription = itemDescription;
            Image iconImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
            iconImage.sprite = item.sprite;
        }
    }
}