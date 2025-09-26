using UnityEngine;

public class FlyEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private Transform leftPatrolPoint;
    [SerializeField] private Transform rightPatrolPoint;
    [SerializeField] private AudioClip hurtSound, deathSound;

    public DetectionZone attackZone;
    private AudioSource audioSource;
    private Vector2 homePosition;
    private bool canMove = true;
    public bool _hasTarget = false;
    private bool isFacingRight = true;

    Rigidbody2D rgbd;
    Animator anim;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        homePosition = transform.position;
    }


    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        if (HasTarget)
        {
            ChaseAndAttack();
        }
        else
        {
            ReturnOrPatrol();
        }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(hurtSound, 0.5f);

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }

        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            //Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        print("Flip called at position: " + transform.position + ", isFacingRight: " + isFacingRight);
    }

    public void FlyEnemyDeath()
    {
        GetComponent<Animator>().SetTrigger("Hit");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        // death sound
        canMove = false;
        Destroy(gameObject, 0.6f);
    }

    private void ChaseAndAttack()
    {
        Vector2 playerPos = attackZone.detectedColliders[0].transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos);

        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;
        TryFlipDir(direction.x);

        // chase or attack
        if (distanceToPlayer <= chaseRange && distanceToPlayer > stopDistance)
        {
            rgbd.linearVelocity = new Vector2(direction.x * moveSpeed, 0);
        }
        else if (distanceToPlayer <= stopDistance)
        {
            rgbd.linearVelocity = Vector2.zero;
            anim.SetTrigger("Attack");
        }
        else
        {
            // player too far,,, stop chasing
            HasTarget = false;
        }
    }

    private void Patrol()
    {
        Debug.Log($"Patrol — isFacingRight: {isFacingRight}, posX: {transform.position.x:F3}");
        rgbd.linearVelocity = new Vector2(isFacingRight ? moveSpeed : -moveSpeed, 0);

        float posX = transform.position.x;

        if ((isFacingRight && posX >= rightPatrolPoint.position.x) || (!isFacingRight && posX <= leftPatrolPoint.position.x))
        {
            transform.position = new Vector2(isFacingRight ? rightPatrolPoint.position.x : leftPatrolPoint.position.x, transform.position.y);

            Flip();
        }
    }

    private void ReturnOrPatrol()
    {
        homePosition = transform.position;

        float distanceToHome = Vector2.Distance(transform.position, homePosition);

        if (distanceToHome > 0.05f)
        {

            Vector2 direction = (homePosition - (Vector2)transform.position).normalized;
            rgbd.linearVelocity = new Vector2(direction.x * moveSpeed, 0);
        }
        else
        {
            rgbd.linearVelocity = Vector2.zero;
            Patrol();
        }
    }

    // helper method to flip direction only when actually needed
    private void TryFlip(float targetX)
    {
        float direction = targetX - transform.position.x;

        // prevent flip chaos
        if (Mathf.Abs(direction) < 0.1f)
            return;

        bool shouldFaceRight = direction > 0;

        if (shouldFaceRight != isFacingRight)
        {
            Flip();
        }
    }
    private void TryFlipDir(float directionX)
    {
        if (Mathf.Abs(directionX) < 0.1f)
            return;

        bool shouldFaceRight = directionX > 0;
        
        if (shouldFaceRight != isFacingRight)
        {
            Flip();
        }
        
    }
}
