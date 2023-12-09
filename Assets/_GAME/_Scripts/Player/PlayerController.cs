using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Animator animator;

    private void Start()
    {

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Set Animator parameters
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shopkeeper"))
        {
            Debug.Log("HELLYEA");
        }
    }

}
