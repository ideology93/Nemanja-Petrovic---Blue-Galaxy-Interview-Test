// InventoryItem.cs
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour
{
    public ItemScriptableObject item;
    public ItemDescription itemDescription;

    public void OnPointerEnter()
    {
        itemDescription.SetDescription(item.itemName, item.itemSlot.ToString(), item.armor, item.speedPenalty, item.value);
    }

    public void OnPointerExit()
    {
        itemDescription.ClearDescription();
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
    }

}