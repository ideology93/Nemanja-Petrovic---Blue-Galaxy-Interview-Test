using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    private GameObject _playerInventoryPanel;
    Animator _animator;
    public bool _interacting;
    public NPC npc;

    private void Start()
    {
        _playerInventoryPanel = GameManager.Instance.playerInventory;
        _animator = GetComponent<Animator>();
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
            _playerInventoryPanel.SetActive(!_playerInventoryPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (npc != null)
            {
                if (!_interacting)
                {
                    _interacting = true;
                    npc.ToggleShop(true);
                    npc.ToggleInteractBox(false, true);
                }
                else
                {
                    _interacting = false;
                    npc.ToggleShop(false);
                    npc.ToggleInteractBox(true, false);
                }

            }
        }
    }
    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(movement * _moveSpeed * Time.deltaTime);

        // Set Animator parameters
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);

    }
    // public void Interact(NPC npc)
    // {
    //     if (npc == null) return;

    //     npc.OpenShop();
    //     npc.Interact();

    // }



}
