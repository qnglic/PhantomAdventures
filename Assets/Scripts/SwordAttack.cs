using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {    
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.EnemyDeath();
            return;
        }

        FlyEnemyMovement flyEnemy = other.GetComponent<FlyEnemyMovement>();

        if (flyEnemy != null)
        {
            flyEnemy.FlyEnemyDeath();
        }
    }
}
