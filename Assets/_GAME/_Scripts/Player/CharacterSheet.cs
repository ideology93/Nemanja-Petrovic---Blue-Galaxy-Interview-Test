using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    public CharacterScriptableObject playerCharacter;

    void Start()
    {
        // Access character stats
        Debug.Log($"Character Name: {playerCharacter.characterName}");


        // Access equipped items
        Debug.Log($"Equipped Body Armor: {playerCharacter.bodyArmorSlot.equippedItem?.name}");


        // Access character info
        Debug.Log($"Character Type: {playerCharacter.characterInfo.characterType}");
        Debug.Log($"Character Hostility: {playerCharacter.characterInfo.characterHostility}");
    }
}
