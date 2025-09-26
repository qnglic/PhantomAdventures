using UnityEngine;

public class AntiSlide : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (!playerCollider.CompareTag("Player")) return;

        var rampCollider = GetComponent<Collider2D>();
        rampCollider.isTrigger = false;
        gameObject.tag = "Ground";
    }

    void OnCollisionStay2D(Collision2D playerCollision)
    {
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