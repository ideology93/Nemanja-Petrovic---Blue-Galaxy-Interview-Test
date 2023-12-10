using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueBox : MonoBehaviour
{
    public NPC npc;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public GameObject yesButton, noButton;

    private void Start()
    {
        dialogueBox = this.gameObject;
    }
    public IEnumerator WriteDialogue(string dialogue)
    {
        ToggleDialogue(true);
        foreach (char character in dialogue.ToCharArray())
        {
            dialogueText.text += character;
            yield return null;
        }
        ToggleAnswerButtons(true);

    }
    public void ToggleDialogue(bool active)
    {
        dialogueBox.SetActive(active);
        dialogueText.text = "";
    }
    public void ToggleAnswerButtons(bool active)
    {
        yesButton.SetActive(active);
        noButton.SetActive(active);
    }
    public void Answer(bool answer)
    {
        if (answer)
        {
            GameManager.Instance.playerController.PlayerInventoryPanel.SetActive(true);
            npc.ToggleInteractButton(false, true);
            npc.ToggleShop(true);
            dialogueBox.SetActive(false);
        }
        else
        {
            npc.ToggleInteractButton(true, false);
            npc.ToggleShop(false);
            GameManager.Instance.playerController.Interacting = false;
            dialogueBox.SetActive(false);
        }
    }

}
