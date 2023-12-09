using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    public string characterName;

    public EquipmentSlot bodyArmorSlot;

    public CharacterInfo characterInfo;
}

[System.Serializable]
public class EquipmentSlot
{
    public ItemScriptableObject equippedItem;
}

[System.Serializable]
public class CharacterInfo
{
    public CharacterType characterType;
    public CharacterHostility characterHostility;
}

public enum CharacterType
{
    Player,
    NPC
}

public enum CharacterHostility
{
    Friendly,
    Hostile,
    Neutral
}
