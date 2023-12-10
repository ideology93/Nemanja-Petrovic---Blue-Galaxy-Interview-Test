using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerEquipment : MonoBehaviour
{
    public ItemScriptableObject defaultOutfit;
    public ItemScriptableObject outfit;
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
    }
    public void EquipItem(ItemScriptableObject item, InventoryItem invItem)
    {
        outfit = item;
        inventoryItem.item = invItem.item;
        outfitImage.sprite = item.sprite;
        _animator.SetInteger("Outfit", item.OutfitID);
    }

    public void UnequipItem(ItemScriptableObject currentItem)
    {

        outfit = defaultOutfit;
        outfitImage.sprite = defaultOutfit.sprite;  // Set to a default sprite or blank if needed
        _animator.SetInteger("Outfit", 0);  // Assuming 0 represents no outfit
    }



}
