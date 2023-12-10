using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public Inventory PlayerInventory;
    public Inventory NPCInventory;
    public GameObject PlayerInventory_Panel, NPCInventory_Panel;
    public TextMeshProUGUI goldTextPlayer, goldTextNPC;
    public PlayerEquipment playerEquipment;
    

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Inventory Manager is null");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}