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

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnimation)
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement.hasKey == true)
            {
                hasPlayedAnimation = true;
                anim.SetTrigger("Move");
                playerMovement.hasKey = false;
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
