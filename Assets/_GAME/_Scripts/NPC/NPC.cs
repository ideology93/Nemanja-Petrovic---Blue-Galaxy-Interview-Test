using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public PlayerController playerController;
    private GameObject npcInventory;
    public bool interactable, interacting;
    public GameObject interactBox;

    private void Start()
    {
        interactable = true;
        playerController = GameManager.Instance.playerController;
        npcInventory = GameManager.Instance.npcInventory;
    }
    public void ToggleInteractBox(bool active, bool interacting)
    {
        interactBox.SetActive(active);
        interacting = this.interacting;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interacting = false;
            ToggleShop(false);
            ToggleInteractBox(false, false);
            playerController.npc = null;
            playerController._interacting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interacting = true;
            ToggleInteractBox(true, false);
            playerController.npc = this;

        }
    }
    public void ToggleShop(bool active)
    {
        npcInventory.SetActive(active);
    }


}
