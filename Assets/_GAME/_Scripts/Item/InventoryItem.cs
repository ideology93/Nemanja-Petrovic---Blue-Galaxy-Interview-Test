// InventoryItem.cs
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public bool isEquipped;
    public ItemScriptableObject item;
    public ItemDescription itemDescription;
    public Inventory ownerInventory;
    private bool hasBeenClicked;

    public void OnPointerEnter()
    {
        itemDescription.SetDescription(item.itemName, item.itemSlot.ToString(), item.armor, item.speedPenalty, item.value);
        hasBeenClicked = false;
    }

    public void OnPointerExit()
    {
        itemDescription.ClearDescription();
        hasBeenClicked = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !hasBeenClicked)
        {
            if (GameManager.Instance.playerController.Interacting)
            {
                if (ownerInventory.CompareTag("Player"))
                {
                    SellItem();
                }
                else
                {
                    BuyItem();
                }
            }
            else EquipOrUnequipItem();
        }
        hasBeenClicked = true;
    }
    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) =>
        {
            OnPointerEnter();
        });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryExit);

        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) =>
        {
            OnPointerClick((PointerEventData)data);
        });
        trigger.triggers.Add(entryClick);
    }
    private void BuyItem()
    {
        if (GameManager.Instance.playerController.PlayerGold < item.value) return;
        Inventory playerInventory = InventoryManager.Instance.PlayerInventory;
        ownerInventory.RemoveFromInventory(item);
        playerInventory.AddToInventory(item);
        GameManager.Instance.playerController.SpendGold(item.value);
        Debug.Log("Item Purchased");
    }

    private void SellItem()
    {
        Debug.Log("Selling");
        Inventory npcInventory = InventoryManager.Instance.NPCInventory;
        Debug.Log("NPC Inventory " + npcInventory.name);
        if (npcInventory != null)
        {
            if (item.OutfitID == 0 || IsEquipped()) return;
            Debug.Log("Item Sold");
            ownerInventory.RemoveFromInventory(item);
            npcInventory.AddToInventory(item);
            GameManager.Instance.playerController.EarnGold(item.value);
        }
        else
        {
            Debug.LogError("NPC's inventory not found.");
        }
    }

    private void EquipOrUnequipItem()
    {
        // Assuming you have a reference to the player's equipment
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        if (playerEquipment.outfit.OutfitID == 0)
        {
            // Equip the item
            EquipItem();
        }
        else
        {
            // Unequip the item
            UnequipItem();
        }
    }

    private void EquipItem()
    {

        Debug.Log("Clicking item");
        // Assuming you have a reference to the player's equipment
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        // Check if the item is the default outfit
        if (item.OutfitID == 0)
        {
            // Equip the default outfit without adding it to the inventory
            playerEquipment.EquipItem(item, this);
        }
        else
        {
            // Check if the player already has an outfit equipped
            if (playerEquipment.outfit != null)
            {
                // Unequip the current outfit
                UnequipCurrentOutfit(playerEquipment.outfit);
            }

            // Equip the new outfit
            playerEquipment.EquipItem(item, this);
            // Remove the item from the player's inventory
            ownerInventory.RemoveFromInventory(item);
        }
    }

    private void UnequipItem()
    {
        // Assuming you have a reference to the player's equipment
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        // Check if the item is the default outfit
        if (item.OutfitID == 0)
        {
            Debug.Log("Default outfit so not unequipping");
            // Unequipping the default outfit, do nothing
        }
        else
        {
            Debug.Log("Unequipping the item");
            // Unequip the item
            UnequipCurrentOutfit(item);
            // Add the unequipped item back to the player's inventory
            Debug.Log("Owner" + ownerInventory.name);
            ownerInventory.AddToInventory(item);
        }
    }

    private void UnequipCurrentOutfit(ItemScriptableObject currentItem)
    {
        // Assuming you have a reference to the player's equipment
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        // Unequip the current outfit
        playerEquipment.UnequipItem(currentItem);
    }
    private bool IsEquipped()
    {
        // Assuming you have a reference to the player's equipment
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        // Check if the item is the currently equipped outfit
        return playerEquipment.outfit == item;
    }
}