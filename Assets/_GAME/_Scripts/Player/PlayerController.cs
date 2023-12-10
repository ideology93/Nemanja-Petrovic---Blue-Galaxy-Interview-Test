using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MoveSpeed = 0f;
    public float StartMoveSpeed = 5f;
    public GameObject PlayerInventoryPanel;
    public bool IsInShop;
    public CharacterScriptableObject characterScriptableObject;
    public Inventory playerInventory;
    [SerializeField] Animator _animator;
    public bool Interacting;
    public NPC NPC;
    public float PlayerGold = 200;
    public PlayerEquipment playerEquipment;

    private void Start()
    {
        MoveSpeed = StartMoveSpeed;
        playerInventory = GetComponent<Inventory>();
        playerEquipment = GetComponent<PlayerEquipment>();
        // playerEquipment.outfit = currentOutfit;
        // playerEquipment.outfitImage.sprite = currentOutfit.sprite;
        PlayerInventoryPanel = InventoryManager.Instance.PlayerInventory_Panel;
        InventoryManager.Instance.PlayerInventory = playerInventory;
        InventoryManager.Instance.goldTextPlayer.text = PlayerGold.ToString();
    }
    void Update()
    {
        HandleMovement();
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerInventoryPanel.SetActive(!PlayerInventoryPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NPC != null)
            {
                AudioManager.Instance.PlaySound(0);
                if (!Interacting)
                {

                    Interacting = true;
                    NPC.ToggleInteractButton(false, true);
                    NPC.StartDialogue();
                }
                else
                {
                    Interacting = false;
                    NPC.ToggleShop(false);
                    NPC.ToggleInteractButton(true, false);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.menu.SetActive(!GameManager.Instance.menu.activeSelf);
        }
    }
    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(movement * MoveSpeed * Time.deltaTime);
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
    }
    public void EarnGold(float amount)
    {
        PlayerGold += amount;
        InventoryManager.Instance.goldTextPlayer.text = PlayerGold.ToString();
        NPC.SpendGold(amount);
    }

    public void SpendGold(float amount)
    {
        PlayerGold -= amount;
        InventoryManager.Instance.goldTextPlayer.text = PlayerGold.ToString();
        NPC.EarnGold(amount);
    }
    public void EquipItem(ItemScriptableObject item)
    {
        playerEquipment.outfit = item;
        playerEquipment.outfitImage.sprite = item.sprite;
        _animator.SetInteger("Outfit", item.OutfitID);
    }

}
