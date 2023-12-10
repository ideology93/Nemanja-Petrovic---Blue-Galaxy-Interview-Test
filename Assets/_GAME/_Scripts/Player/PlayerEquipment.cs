using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerEquipment : MonoBehaviour
{
    public ItemDescription itemDescription;
    public ItemScriptableObject defaultOutfit;
    public ItemScriptableObject outfit;
    private PlayerController playerController;
    public InventoryItem inventoryItem;
    public Image outfitImage;
    public Sprite noOutfit;
    [SerializeField] Animator _animator;
    public GameObject weapon;

    private void Start()
    {

        // _animator = GetComponent<Animator>();
        if (outfit.OutfitID == 0)
        {
            outfitImage.sprite = noOutfit;
        }
        else
            outfitImage.sprite = outfit.sprite;
        inventoryItem.item = outfit;
        playerController = GetComponent<PlayerController>();
        itemDescription.descriptionText.text = "Armor: " + defaultOutfit.armor.ToString();
        itemDescription.valueText.text = "Speed: " + playerController.MoveSpeed.ToString();

    }
    public void EquipItem(ItemScriptableObject item, InventoryItem invItem)
    {
        outfit = item;
        inventoryItem.item = invItem.item;
        outfitImage.sprite = item.sprite;

        _animator.SetInteger("Outfit", item.OutfitID);
        playerController.MoveSpeed = playerController.MoveSpeed / outfit.speedPenalty;
        UpdateStats(item);
    }

    public void UnequipItem(ItemScriptableObject currentItem)
    {

        outfit = defaultOutfit;
        outfitImage.sprite = defaultOutfit.sprite;  // Set to a default sprite or blank if needed
        _animator.SetInteger("Outfit", 0);  // Assuming 0 represents no outfit
        playerController.MoveSpeed = playerController.StartMoveSpeed;
        UpdateStats(currentItem);

    }
    public void UpdateStats(ItemScriptableObject _outfit)
    {
        itemDescription.descriptionText.text = "Armor: " + _outfit.armor;
        float newSpeed = (float)playerController.StartMoveSpeed / outfit.speedPenalty;
        playerController.MoveSpeed = newSpeed;
        Debug.Log("new speed " + newSpeed);
        itemDescription.valueText.text = "Speed: " + newSpeed.ToString("F2");
    }



}
