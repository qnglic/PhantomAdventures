using UnityEngine;

public class AntiSlide : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private bool isActive = false;

    void Update()
    {
        if (!isActive && box != null && !box.activeSelf)
        {
            var rampCollider = GetComponent<Collider2D>();
            if (rampCollider) rampCollider.isTrigger = false;
            gameObject.tag = "Ground";
            isActive = true;
        }
    }

    // Prevent sliding on slope: put gravity along slope and apply opposite force.
    void OnCollisionStay2D(Collision2D playerCollision)
    {
        if (!isActive) return;
        if (!playerCollision.collider.CompareTag("Player")) return;

        var playerRigidbody = playerCollision.rigidbody;
        if (playerRigidbody == null) return;

        Vector2 surfaceNormal = playerCollision.GetContact(0).normal;
        Vector2 slopeTangent = new Vector2(-surfaceNormal.y, surfaceNormal.x).normalized;

        Vector2 gravity = Physics2D.gravity * playerRigidbody.gravityScale;
        Vector2 gravityAlong = Vector2.Dot(gravity, slopeTangent) * slopeTangent;

        playerRigidbody.AddForce(-gravityAlong * playerRigidbody.mass, ForceMode2D.Force);
    }
}
