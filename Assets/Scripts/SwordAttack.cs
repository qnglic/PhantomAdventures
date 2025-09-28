using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {    
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();

        if (other.CompareTag("Enemy"))
        {
            enemy.EnemyDeath();
        }
    }
}
