// InventoryItem.cs
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public ItemScriptableObject item;
    public ItemDescription itemDescription;
    public Inventory ownerInventory;
    private bool hasBeenClicked;

    public void OnPointerEnter()
    {
        itemDescription.SetDescription(item.itemName, item.itemSlot.ToString(), item.armor, item.speedPenalty, item.value);
    }

    public void OnPointerExit()
    {
        itemDescription.ClearDescription();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !hasBeenClicked)
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
        Inventory npcInventory = InventoryManager.Instance.NPCInventory;
        if (npcInventory != null)
        {
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

}