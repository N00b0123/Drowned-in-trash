using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamaged(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
