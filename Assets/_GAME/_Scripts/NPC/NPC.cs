using UnityEngine;

public class NPC : MonoBehaviour
{
    public PlayerController playerController;
    public Inventory inventory;
    [SerializeField] DialogueBox dialogueBox;
    [SerializeField] string dialogueText;
    private GameObject npcInventory;
    public bool interactable, interacting;
    public GameObject interactBox, dialogueBoxText;
    public float NPCGold = 50;


    private void Start()
    {
        inventory = GetComponent<Inventory>();
        interactable = true;
        playerController = GameManager.Instance.playerController;
        npcInventory = InventoryManager.Instance.NPCInventory_Panel;
        InventoryManager.Instance.goldTextNPC.text = NPCGold.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CurrentNPC = this;
            interacting = true;
            ToggleInteractButton(true, false);
            playerController.NPC = this;

            dialogueBox.npc = this;
            InventoryManager.Instance.NPCInventory = inventory;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interacting = false;
            ToggleShop(false);
            ToggleInteractButton(false, false);
            GameManager.Instance.playerController.PlayerInventoryPanel.SetActive(false);
            playerController.NPC = null;
            playerController.Interacting = false;
            InventoryManager.Instance.NPCInventory = null;
        }
    }
    public void ToggleInteractButton(bool active, bool _interacting)
    {
        interactBox.SetActive(active);
        interacting = _interacting;
    }

    public void StartDialogue()
    {
        StartCoroutine(dialogueBox.WriteDialogue(dialogueText));
    }
    public void ToggleShop(bool active)
    {
        npcInventory.SetActive(active);
    }
    public void EarnGold(float amount)
    {
        NPCGold += amount;
        InventoryManager.Instance.goldTextNPC.text = NPCGold.ToString();
    }

    public void SpendGold(float amount)
    {
        NPCGold -= amount;
        InventoryManager.Instance.goldTextNPC.text = NPCGold.ToString();
    }

}
