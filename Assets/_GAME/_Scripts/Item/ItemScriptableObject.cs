using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemScriptableObject : ScriptableObject

{
    public string itemName;
    public Sprite sprite;
    public ItemType itemType;
    public int value;
    public int armor;
    public float speedPenalty;
    public ItemSlot itemSlot;
    public int OutfitID;
}
public enum ItemType
{
    Equippable,
    Consumable
}
public enum ItemSlot
{
    None,
    Armor
}


