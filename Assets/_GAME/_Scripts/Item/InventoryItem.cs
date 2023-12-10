using System.Collections;
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
        if (!hasBeenClicked)
        {
            AudioManager.Instance.PlaySound(0);
            if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left)
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
                else
                {
                    EquipOrUnequipItem();
                }
            }
        }
        hasBeenClicked = true;
        StartCoroutine(ResetClick());
    }
    private IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.2f);
        hasBeenClicked = false;
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
        Inventory npcInventory = InventoryManager.Instance.NPCInventory;
        if (npcInventory != null)
        {
            if (item.OutfitID == 0 || IsEquipped()) return;
            ownerInventory.RemoveFromInventory(item);
            npcInventory.AddToInventory(item);
            Debug.Log("Item Sold" + item);
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
            EquipItem();
        }
        else if (playerEquipment.outfit != item)
        {
            UnequipItem();
            EquipItem();
        }
        else UnequipItem();
    }

    private void EquipItem()
    {
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;
        if (item.OutfitID == 0)
        {
            playerEquipment.EquipItem(item, this);
        }
        else
        {
            if (playerEquipment.outfit != null)
            {
                UnequipCurrentOutfit(playerEquipment.outfit);
            }
            Debug.Log("Equipping the item : " + item.name);
            playerEquipment.EquipItem(item, this);
            ownerInventory.RemoveFromInventory(item);
        }
    }

    private void UnequipItem()
    {

        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        if (item.OutfitID == 0)
        {
            Debug.Log("Default outfit so not unequipping");
        }
        else
        {
            Debug.Log("Unequipping the item : " + playerEquipment.outfit.name);
            ownerInventory.AddToInventory(playerEquipment.outfit);
            UnequipCurrentOutfit(playerEquipment.outfit);

        }
    }

    private void UnequipCurrentOutfit(ItemScriptableObject currentItem)
    {
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;
        playerEquipment.UnequipItem(currentItem);
    }
    private bool IsEquipped()
    {
        PlayerEquipment playerEquipment = GameManager.Instance.playerController.playerEquipment;

        // Check if the item is the currently equipped outfit
        return playerEquipment.outfit == item;
    }
}