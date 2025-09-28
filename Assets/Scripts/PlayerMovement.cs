using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip pickupCoinSound, pickupHealthSound, swordAttackSound;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private GameObject coinParticles, dustParticles;
    [SerializeField] private GameObject swordHitbox;


    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color blueHealth, redHealth;
    [SerializeField] private TMP_Text coinText;

    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;

    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool canMove;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int coinsCollected = 0;
    public bool isFlipped = false;
    public bool hasKey = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        currentHealth = startingHealth;
        coinText.text = "" + coinsCollected;

        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0)
        {
            FlipSprite(true);
            swordHitbox.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.4f, 0.35f);
        }

        if (horizontalValue > 0)
        {
            FlipSprite(false);
            swordHitbox.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.4f, 0.35f);
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }
        // Mathf.Abs absolute numbers so -1 to 0 to 1. how much until it goes to 0. so 1 for both
        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.linearVelocity.x));

        // sets the speed to whatever speed the character (rigidbody) got
        anim.SetFloat("VerticalSpeed", rgbd.linearVelocity.y);

        // sets isgrounded parameter to whatever checkifgrounded method returns
        anim.SetBool("IsGrounded", CheckIfGrounded());

        // made attackbutton F, can be changed
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("IsAttacking");
            audioSource.pitch = 1.5f;
            audioSource.PlayOneShot(swordAttackSound, 0.05f);
        }

    }

    // Update that has a fixed frame rate
    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinsCollected++;
            coinText.text = "" + coinsCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupCoinSound, 0.5f);
            Instantiate(coinParticles, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }

        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            hasKey = true;
        }
    }

    public void SwordHitboxON()
    {
        swordHitbox.SetActive(true);
    }

    public void SwordHitboxOFF()
    {
        swordHitbox.SetActive(false);
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        int randomValue = Random.Range(0, jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomValue], 0.5f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    private bool CheckIfGrounded()
    {
        // if gravity is flipped the raycast is directed up
        Vector2 rayDirection;
        if (rgbd.gravityScale == 2)
        {
            rayDirection = Vector2.down;
        }
        else
        {
            rayDirection = Vector2.up;
        }

        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, rayDirection, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, rayDirection, rayDistance, whatIsGround);

        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        //Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Respawn();

        }
    }

    public void TakeKnockback(float knockbackForce, float upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        transform.position = spawnPosition.position;
        currentHealth = startingHealth;
        UpdateHealthBar();
        rgbd.linearVelocity = Vector2.zero;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if (currentHealth >= 2)
        {
            fillColor.color = blueHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            int healthToRestore = healthPickup.GetComponent<HealthPickup>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthBar();
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupHealthSound, 0.5f);
            Destroy(healthPickup);

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    public void GravityFlip()
    {
        // flips the sprite and moves it up/down to avoid bug
        isFlipped = !isFlipped;
        rgbd.gravityScale *= -1;
        rgbd.transform.localScale = new Vector3(1, rgbd.transform.localScale.y * -1, 1);
        rgbd.transform.position = new Vector3(rgbd.transform.position.x, rgbd.transform.position.y - 1 * Mathf.Sign(rgbd.gravityScale), 0);
        jumpForce *= -1;

    }
}
