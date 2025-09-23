using UnityEngine;

public class FlyEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float knockbackForce = 200f;

    public DetectionZone attackZone;
    private bool canMove = true;
    private AudioSource audioSource;
    public bool _hasTarget = false;

    Rigidbody2D rgbd;
    Animator anim;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;
    

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
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

    }

    public void FlyEnemyDeath()
    {
        GetComponent<Animator>().SetTrigger("Hit");
    }
}
