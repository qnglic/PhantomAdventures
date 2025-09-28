using UnityEngine;

public class StrangeDoor : MonoBehaviour
{
    [SerializeField] private GameObject textPopUp;

    private Animator anim;
    private bool hasPlayedAnimation = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnimation)
        {
            if (playerMovement.hasKey == true)
            {
                hasPlayedAnimation = true;
                anim.SetTrigger("Move");
            }
            else
            {
                textPopUp.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(false);
        }
    }
}
