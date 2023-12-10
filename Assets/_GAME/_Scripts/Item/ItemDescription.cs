using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDescription : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI valueText;
    public Image goldImage;
    public void ClearDescription()
    {
        nameText.text = "";
        descriptionText.text = "";
        valueText.text = "";
        goldImage.enabled = false;
    }
    public void SetDescription(string name, string itemSlot, int armor, float speedPenalty, int value)
    {
        nameText.text = name + " - " + itemSlot;
        descriptionText.text = "Armor: " + armor + ", Speed Penalty: " + speedPenalty;
        valueText.text = value + "";
        goldImage.enabled = true;
    }

}
