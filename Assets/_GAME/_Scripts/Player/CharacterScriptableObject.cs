using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    public string characterName;
    public CharacterInfo characterInfo;
    public ItemScriptableObject outfit;
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
